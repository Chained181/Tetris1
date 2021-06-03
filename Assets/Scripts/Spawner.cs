using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Qubes;
    // Start is called before the first frame update
    void Start()
    {
        NewQube();
    }

    public void NewQube()
    {
        Instantiate(Qubes[Random.Range(0, Qubes.Length)], transform.position, Quaternion.identity);
    }








}
