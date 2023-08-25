using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyCameraFit : MonoBehaviour
{
    Quaternion startRotation;
    private void Awake()
    {
        startRotation = transform.rotation;
    }
    void Start()
    {
    }

    void Update()
    {
        transform.rotation = startRotation;
    }
}
