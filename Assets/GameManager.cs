using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;

    public GUISkin layout;

    GameObject theBall;
    GameObject P1;
    GameObject P2;

    GUIStyle infoStyle;

    GUIStyle smallLabel;

    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        P1 = GameObject.FindGameObjectWithTag("Player");
        P2 = GameObject.FindGameObjectWithTag("Player2");

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
            GUI.Label(new Rect(Screen.width / 2 - 100, 40, 300, 60), "DESENVOLVEDORES");
            GUI.Label(
                new Rect(Screen.width / 2 - 300, 120, 600, 300),
                "Pedro Henrique Ferreira Valim\n" +
                "R.A 24.123.048-1\n\n" +
                "Lucas Tonoli Cabral Duarte\n" +
                "R.A 24.123.032-5",
                infoStyle
            );

            if (GUI.Button(new Rect(Screen.width / 2 - 75, 350, 200, 50), "VOLTAR"))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        // ================= FASES =================
        else if (sceneName == "Fase1" || sceneName == "Fase2" || sceneName == "Fase3")
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, 40, 300, 60), sceneName);
            if (GUI.Button(new Rect(50, 35, 120, 40), "RESTART"))
            {
                RestartMatch();
            }
        }
    }

    void RestartMatch()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        if (theBall != null)
            theBall.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);

        if (P1 != null)
            P1.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);

        if (P2 != null)
            P2.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
    }

    void Update()
    {
        // Só verifica tijolos se estiver em uma cena de jogo
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Fase1" || scene.name == "Fase2" || scene.name == "Fase3")
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Brick");
            print(gos.Length);

            if (gos.Length == 0)
            {
                if (scene.name == "Fase1")
                {
                    SceneManager.LoadScene("Fase2");
                }
                else if (scene.name == "Fase2")
                {
                    SceneManager.LoadScene("Fase3");
                }
                else if (scene.name == "Fase3")
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
    }

    void EndGame()
    {
        PlayerScore1 = 0;

        SceneManager.LoadScene("SampleS"); // volta para o menu
    }

    public static void Score(string wallID)
    {
        if (wallID == "TopWall")
        {
            PlayerScore1++;
        }
        else if (wallID == "BottomWall")
        {
            PlayerScore2++;
        }
    }
}