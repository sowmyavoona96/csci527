using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SingleAgentConstants;

public class ServeBot : MonoBehaviour
{
    public GameObject ballObj;
    Rigidbody ballRB;
    SingleAgentBall ball;
    int score;
    void Start()
    {
        Debug.Log("ball starts");

        ballRB = ballObj.GetComponent<Rigidbody>();
        ball = ballObj.GetComponent<SingleAgentBall>();
        serveBall();
    }
   public void serveBall() {
        
        //Debug.Log("serving ball");
        var ballPos_X = gameObject.transform.position.x;
        var ballPos_Y = Random.Range(init_transform_ball_Y_LB, init_transform_ball_Y_UB);
        var ballPos_Z = Random.Range(init_transform_ball_Z_LB, init_transform_ball_Z_UB);

        //  gameObject.transform.position = new Vector3(gameObject.transform.position.x,
        //    gameObject.transform.position.y, ballPos_Z);

        ballRB.transform.position = new Vector3(ballPos_X, ballPos_Y,
            ballPos_Z);


        var x_vel = Random.Range(2.7f, 4f);
        var y_vel = 0f;

        if (x_vel < 2.9)
            y_vel = Random.Range(3.7f, 4f);

        if(x_vel < 3f)
            y_vel = Random.Range(3.4f, 4f);

        var z_vel = 0;

        ballRB.velocity = new Vector3(3.5f, 4f, z_vel);

        //2.7 - 3.7 to 4
        //2.8 - 3.7 to 4
        //2.9 - 3.4 to 4
        //3   - 3.3 - 4
        //3.5 - 3 - 3.5 - 
        //4   - 2.5 - 3 - 3.5?


        //TODO add velocity: x-range and corresponding y ranges - choose randomly from here
        //TODO add z range - that works for above list

        //ballRB.AddForce(Vector3.right * 20f + Vector3.up * 23f);
        ball.setLastHitAgent(TeamEnum.BOT);
    }


    public void resetScore()
    {
        score = 0;
    }

    public void addScore(int addBy)
    {
        score = score + addBy;
    }

    public int getScore()
    {
        return score;
    }

}
