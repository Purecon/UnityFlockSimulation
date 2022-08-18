﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 --> butuh pintu bottom (bawah)
    //2 --> butuh pintu top (atas)
    //3 --> butuh pintu left (kiri)
    //4 --> butuh pintu right (kanan)

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1) //1 --> butuh spawn pintu bottom (bawah)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2) //2 --> butuh spawn pintu top (atas)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3) //3 --> butuh spawn pintu left (kiri)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4) //4 --> butuh spawn pintu right (kanan)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true; //akan stop
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            try
            {
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                spawned = true;
            }


            catch(System.Exception)
            {
                Destroy(gameObject);
            }
        }  
            


    }

}
