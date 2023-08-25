using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject spawn;
    public Vector3 offset;
    private void OnCollisionEnter(Collision collision)
    {
        if (spawn == null) return;
        Instantiate(spawn, transform.position + offset, spawn.transform.rotation);
        Destroy(gameObject);
    }
}
