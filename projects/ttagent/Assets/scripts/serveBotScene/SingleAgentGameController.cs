using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using static SingleAgentConstants;

public class SingleAgentGameController : MonoBehaviour
{

    public SingleAgentBall ball;
    public SingleAgentBat agent;
    public ServeBot bot;
    Rigidbody ballRB;

    int resetTimer = 0;
    float maxEnvironmentSteps;
    float ballHitReward;
    float ballOverNetReward;
    float agentReward;
    EnvironmentParameters environmentParameters;

    public void Start()
    {
        Debug.Log("game controller called");
        ballRB = ball.GetComponent<Rigidbody>();
        environmentParameters = Academy.Instance.EnvironmentParameters;

        maxEnvironmentSteps = environmentParameters.GetWithDefault(env_max_academy_steps, 10000);
        ballHitReward = environmentParameters.GetWithDefault(env_reward_ball_hit, 0);
        ballOverNetReward = environmentParameters.GetWithDefault(env_reward_ball_over_net, 0);
        agentReward = environmentParameters.GetWithDefault(env_reward_agent, 1);
        matchReset();
    }

    void agentScores(TeamEnum team)
    {
        
        if (team.Equals(TeamEnum.AGENT))
            this.agent.addScore(1);
        else
            bot.addScore(1);

        //Debug.Log("agent scores: " + agent.ToString());
        //Debug.Log("CR: " + agentA.GetCumulativeReward());
        
      episodeReset();

    }

    public void agentPenalty(TeamEnum team)
    {
        if (team.Equals(TeamEnum.AGENT))
            this.agent.AddReward(environmentParameters.GetWithDefault(env_reward_penalty, 0));
    }

    void agentHitsBallReward(Team team)
    {
        Debug.Log("Agent hits ball reward: " + team.teamEnum.ToString()
        + ", reward: " + environmentParameters.GetWithDefault(env_reward_ball_hit, 0));

        if (team.isAgent())
            this.agent.AddReward(environmentParameters.GetWithDefault(env_reward_ball_hit, 0));
    }

    public void agentHitsBallAcrossNetReward(Team team)
    {
        //Debug.Log("Agent hits ball across net reward: " + agent.teamEnum.ToString());

        if (team.isAgent())
            this.agent.AddReward(environmentParameters.GetWithDefault(env_reward_ball_over_net, 0));
    }

    public void ballHitsAgent(Team team,
       ObjectTypeEnum lastCollidedWith,
       TeamEnum lastHitAgentTeam,
        TeamEnum nextAgentTurn)
    {

        //agent didn't let the ball bounce on his side (check serve case)
        //or agent hits the ball twice

        // Debug.Log("GC ball hits agent: " + agent.teamEnum.ToString());

        agentHitsBallReward(team);

        //negative reward for foul moves
        if (lastCollidedWith != team.getFloor()
            || lastHitAgentTeam == team.getTeam())
            
        {
            agentPenalty(team.getTeam());
            agentScores(team.getOpponentTeam());

        }

    }

    public void ballHitsFloor(Team floor,
       ObjectTypeEnum lastCollidedWith,
       TeamEnum lastHitAgentTeam,
       TeamEnum nextAgentTurn)
    {
        //hits on his own court side?
        //or if agent doesn't serve 
        //agent let ball bounce twice on his court

        //Debug.Log("GC: ball hits floor: " + floor.teamEnum.ToString());

        if (lastHitAgentTeam == floor.getTeam()
            || lastCollidedWith == floor.getFloor())
        {
            agentScores(floor.getOpponentTeam());

        }
    }

    public void ballHitsBoundary(Team boundary,
        ObjectTypeEnum lastCollidedWith,
        TeamEnum lastHitAgentTeam,
        TeamEnum nextAgentTurn)
    {
        //Debug.Log("GC ball hits boundary: " + boundary.teamEnum.ToString());

        if (nextAgentTurn != TeamEnum.NA)
        {
            agentPenalty(nextAgentTurn == TeamEnum.AGENT ?
                  TeamEnum.BOT : TeamEnum.AGENT);
            agentScores(nextAgentTurn == TeamEnum.AGENT ?
                  TeamEnum.BOT : TeamEnum.AGENT);

        }
           

        else if (lastHitAgentTeam == TeamEnum.AGENT)
            agentScores(TeamEnum.BOT);
        else if (lastHitAgentTeam == TeamEnum.BOT)
            agentScores(TeamEnum.AGENT);
        else
        {
            Debug.Log("ball hits boundary edge case");
          episodeReset();
        }
    }

    public void agentHitsNet(Team team)
    {
        agentScores(team.getOpponentTeam());
    }

    public void ballCollidesEdgeCase() {
       episodeReset();

    }

    void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= maxEnvironmentSteps && maxEnvironmentSteps > 0)
        {
            Debug.Log("reset timer exceeded max steps");
            agent.EpisodeInterrupted();
            episodeReset();
        }

    }

   
    void episodeReset()
    {
        resetTimer = 0;
        agent.EndEpisode();
        agent.resetRacket();
        bot.serveBall();
        ball.resetParameters();
        //Debug.Log("CR: " + agentA.GetCumulativeReward());
    }

    public void matchReset()
    {
        Debug.Log("Resetting match");
        episodeReset();
        agent.resetScore();
        bot.resetScore();
    }

}