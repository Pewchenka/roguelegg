using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreate : MonoBehaviour
{
    public Room[] FinalRoom;
    public Room[] RoomPrefab;
    private List<Room> spawnedRoom = new List<Room>();
    static public int dif;
    bool starto;
    int RoomCount;
    [SerializeField]
    Room FirstRoom;


    private void Start()
    {
        spawnedRoom.Add(FirstRoom);
        starto = true;
        RoomCount = Random.Range(2, 6);

    }
    private void FixedUpdate()
    {
        if (starto == true)
        {
            for (int i = 0; i < dif * RoomCount; i++)
            {
                RoomCreated();
            }
            Room newRoom = Instantiate(FinalRoom[Random.Range(0, FinalRoom.Length)]);
            newRoom.transform.position = spawnedRoom[spawnedRoom.Count - 1].End.position - newRoom.start.localPosition;
            starto = false;
        }
    }
    private void RoomCreated()
    {
        Room newRoom = Instantiate(RoomPrefab[Random.Range(0, RoomPrefab.Length)]);
        newRoom.transform.position = spawnedRoom[spawnedRoom.Count - 1].End.position - newRoom.start.localPosition;
        spawnedRoom.Add(newRoom);
    }
}
