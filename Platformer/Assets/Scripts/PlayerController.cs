using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxSpeed = 4.0f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 5.0f;
    private bool isJumping;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();

        Application.targetFrameRate = 144;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        
        if (Input.GetButton("Vertical") && !isJumping)
        {
            Jump();
        }
    }

    private void Run()
    {
        float direction = Input.GetAxis("Horizontal");
        
        Vector2 velocityRB = _rigidbody2D.velocity;
        velocityRB.x += direction * speed;
        velocityRB.x = Math.Abs(velocityRB.x) >= maxSpeed ? maxSpeed * direction : velocityRB.x;
        
        _rigidbody2D.velocity = velocityRB;
        
        _sprite.flipX = direction < 0.0f;
    }

    private void Jump()
    {
        Vector2 velocityRB = _rigidbody2D.velocity;
        velocityRB.y = jumpForce;
        
        _rigidbody2D.velocity = velocityRB;
        
        isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Teleport"))
        {
            Portal portal = col.GetComponent<Portal>();
            if (!portal.Active)
                return;
            
            portal.CharacterTeleported();
            transform.position = portal.GetPortalPosition();
            _rigidbody2D.velocity *= Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Teleport"))
        {
            Portal portal = other.GetComponent<Portal>();
            if (portal.Active)
                return;
            
            portal.CharacterExit();
        }
    }
}
