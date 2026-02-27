using UnityEngine;
using System.Collections;
public class SideWalls : MonoBehaviour {

    public AudioSource source;
    // Verifica colisões da bola nas paredes
    void OnTriggerEnter2D (Collider2D hitInfo) {
        if (hitInfo.CompareTag("Ball") && (CompareTag("TopWall") || CompareTag("BottomWall")))
        {
            source.Play();
            string wallName = gameObject.tag;
            GameManager.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
            StartCoroutine(DebugWhilePlaying());
        }
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private IEnumerator DebugWhilePlaying()
    {
        while (source.isPlaying)
        {
            Debug.Log("O som está tocando!");
            yield return null; // espera 1 frame
        }

        Debug.Log("O som parou!");
    }


}
