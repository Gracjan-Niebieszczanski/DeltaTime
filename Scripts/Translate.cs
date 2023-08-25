using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    public Vector3 speed;
    public bool useRotation;
    public bool UseRigidbody;
    public bool BalanceSpeed;
    public bool performanceMode;
    public bool allowStopping = false;
    SpriteRenderer spriteRenderer;
    Rigidbody rb;
    private void Start()
    {
        TryGetComponent(out rb);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
            Move();
    }
    private void Move()
    {
        bool active = true;
        if (spriteRenderer != null && spriteRenderer.isVisible == false && allowStopping)
        {
            active = false;
        }
        if (active == true)
        {
            if (UseRigidbody && rb != null)
            {
                if (BalanceSpeed)
                {
                    rb.velocity = Manager.balanceSpeed(speed, rb);
                }
                else
                {
                    if (useRotation)
                    {
                        rb.velocity = speed.x * transform.up;
                        rb.velocity += speed.y * transform.right;
                        rb.velocity += speed.z * transform.forward;
                    }
                    else
                    {
                        rb.velocity = speed;
                    }

                }
            }
            else
            {
                if (useRotation)
                {
                    transform.Translate(speed * Time.deltaTime);
                }
                else
                {
                    transform.position += speed * Time.deltaTime;
                }
            }
        }
    }
}
