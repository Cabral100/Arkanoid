using UnityEngine;
using System.Collections;
public class SideWalls : MonoBehaviour {

    // Verifica colisões da bola nas paredes
    void OnTriggerEnter2D (Collider2D hitInfo) {
        if (hitInfo.CompareTag("Ball") && CompareTag("BottomWall"))
        {
            string wallName = gameObject.tag;
            GameManager.Score(wallName);
        }
    }
}
