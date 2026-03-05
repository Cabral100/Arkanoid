using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static AudioSource source;
    public AudioClip perderVidaSom;
    public AudioClip PowerUpSound;
    public static int Player1Lifes = 1;
    public static int Balls = 1;
    public GUISkin layout;

    GameObject theBall;
    GameObject P1;
    GUIStyle infoStyle;

    GUIStyle smallLabel;

    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        P1 = GameObject.FindGameObjectWithTag("Player");

        // Estilo das informações das fases
        infoStyle = new GUIStyle();
        infoStyle.normal.textColor = Color.white;
        infoStyle.fontSize = 20;
        infoStyle.alignment = TextAnchor.UpperCenter;

        smallLabel = new GUIStyle();
        smallLabel.normal.textColor = Color.white;
        smallLabel.fontSize = 18;
        smallLabel.alignment = TextAnchor.UpperCenter;
        smallLabel.wordWrap = true;

        source = GetComponent<AudioSource>();
    }

    void OnGUI()
    {
        GUI.skin = layout;

        string sceneName = SceneManager.GetActiveScene().name;

        // ================= MENU =================
        if (sceneName == "SampleScene")
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, 40, 300, 60), "ARKANOID");

            if (GUI.Button(new Rect(Screen.width / 2 - 75, 110, 200, 50), "START"))
            {
                SceneManager.LoadScene("Fase1");
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 75, 180, 200, 50), "DESENVOLVEDORES"))
            {
                SceneManager.LoadScene("Desenvolvedores");
            }
        }

        // ================= DESENVOLVEDORES =================
        else if (sceneName == "Desenvolvedores")
        {
            GUI.Label(new Rect(Screen.width / 2 - 250, 40, 500, 60), "DESENVOLVEDORES");
            GUI.Label(
                new Rect(Screen.width / 2 - 300, 120, 600, 300),
                "Pedro Henrique Ferreira Valim\n" +
                "R.A 24.123.048-1\n\n" +
                "Lucas Tonoli Cabral Duarte\n" +
                "R.A 24.123.032-5",
                infoStyle
            );

            if (GUI.Button(new Rect(Screen.width / 2 - 75, 300, 200, 50), "VOLTAR"))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        // ================= FASES =================
        else if (sceneName == "Fase1" || sceneName == "Fase2" || sceneName == "Fase3" || sceneName == "FinalScene")
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, 40, 300, 60), sceneName);
            if (GUI.Button(new Rect(50, 35, 120, 40), "MENU"))
            {
                SceneManager.LoadScene("SampleScene");
            }
            if (GUI.Button(new Rect(1000, 300   , 120, 40), "LANÇAR"))
            {
                LancarBola();
            }
        }
    }

    void RestartMatch()
    {
        Player1Lifes = 1;   
        Balls = 1;
        if (theBall != null)
            theBall.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);

        if (P1 != null)
            P1.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
    }

    void Update()
    {
        // Só verifica tijolos se estiver em uma cena de jogo
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Fase1" || scene.name == "Fase2" || scene.name == "Fase3")
        {
            print("Vidas do jogador: " + Player1Lifes);
            //print("Bolas: " + Balls);
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Brick");
            if(Player1Lifes < 1){
                EndGame();
            }
            if (gos.Length <= 1)
            {
                if (scene.name == "Fase1")
                {
                    SceneManager.LoadScene("Fase2");
                    Balls = 1;
                }
                else if (scene.name == "Fase2")
                {
                    SceneManager.LoadScene("Fase3");
                    Balls = 1;
                }
                else if (scene.name == "Fase3")
                {
                    SceneManager.LoadScene("FinalScene");
                    Balls = 1;
                }
            }
        }
    }

    void EndGame()
    {
        Player1Lifes = 1;
        Balls = 1;
        SceneManager.LoadScene("SampleScene"); // volta para o menu
    }

    public static void Score(string wallID)
    {
        if (wallID == "BottomWall" && Balls == 1)
        {
            source.PlayOneShot(FindObjectOfType<GameManager>().perderVidaSom);
            Player1Lifes--;
        }
    }

    public void LancarBola()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        ball.GetComponent<BallControl>().GoBall(rb);
    }
}