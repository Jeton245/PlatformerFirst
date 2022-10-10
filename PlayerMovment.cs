using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Animator animator;
   



    public float speed = 10.0f;
    public float jumpHeigth = 5.0f;
    public bool groundCheck;
    

    private void Awake()
    {
         rigidbody2D = GetComponent<Rigidbody2D>();
         animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

        rigidbody2D.velocity = new Vector2(inputHorizontal * speed , rigidbody2D.velocity.y);
        if (inputHorizontal > 0.01f)
        {
            transform.localScale = Vector3.one;
            
        }
        else if (inputHorizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            
        }
      
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            Jump(); 
        }

        animator.SetBool("Running", inputHorizontal !=0);
        animator.SetBool("Jumping", groundCheck);
    }

    private void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeigth);
        animator.SetTrigger("Jump");
        groundCheck = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            groundCheck = true;
        }
    }
}
