using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxSpeed = 4.0f;
    [SerializeField] public int startingHealth = 5;
    public int currentHealth;
    private HealthController healthController;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField]private GameObject BottomPos;
    
    [Header("REFs")][Space]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private CharacterAnimatorController animatorHelper;
    public static PlayerController Instance { get; set; }

    private bool _activeMovement;
    public bool ActiveMovement
    {
        set
        {
            _activeMovement = value;
            if (_activeMovement)
            {
                StartCoroutine(Run());
                StartCoroutine(Jump());
            }
            else
            {
                animatorHelper.ForceActiveAnimationState("Idle");
            }
        }
        get => _activeMovement;
    }

    private void Awake()
    {
        Instance = this;
        currentHealth = startingHealth;
        healthController = FindObjectOfType<HealthController>();
        healthController.UpdateTotalHealthbar(currentHealth);
    }
    

    private bool isJumping;

    private void OnEnable()
    {
        ActiveMovement = true;
    }
    
    private void OnDisable()
    {
        ActiveMovement = false;
    }

    private IEnumerator Run()
    {
        while (!Input.GetButton("Horizontal"))
        {
            if (!ActiveMovement)
                yield break;
            
            yield return null;
        }
        
        while (Input.GetButton("Horizontal"))
        {
            float direction = Input.GetAxis("Horizontal");

            Vector2 velocityRB = _rigidbody2D.velocity;
            velocityRB.x += direction * speed;
            velocityRB.x = Math.Abs(velocityRB.x) >= maxSpeed ? maxSpeed * direction : velocityRB.x;

            _rigidbody2D.velocity = velocityRB;

            _sprite.flipX = direction < 0.0f;

            animatorHelper.SetAnimationState("Run", true);
            
            if (!ActiveMovement)
                yield break;
            
            yield return new WaitForFixedUpdate();
        }
        
        animatorHelper.SetAnimationState("Run", false);

        StartCoroutine(Run());
    }

    private IEnumerator Jump()
    {
        while (!Input.GetButton("Vertical") || isJumping)
        {
            if (!ActiveMovement)
                yield break;
            
            yield return null;
        }
        
        Vector2 velocityRB = _rigidbody2D.velocity;
        velocityRB.y = jumpForce;

        _rigidbody2D.velocity = velocityRB;
        
        animatorHelper.SetAnimationState("Jump", true);

        isJumping = true;
        
        StartCoroutine(Jump());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            animatorHelper.SetAnimationState("Jump", false);
            isJumping = false;
        }

        else if (col.gameObject.CompareTag("Spike"))
        {
            EnemyController spike = col.gameObject.GetComponent<EnemyController>();
            spike.GetDamageSpike();
            healthController.UpdateCurrentHealthbar(currentHealth);
        }

        else if (col.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
            if (BottomPos.transform.position.y > enemy.GetTopPos().y)
            {
                Destroy(enemy.gameObject);
            }
            else
            {
                enemy.GetDamageEnemy();
                healthController.UpdateCurrentHealthbar(currentHealth);
            }
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