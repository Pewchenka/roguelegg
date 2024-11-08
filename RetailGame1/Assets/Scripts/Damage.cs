using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public static float Php;
    public static float Ehp;
    public float dmgp;
    public bool uronTa;
    public float dmge;

    void Start()
    {
    }

    public IEnumerator DamageTake()
    {
        Php = Ehp - dmgp;
        yield return new WaitForSeconds(.25f);
        uronTa = false;

    }
    public IEnumerator DamagePTake()
    {
        Php = Php - dmge;
        yield return new WaitForSeconds(.25f);
        uronTa = false;

    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
