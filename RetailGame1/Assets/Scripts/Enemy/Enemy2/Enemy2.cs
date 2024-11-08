using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Damage
{
    Rigidbody2D rb;
    Animator anim;
    bool isAttacking = false;
    private float targetX;
    private float positionX;
    private RoomController rooom;
    [SerializeField]
    GameObject efectEny;
    [SerializeField]
    Transform enemyEf;
    public GameObject[] effi;
    private List<GameObject> eff = new List<GameObject>();
    float hp;
    public Transform snarSpa;
    public GameObject snari;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        uronTa = false;
        hp = Random.Range(4, 6);
        anim = GetComponent<Animator>();
        rooom = GetComponentInParent<RoomController>();
    }
    private void FixedUpdate()
    {
        targetX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        positionX = transform.position.x;
        dmgp = PlayerMove.dmg;
        if (targetX > positionX)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (targetX < positionX)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (!isAttacking)
        {
            anim.Play("Enemy2Wait");
            StartCoroutine(DoAttack1());
        }
        if (hp <= 0)
        {
            Die();
            rooom.enemies.Remove(gameObject);
            Destroy(eff[0].gameObject);
            eff.RemoveAt(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAt") && !uronTa)
        {
            uronTa = true;
            GameObject a = Instantiate(efectEny, enemyEf.position, Quaternion.identity);
            eff.Add(a);
            //StartCoroutine(DamageTake());
            StartCoroutine(DM());
            StartCoroutine(efDes());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DMag" && !uronTa)
        {
            uronTa = true;
            GameObject a = Instantiate(efectEny, enemyEf.position, Quaternion.identity);
            eff.Add(a);
            StartCoroutine(DM());
            StartCoroutine(efDes());
        }
    }

    IEnumerator DM()
    {
        hp = hp - dmgp;
        yield return new WaitForSeconds(.25f);
        uronTa = false;
    }
    IEnumerator efDes()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(eff[0].gameObject);
        eff.RemoveAt(0);
    }
    IEnumerator DoAttack1()
    {
        isAttacking = true;
        anim.Play("Enemy2Atack");
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Instantiate(snari, snarSpa.position, Quaternion.identity);
        anim.Play("Enemy2Wait");
        yield return new WaitForSeconds(2.5f);
        isAttacking = false;
    }
}
