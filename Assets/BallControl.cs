using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a bola

    public float speed = 5f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 1f); // começa após 1 segundo
    }

    void GoBall()
    {
        rb2d.linearVelocity = new Vector2(0f, -speed);
    }
    
    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player")){
            Vector2 vel;
            vel.x = rb2d.linearVelocity.x;
            vel.y = (rb2d.linearVelocity.y / 2) + (coll.collider.attachedRigidbody.linearVelocity.y / 3);
            rb2d.linearVelocity = vel;
        }
        if (coll.gameObject.tag == "Brick"){
            Destroy(coll.gameObject);  
        }

    }

    void ResetBall(){
        rb2d.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocity = rb2d.linearVelocity.normalized * speed;

        if(rb2d.position.y < -5.4f ){
            ResetBall();
        }
    }
}
