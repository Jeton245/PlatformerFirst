using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Animator animator;
    
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;



    public float wallJumpCoolDown;
    public float speed = 10.0f;
    public float jumpHeigth = 5.0f;
    public bool groundCheck;
    

    private void Awake()
    {
         rigidbody2D = GetComponent<Rigidbody2D>();
         animator = GetComponent<Animator>();
         boxCollider2D = GetComponent<BoxCollider2D>();
        
         
    }

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

      
        if (inputHorizontal > 0.01f)
        {
            transform.localScale = Vector3.one;
            
        }
        else if (inputHorizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            
        }
      
        

        animator.SetBool("Running", inputHorizontal !=0);
        animator.SetBool("Jumping", isGrounded());

        if (wallJumpCoolDown < 0.2f)
        {

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                Jump();
            }

            rigidbody2D.velocity = new Vector2(inputHorizontal * speed, rigidbody2D.velocity.y); 

            if(isWall() && !isGrounded())
            {
                rigidbody2D.gravityScale = 0;
                rigidbody2D.velocity = Vector2.zero;
                animator.SetBool("Wall",true);
               
            }
            else
            {
                rigidbody2D.gravityScale = 1;
                animator.SetBool("Wall", false);
            }
           
        }
    }

    private void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeigth);
        animator.SetTrigger("Jump");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {      
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down,0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool isWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }


  
}
