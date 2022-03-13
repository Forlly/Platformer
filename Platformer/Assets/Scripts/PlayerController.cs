using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 5.0f;
    private bool isJumping = false;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButton("Vertical") && !isJumping)
        {
            Jump();
            isJumping = true;
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        _sprite.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isJumping = false;
    }

}
