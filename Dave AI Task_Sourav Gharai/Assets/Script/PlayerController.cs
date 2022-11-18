using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public Camera followCamera;
    private Animator playerAnim;

    public float speed = 2f;
    public float rotationSpeed = 10f;
    public float gravity = -7.5f;
    private Vector3 velocity;


    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Gravity();
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            cc.Move(direction * speed * Time.deltaTime);
        }

        if (direction != Vector3.zero)
        {
            Quaternion _rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, rotationSpeed * Time.deltaTime);
            playerAnim.SetBool("isWalk", true);
        }
        else
        {
            playerAnim.SetBool("isWalk", false);
        }
    }

    private void Gravity()
    {
        if (!cc.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            cc.Move(velocity);
        }
    }
}
