using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorPlayer : MonoBehaviour
{

    Rigidbody2D rgb;
    private CharacterController controller;
    private const float Turn_Speed = 0.05f;
    private const float LANE_DISTANCE = 3.0f;
    private float jumpForce = 12.0f;
    private float verticalVelocity;
    private float gravity = 15.0f;
    private float speed = 12.0f;
    private float desirdLine = 1; // Linie lewo, prawo
    public bool isRunning = false;
    public bool IsDead { set; get; }

    // Animation
    private Animator anim;

    // Speed Modifier
    private float originalSpeed = 7.0f;
    private float speeds;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        speeds = originalSpeed;
        anim = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody2D>();
       
    }
    private void Update()

    {
        if (!isRunning)
            return;


        // speed modifier
        if ((Time.time - speedIncreaseLastTick) > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speeds += speedIncreaseAmount;
            GameManager.Instance.UpdateModifier (speeds - originalSpeed);
        }


        // Gdzie się znajdujemy
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLane(false);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveLane(true);

        //  Gdzie będziemy się znajdować
        Vector3 targetposition = transform.position.z * Vector3.forward;
        if (desirdLine == 0)
            targetposition += Vector3.left * LANE_DISTANCE;
        else if (desirdLine == 2)
            targetposition += Vector3.right * LANE_DISTANCE;

        // Wylczanie delty
        Vector3 movevector = Vector3.zero;
        movevector.x = (targetposition - transform.position).normalized.x * speed;

        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        // Skok i przyciąganie ziemskie
        if (IsGrounded())

        {
            verticalVelocity = -0.1f;
            if (Input.GetKeyDown(KeyCode.UpArrow)) /*(Input.GetKeyDown(KeyCode.UpArrow))*/ /*(SwipeController.Instance.SwipeUp) */
            {
                verticalVelocity = jumpForce;
                anim.SetTrigger("Jump");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                StartSliding();
               
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                verticalVelocity = -jumpForce;

            }

        }
        movevector.y = verticalVelocity;
        movevector.z = speed;

        // Poruszanie się
        controller.Move(movevector * Time.deltaTime);


    }

    private void MoveLane(bool goingRight)
    {
        if(!goingRight)
        {
            desirdLine--;
            if (desirdLine == -1)
                desirdLine = 0;
        }
        else
        {
            desirdLine++;
            if (desirdLine == 3)
                desirdLine = 2;
        }
    }
    private bool IsGrounded()
    {
        Ray groundray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);

        return Physics.Raycast(groundray, 0.2f + 0.1f);
    }
    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");

    }

    public void SetSpeed (float modifier)
    {
        originalSpeed = 12.0f + modifier;
    }
    private void Crash()
    {

        isRunning = false;
        anim.SetTrigger("Death");
        GameManager.Instance.OnDeath();

    }
/*
    private void Heart()
    {
        isRunning = true;
        anim.SetTrigger("Rest");
      
    }
    */

     private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height *= 0.5f;
        controller.center *= 0.5f;
        Invoke("StopSliding", 1f);
    }


    private void StopSliding()
    {
       controller.height *= 2;
        controller.center *= 2;
        anim.SetBool("Sliding", false);
    }
    

        private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "CrashObject":
                Crash();
                break;

        }
       
    }

}