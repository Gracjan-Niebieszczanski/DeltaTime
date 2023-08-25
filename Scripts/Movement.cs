using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float Acceleration;
    public float speedMultiplayer = 1;
    Rigidbody rb;
    SpriteRenderer Renderer;
    int animationState = 1;
    bool animationLoopActive;
    public Sprite idle;
    public Sprite moving1;
    public Sprite moving2;
    public float animationTimeStamp = 0.6f;
    public Vector3 playerPower
    {
        get { return new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical")); }
    }
    public Vector3 playerPowerRaw
    {
        get { return new Vector3(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical")); }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        Renderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        List<float> slowDonws = Manager.PlayerComponent.slowDonws;
        
        if (slowDonws.Count > 0)
        {
            speedMultiplayer = slowDonws.OrderByDescending(e => e).First();
        }
        else
        {
            speedMultiplayer = 1;
        }
        Vector3 targetVelocity = 
            new Vector3(-Input.GetAxis("Horizontal") ,0,-Input.GetAxis("Vertical"));

        if(TutorialManager.get != null && TutorialManager.get.tutorialIndex == 6 && targetVelocity != Vector3.zero)
        {
            TutorialManager.get.changeTutorialStep();
        }

        /*
        float forceX = (targetVelocity.x / (1 - Time.fixedDeltaTime * rb.drag)) / (Time.fixedDeltaTime * rb.mass);
        float forceZ = (targetVelocity.z / (1 - Time.fixedDeltaTime * rb.drag)) / (Time.fixedDeltaTime * rb.mass);
        rb.AddForce(new Vector3(forceX, 0, forceZ));
        */
        //rb.AddForce(Vector3.ClampMagnitude((targetVelocity - rb.velocity) / Time.fixedDeltaTime, maxAcceleration), ForceMode.Force);
        float realAcceleration = Acceleration;
        if (Time.timeScale < 1)
            realAcceleration /= Time.timeScale;
        rb.AddForce(targetVelocity.normalized * realAcceleration * Time.deltaTime / Time.timeScale / Time.timeScale, ForceMode.Force);
        if (targetVelocity.magnitude != 0 && !animationLoopActive)
        {
            StartCoroutine(changeAnimation());
        }
        SpeedControl();
    }
    void SpeedControl()
    {
        Vector3 groundVelocity = rb.velocity;
        groundVelocity.y = 0;
        float realSpeed = speed * speedMultiplayer;
        if(Manager.PlayerComponent.unlimitedSpeed)
            realSpeed = Manager.PlayerComponent.speedLimit;
        //realSpeed *= speedMultiplayer;
        if(Time.timeScale < 1)
            realSpeed /= Time.timeScale;
        
        if (groundVelocity.magnitude > realSpeed)
        {
            Vector3 limitSpeed = groundVelocity.normalized * realSpeed;
            rb.velocity = new Vector3(limitSpeed.x, rb.velocity.y, limitSpeed.z);
        }
        
    }
    IEnumerator changeAnimation()
    {
        if (playerPower.magnitude != 0)
        {
            if (animationState == 0)
            {
                Renderer.sprite = moving1;
                animationState = 1;
            }
            else
            {
                Renderer.sprite = moving2;
                animationState = 0;

            }
            animationLoopActive = true;
            float multiplayer = 1;
            if(Time.timeScale < 1)
            {
                multiplayer *= Time.timeScale;
            }
            yield return new WaitForSeconds(animationTimeStamp * multiplayer);
            StartCoroutine(changeAnimation());
        }
        else
        {
            Renderer.sprite = idle;
            animationLoopActive = false;
        }
    }
}
