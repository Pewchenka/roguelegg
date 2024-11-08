using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Damage
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    [SerializeField]
    Transform platformChek;
    public float speed;
    public int JumpVelo;
    public bool isPlatform;
    bool isAttacking = false;
    [SerializeField]
    GameObject attackHitBox;
    private float targetX;
    bool isPlayer;
    private float positionX;
    public bool propast;
    [SerializeField]
    Transform groundDet;
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

    private void Start()
    {
        speed = Random.Range(2, 4);
        uronTa = false;
        hp = Random.Range(2, 5);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        //Всем штукам даю штуки
        attackHitBox.SetActive(false);
        //Хитбокса атаки нету
        rooom = GetComponentInParent<RoomController>();
        JumpVelo = 7;
    }
    private void FixedUpdate()
    {
        if (isPlayer == true && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(DoAttack());
        }

        dmgp = PlayerMove.dmg;
        if (hp <= 0)
        {
            Die();
            rooom.enemies.Remove(gameObject);
            Destroy(eff[0].gameObject);
            eff.RemoveAt(0);
        }
        if (Physics2D.Linecast(transform.position, wallS.position, 1 << LayerMask.NameToLayer("Platform")))
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }
        if (Physics2D.Linecast(transform.position, groundDet.position, 1 << LayerMask.NameToLayer("Platform")))
        {
            propast = false;
        }
        else
        {
            propast = true;
        }
        if (Physics2D.Linecast(transform.position, platformChek.position, 1 << LayerMask.NameToLayer("Platform")))
        {
            isPlatform = true;
        }
        else
        {
            isPlatform = false;
        }
        //Стреляем лучом от объекта в персонажа и есле на пересечении есть объект с тегом "Платформа" говорит "Правда", иначе "Ложь"
        targetX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        //берем у объекта с тегом "игрок" позицию X
        positionX = transform.position.x;
        if (isPlayer == false)
        {
            if (targetX > positionX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
                if (!isAttacking)
                {
                    anim.Play("EnemyWait");
                }

            }
            else if (targetX < positionX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                if (!isAttacking)
                {
                    anim.Play("EnemyWait");
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (isPlatform == true)
        {
            if (propast == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpVelo);
            }
            if (isWall == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpVelo);
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
    //если объект косается объекта с тегом "игрок", то переменная "исИгрок" = правде
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = false;
        }
    }
    //если объект косается объекта с тегом "игрок", то переменная "исИгрок" = ложь
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, groundDet.position);
        Gizmos.DrawLine(transform.position, wallS.position);
    }
    IEnumerator DoAttack()
    {
        speed = 0;
        yield return new WaitForSeconds(.25f);
        attackHitBox.SetActive(true);
        anim.Play("EnemyAtack1");
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
