using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxSpeed = 4.0f;
    [SerializeField] public int lives = 5;
    [SerializeField] private float jumpForce = 5.0f;

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
        }

        else if (col.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
            Vector2 offset = col.gameObject.GetComponent<Collider2D>().bounds.size;
            if (transform.position.y > enemy.transform.position.y)
            {
                Destroy(enemy.gameObject);
                Debug.Log( enemy.transform.position.y + offset.y);
                Debug.Log( transform.position.y);
            }
            else
            {
                enemy.GetDamageEnemy();
                Debug.Log( enemy.transform.position.y);
                Debug.Log( transform.position.y);
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