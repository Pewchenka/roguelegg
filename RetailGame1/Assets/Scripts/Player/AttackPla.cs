using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPla : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(live());
    }

    IEnumerator live()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
