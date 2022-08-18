using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //Array kategori setiap room sesuai pintunya
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    //Room tertutup jika ada dua pintu dari dua ruangan menuju arah yg sama
    public GameObject closedRooms;

    //List of rooms
    public List<GameObject> rooms;

    //variabel untuk boss
    public float waitTime;
    private bool spawnedBoss = false;
    public GameObject boss;

    void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            //spawn boss jika syarat terpenuhi
            Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
            spawnedBoss = true;
        }

        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
