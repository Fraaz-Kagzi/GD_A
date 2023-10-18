using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public float movementSpeed = 3;
    [HideInInspector] public Vector3 dir;
    float hInput, vInput;
    CharacterController controller;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 playerpos;
    [SerializeField] float gravity= -9.81f;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();


    }
    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        // Check if the character is moving (you can adapt this based on your movement system).
        bool isMoving = rb.velocity.magnitude > 0.1f;

        // Set the "IsRunning" parameter in the Animator.
        animator.SetBool("isRunning", isMoving);
    }
    void GetDirectionAndMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        dir = transform.forward * vInput + transform.right * hInput;
        controller.Move(dir * movementSpeed * Time.deltaTime);
    }
    bool isGrounded()
    {
        playerpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Physics.CheckSphere(playerpos, controller.radius - 0.05f, groundMask)) return true;
        return false;
           
    }
    void Gravity()
    {
        if (!isGrounded()) { velocity.y += gravity * Time.deltaTime; }
        else if (velocity.y<0) { velocity.y = -2; }

        controller.Move(velocity * Time.deltaTime);
    }
}
