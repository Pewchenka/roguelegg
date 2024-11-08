using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (PlayerMove.Php)
            {
                case 1:
                    PlayerMove.Php++;
                    Destroy(gameObject);
                    break;
                case 2:
                    PlayerMove.Php++;
                    Destroy(gameObject);
                    break;
                case 3:
                    PlayerMove.Php++;
                    Destroy(gameObject);
                    break;
            } 
        }
    }
}
