using UnityEngine;
using System;


public class PlayerControl : MonoBehaviour
{

    public float speed = 10.0f;             // Define a velocidade da raquete
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    void ResetPlayer(){
        rb2d.linearVelocity = Vector2.zero;
        Vector3 pos = transform.position;
        pos.x = -0.01f;
        pos.y = -3.57f;
        transform.position = pos;
    }

    void RestartGame(){
        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 playerPos = transform.position;
        Vector3 dir = mousePos - playerPos;
        dir.Normalize();
        double dir_final;
        double dir_x = mousePos.x - playerPos.x;
        double dir_y = mousePos.y - playerPos.y;
        dir_final = (Math.Pow(dir_x,2)) + (Math.Pow(dir_y,2));
        Vector3 speedVec = dir * speed;
        rb2d.linearVelocity = speedVec;
        if(dir_final <= 0.1f){
            rb2d.linearVelocity = Vector2.zero;
        }else{
            rb2d.linearVelocity = speedVec;
        }
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -15.2f, 15.2f);
        pos.y = Mathf.Clamp(pos.y, -4f, -4f);

        transform.position = pos;

    }
}
