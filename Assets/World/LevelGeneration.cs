using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //array berisi game object
    public GameObject[] objects;

    void Start()
    {
        //random object yang akan diinstantiate(dimunculkan)
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, Quaternion.identity);
        
    }

}
