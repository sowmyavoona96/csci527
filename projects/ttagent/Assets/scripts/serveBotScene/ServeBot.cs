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
    bool isServe;

    void Start()
    {
        isServe = false;
        Debug.Log("ball starts");

        ballRB = ballObj.GetComponent<Rigidbody>();
        ball = ballObj.GetComponent<SingleAgentBall>();
        serveBall();
    }
   public void serveBall() {
        
        isServe = true;

        Debug.Log("serving ball");
        var ballPos_X = gameObject.transform.position.x;
        var ballPos_Y = init_transform_ball_Y_LB;
        // Random.Range(init_transform_ball_Y_LB, init_transform_ball_Y_UB);
        var ballPos_Z = Random.Range(init_transform_ball_Z_LB, init_transform_ball_Z_UB);

        //  gameObject.transform.position = new Vector3(gameObject.transform.position.x,
        //    gameObject.transform.position.y, ballPos_Z);

        ballRB.transform.position = new Vector3(ballPos_X, ballPos_Y,
            ballPos_Z);

        ballRB.velocity = new Vector3(3f, 3.5f, 0);
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

    public void FixedUpdate()
    {
        //Debug.Log("fixed update: " + (isServe?"true" : "false"));
        if (isServe) {
            /*
            Debug.Log("serving ball");
            var ballPos_X = gameObject.transform.position.x;
            var ballPos_Y = init_transform_ball_Y_LB;
            // Random.Range(init_transform_ball_Y_LB, init_transform_ball_Y_UB);
            var ballPos_Z = Random.Range(init_transform_ball_Z_LB, init_transform_ball_Z_UB);

            //  gameObject.transform.position = new Vector3(gameObject.transform.position.x,
            //    gameObject.transform.position.y, ballPos_Z);

            ballRB.transform.position = new Vector3(ballPos_X, ballPos_Y,
                ballPos_Z);

            ballRB.AddForce(Vector3.right * 20f + Vector3.up * 23f);
            ball.setLastHitAgent(TeamEnum.BOT);
            */
            isServe = false;
        }
    }

}
