using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;
    public GameObject[] enemyType;
    public Transform[] enemySpawner;
    public List<GameObject> enemies;
    private bool spawned;
    bool doorStay;
    [SerializeField]
    Transform pickUp;
    [SerializeField]
    GameObject[] pickUpType;
    List<GameObject> pick;
    private void Start()
    {
        doorStay = false;
    }
    private void FixedUpdate()
    {
        if (!doorStay)
        {
            door1.SetActive(false);
            door2.SetActive(false);
        }
        else
        {
            door1.SetActive(true);
            door2.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spawned)
        {
            spawned = true;
            
            foreach (Transform spawner in enemySpawner)
            {
                GameObject enemyTypes = enemyType[Random.Range(0, enemyType.Length)];
                GameObject enemy = Instantiate(enemyTypes, spawner.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = transform;
                enemies.Add(enemy);
                doorStay = true;
                //StartCoroutine(Wait());

            }
            StartCoroutine(CheckEnemies());
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        doorStay = false;
        switch (Random.Range(1, 4))
        {
                    case 1:
                GameObject pickUpTypes = pickUpType[Random.Range(0, pickUpType.Length)];
                Instantiate(pickUpTypes, pickUp.position, Quaternion.identity);
            break;
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        doorStay = true;
    }
}
