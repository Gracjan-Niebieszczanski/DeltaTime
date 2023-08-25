using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtTime : MonoBehaviour
{
    public float time;
    void Start()
    {
        Invoke("End", time);
    }


    void End()
    {
        Destroy(gameObject);
    }
}
