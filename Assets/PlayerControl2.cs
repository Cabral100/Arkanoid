using UnityEngine;
using System;
//public AudioSource source;

public class PlayerControl2 : MonoBehaviour
{

    public float speed = 7.0f;             // Define a velocidade da raquete
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete
    public Transform ball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        //Source = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void ResetPlayer(){
        rb2d.linearVelocity = Vector2.zero;
        Vector3 pos = transform.position;
        pos.x = 0.06f;
        pos.y = 3.57f;
        transform.position = pos;
    }

    void RestartGame(){
        ResetPlayer();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 ballPos = ball.position;
        ballPos.z = 0f;
        Vector3 playerPos = transform.position;
        Vector3 dir = ballPos - playerPos;
        dir.Normalize();
        double dir_final;
        double dir_x = ballPos.x - playerPos.x;
        double dir_y = ballPos.y - playerPos.y;
        dir_final = (Math.Pow(dir_x,2)) + (Math.Pow(dir_y,2));
        Vector3 speedVec = dir * speed;
        rb2d.linearVelocity = speedVec;
        if(dir_final <= 0.1f){
            rb2d.linearVelocity = Vector2.zero;
        }else{
            //speedVec.y = 0;
            rb2d.linearVelocity = speedVec;
        }
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -2.4f, 2.4f);
        pos.y = Mathf.Clamp(pos.y, 3.0f,4.6f);

        transform.position = pos;
    }
}
