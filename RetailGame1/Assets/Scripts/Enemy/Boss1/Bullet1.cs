using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    Rigidbody2D rb;
    private float targetX;
    private float positionX;
    [SerializeField]
    float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        positionX = transform.position.x;

    }
    private void FixedUpdate()
    {
        if (targetX > positionX)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (targetX < positionX)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
