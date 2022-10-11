using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleLogic : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovment player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerMovment player = GetComponent<PlayerMovment>();
    }

    void Update()
    {
        animator.SetBool("Apple", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("ApplyDamage", 10);
            Destroy(gameObject);
            animator.SetBool("Collected", true);
        }
    }
}