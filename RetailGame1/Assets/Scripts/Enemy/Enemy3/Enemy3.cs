using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Damage
{
    Animator anim;
    Rigidbody2D rb;
    public float speed;
    bool isAttacking = false;
    [SerializeField]
    GameObject attackHitBox;
    Transform player;
    bool isPlayer;
    private float positionX;
    private float targetX;
    private RoomController rooom;
    [SerializeField]
    GameObject efectEny;
    [SerializeField]
    Transform enemyEf;
    public GameObject[] effi;
    private List<GameObject> eff = new List<GameObject>();
    float hp;
    public bool playerIn;
    bool chil = false;
    bool angryy = false;
    float stopDis;
    
    public int JumpVelo;
    public bool isPlatform;
    bool isWall;
    [SerializeField]
    Transform wallS;
    [SerializeField]
    Transform platformChek;
    private void Start()
    {
        speed = Random.Range(3, 5);
        uronTa = false;
        hp = Random.Range(4, 8);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackHitBox.SetActive(false);
        rooom = GetComponentInParent<RoomController>();
        JumpVelo = 7;
    }
    private void FixedUpdate()
    {
        stopDis = 5;
        dmgp = PlayerMove.dmg;
        targetX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        positionX = transform.position.x;
      
        if(isPlayer == true && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(DoAttack());
        }
        if (hp <= 0)
        {
            Die();
            rooom.enemies.Remove(gameObject);
            Destroy(eff[0].gameObject);
            eff.RemoveAt(0);
        }
        if (Physics2D.Linecast(transform.position, platformChek.position, 1 << LayerMask.NameToLayer("Platform")))
        {
            isPlatform = true;
        }
        else
        {
            isPlatform = false;
        }
        if(Physics2D.Linecast(transform.position, wallS.position, 1 << LayerMask.NameToLayer("Platform")))
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }
        if (isPlatform == true)
        {
            if (isWall == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpVelo);
            }
        }
        if (Vector2.Distance(transform.position, player.position) < stopDis)
        {
            angryy = true;
            chil = false;
        } 
        else if (Vector2.Distance(transform.position, player.position) > stopDis)
        {
            chil = true;
            angryy = false;
        }
        if (chil == true)
        {
            Chil();
        }
        else if (angryy == true)
        {
            angry();
        }
        
    }

    void Chil()
    {
        anim.Play("Enemy3wait");
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    void angry()
    {
        if (isPlayer == false)
        {
            if (targetX > positionX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
                if (!isAttacking)
                {
                    anim.Play("Enemy3wait");
                }

            }
            else if (targetX < positionX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                if (!isAttacking)
                {
                    anim.Play("Enemy3wait");
                }
            }
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
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        if (collision.gameObject.tag == "DMag" && !uronTa)
        {
            uronTa = true;
            GameObject a = Instantiate(efectEny, enemyEf.position, Quaternion.identity);
            eff.Add(a);
            StartCoroutine(DM());
            StartCoroutine(efDes());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = false;
        }
    }
    IEnumerator DoAttack()
    {
        speed = 0;
        yield return new WaitForSeconds(.25f);
        attackHitBox.SetActive(true);
        anim.Play("Enemy3Atack1");
        yield return new WaitForSeconds(.25f);
        attackHitBox.SetActive(false);
        yield return new WaitForSeconds(.5f);
        isAttacking = false;
        speed = 3;
    }
    IEnumerator efDes()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(eff[0].gameObject);
        eff.RemoveAt(0);
    }
    IEnumerator DM()
    {
        hp = hp - dmgp;
        yield return new WaitForSeconds(.25f);
        uronTa = false;
    }
}
