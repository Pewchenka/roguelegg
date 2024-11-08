using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack2 : MonoBehaviour
{
    float time;
    float timeLiv;
    Animator anim;
    [SerializeField]
    GameObject attackHitBox;
    private void Start()
    {
        attackHitBox.SetActive(false);
        anim = GetComponent<Animator>();
        time = 1.5f;
        timeLiv = 2.5f;
        anim.Play("AtackBoss1");
    }
    private void FixedUpdate()
    {
        time -= Time.deltaTime;
        timeLiv -= Time.deltaTime;
        if (time <= 0)
        {
            anim.Play("AtackBoss2");
            attackHitBox.SetActive(true);
        }
        if (timeLiv <= 0)
        {
            Destroy(gameObject);
        }
    }
}
