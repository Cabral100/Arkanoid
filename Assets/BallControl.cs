using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a bola
    public GameObject ballPrefab;
    public float speed = 5f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void GoBall(Rigidbody2D rb2d)
    {
        rb2d.linearVelocity = new Vector2(0f, speed);
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
        if (coll.gameObject.tag == "LifeBrick")
            {
                GameManager.Player1Lifes++;
                GameManager.source.PlayOneShot(FindObjectOfType<GameManager>().PowerUpSound);
                Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "MultiBallBrick")
            {
                Vector3 pos = transform.position;

                GameObject ball1 = Instantiate(ballPrefab, pos, Quaternion.identity);
                GameObject ball2 = Instantiate(ballPrefab, pos, Quaternion.identity);

                Rigidbody2D rb1 = ball1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = ball2.GetComponent<Rigidbody2D>();

                rb1.linearVelocity = new Vector2(2f, 8f);
                rb2.linearVelocity = new Vector2(-2f, 8f);

                GameManager.source.PlayOneShot(FindObjectOfType<GameManager>().PowerUpSound);

                GameManager.Balls += 2;
                Destroy(coll.gameObject);
        }

    }

    void ResetBall(){
        rb2d.linearVelocity = Vector2.zero;
        transform.position = new Vector2(0, -3f);
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
    }

    // Update is called once per frame
    void Update()
        {
        rb2d.linearVelocity = rb2d.linearVelocity.normalized * speed;

        if (rb2d.position.y < -5.4f)
        {
            if(GameManager.Balls > 1){
                GameManager.Balls--;
                Destroy(gameObject);
            }else if (GameManager.Balls == 1)
            {
                ResetBall();
            }
        }
    }
}
