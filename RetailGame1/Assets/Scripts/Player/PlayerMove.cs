using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : Damage
{
    Animator anim;
    Rigidbody2D rb;
    [SerializeField]
    Transform platformChek;
    public float speed;
    public int JumpVelo;
    public bool isPlatform;
    public bool isStairs;
    bool isAttacking = false;
    [SerializeField]
    GameObject attackHitBox;
    [SerializeField]
    float radius;
    public LayerMask platform;
    public LayerMask stairs;
    public static float dmg;
    int numberOfLives;
    public Image[] lives;
    public Sprite fullLive;
    public Sprite emptyLive;
    [SerializeField]
    GameObject effectPla;
    [SerializeField]
    Transform playerEf;
    public GameObject[] effi;
    private List<GameObject> eff = new List<GameObject>();

    [SerializeField]
    Transform atHit;
    [SerializeField]
    GameObject atHite;
    private void Start()
    {
        speed = 5;
        JumpVelo = 7;
        uronTa = false;
        dmg = 1;
        numberOfLives = 4;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Всем штукам даю штуки
        //attackHitBox.SetActive(false);
        //Хитбокса атаки нету
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            int choose = UnityEngine.Random.Range(1, 3);
            anim.Play("PlayerAtack" + choose);
            StartCoroutine(DoAttack());
        }
        //Когда нажата фаир1 (Левая кнопка мыши) переменная - сучайное значение от 1 до 2(не знаю почему так) и проиграть анимацию атака+переменная, начать коротин.
        //Остается в апдейте, не в нем не работает!
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Php > numberOfLives)
        {
            Php = numberOfLives;
        }
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < Php)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }
            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }

        //Стреляем лучом от объекта в персонажа и есле на пересечении есть объект с тегом "Платформа" говорит "Правда", иначе "Ложь"
        dmge = 1;
        if (Php <= 0)
        {
            Destroy(eff[0].gameObject);
            eff.RemoveAt(0);
            Die();
            RoomCreate.dif = 1;
            Php = 4;
            SceneManager.LoadScene(1);
        }

        isPlatform = Physics2D.OverlapCircle(platformChek.position, radius, platform);
        isStairs = Physics2D.OverlapCircle(platformChek.position, radius, stairs);
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if ((isPlatform || isStairs) && !isAttacking)
                anim.Play("PlayerWolk");
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if ((isPlatform || isStairs) && !isAttacking)
                anim.Play("PlayerWolk");
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (isPlatform || isStairs)
            {
                if (!isAttacking)
                {
                    anim.Play("PlayerWait");
                }
            }

        }

        if (Input.GetKey(KeyCode.Space) && (isPlatform == true || isStairs == true))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpVelo);
            anim.Play("PlayerJump");
        }
        //Управление, прыгать только если НаПлатформе - правда, анимации не прыжка, только если НаПлатформе - правда 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, platformChek.position);
        //Делает этот луч видимым
    }
    IEnumerator DoAttack()
    {
        Instantiate(atHite, atHit.position, Quaternion.identity);
        //attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.25f);
        //attackHitBox.SetActive(false);
        isAttacking = false;
    }
    //Аттак хитбокс правда, после 0.25 секунд ложь и атка тоже ложь
    /*private void OnCollisionEnter2D(Collision2D platformChec)
    {

            isPlatform = true;
    }
    private void OnCollisionExit2D(Collision2D platformChec)
    {
            isPlatform = false;
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAt") && !uronTa)
        {
            uronTa = true;
            GameObject a = Instantiate(effectPla, playerEf.position, Quaternion.identity);
            eff.Add(a);
            StartCoroutine(DamagePTake());
            StartCoroutine(efDes());

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyAt" && !uronTa) 
        {
            uronTa = true;
            GameObject a = Instantiate(effectPla, playerEf.position, Quaternion.identity);
            eff.Add(a);
            StartCoroutine(DamagePTake());
            StartCoroutine(efDes());
        }
        if (collision.gameObject.tag == "DMag" && !uronTa)
        {
            uronTa = true;
            GameObject a = Instantiate(effectPla, playerEf.position, Quaternion.identity);
            eff.Add(a);
            StartCoroutine(DamagePTake());
            StartCoroutine(efDes());
        }
    }
    IEnumerator efDes()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(eff[0].gameObject);
        eff.RemoveAt(0);
    }
}
