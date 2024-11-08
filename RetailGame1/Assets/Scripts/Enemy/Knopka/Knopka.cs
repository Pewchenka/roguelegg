using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knopka : MonoBehaviour
{
    Animator anim;
    private RoomController rooom;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("KnopkaVKL");
        rooom = GetComponentInParent<RoomController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAt"))
        {
            anim.Play("KnopkaVIKL");
            rooom.enemies.Remove(gameObject);
        }
    }
}
