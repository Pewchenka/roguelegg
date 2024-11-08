using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Damage
{
    Animator anim;
    Rigidbody2D rb;
    public float speed;
    public int JumpVelo;
    public bool isPlatform;
    bool isAttacking = false;
    private float targetX;
    private float positionX;
    [SerializeField]
    Transform platformChek;
    bool isWall;
    [SerializeField]
    Transform wallS;
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
    public Transform[] atak;
    [SerializeField]
    GameObject bab;
    float time;

    float disStop;
    float disRet;
    public Transform player;
    //public List<GameObject> pop;


    private void Start()
    {
        disRet = 5;
        disStop = 8;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        JumpVelo = 6;
        speed = Random.Range(3, 5);
        uronTa = false;
        hp = Random.Range(10, 17);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rooom = GetComponentInParent<RoomController>();
        time = 8;
    }
    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, wallS.position, 1 << LayerMask.NameToLayer("Platform")))
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }
        if (Physics2D.Linecast(transform.position, platformChek.position, 1 << LayerMask.NameToLayer("Platform")))
        {
            isPlatform = true;
        }
        else
        {
            isPlatform = false;
        }
        if (!isAttacking)
        {
            anim.Play("BossWait");
            StartCoroutine(DoAttack1());
        }
        time -= Time.deltaTime;
        if (time <= 0)
        {
            anim.Play("BossWait");
            StartCoroutine(DoAttack2());
            time = 10;
        }
        if (hp <= 0)
        {
            Die();
            rooom.enemies.Remove(gameObject);
            Destroy(eff[0].gameObject);
            eff.RemoveAt(0);
        }
        if (isPlatform == true)
        {
            if (isWall == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpVelo);
            }
        }
        targetX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        positionX = transform.position.x;
        dmgp = PlayerMove.dmg;
        if (targetX > positionX)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (targetX < positionX)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Vector2.Distance(transform.position, player.position) > disStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < disStop && (Vector2.Distance(transform.position, player.position) > disRet))
        {
            transform.position = this.transform.position;
        }
        else if ((Vector2.Distance(transform.position, player.position) < disRet))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
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
    IEnumerator DoAttack1()
    {
        isAttacking = true;
        anim.Play("BossAtack1");
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        Instantiate(snari, snarSpa.position, Quaternion.identity);
        anim.Play("BossWait");
        yield return new WaitForSeconds(2.5f);
        isAttacking = false;
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
    IEnumerator DoAttack2()
    {
        isAttacking = true;
        anim.Play("BossAtack1");
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        foreach (Transform at in atak)
        {
            GameObject atack = Instantiate(bab, at.position, Quaternion.identity);
        }
        //Instantiate(bab, atak.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        isAttacking = false;
    }

}
 



