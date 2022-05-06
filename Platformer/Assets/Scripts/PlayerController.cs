using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// \brief Класс контроллирующий поведение персонажа
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// \param speed Скорость 
    /// \param maxSpeed Максимальная скорость
    /// \param startingHealth Стартовое здоровье
    /// \param jumpForce Сила прыжка
    /// \param _activeMovement Активация передвижения персонажа
    /// </summary>
    [Header("Settings")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxSpeed = 4.0f;
    [SerializeField] public int startingHealth = 5;
    [SerializeField] public AudioSource hitSound;
    public int currentHealth;
    public int damage = 1;
    private bool _takesDamage = false;
    public ScoreSystem scoreSystem;
    public HealthController healthController;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] public GameObject BottomPos;
    public float direction;

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
        healthController.UpdateTotalHealthbar(currentHealth, startingHealth);
        scoreSystem = FindObjectOfType<ScoreSystem>();
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
    /// <summary>
    /// \brief Метод передвижения персонажа по горизонтали (бег)
    /// </summary>
    /// <returns></returns>
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
            direction = Input.GetAxis("Horizontal");

            Vector2 velocityRB = _rigidbody2D.velocity;
            velocityRB.x += direction * speed;
            velocityRB.x = Math.Abs(velocityRB.x) >= maxSpeed ? maxSpeed * direction : velocityRB.x;

            _rigidbody2D.velocity = velocityRB;

            transform.rotation = direction < 0.0f ? Quaternion.Euler(0,180,0) : Quaternion.Euler(0,0,0);

            animatorHelper.SetAnimationState("Run", true);
            
            if (!ActiveMovement)
                yield break;
            
            yield return new WaitForFixedUpdate();
        }
        
        animatorHelper.SetAnimationState("Run", false);

        StartCoroutine(Run());
    }
    
    public int GetDirection()
    {
        return direction >= 0 ? 1 : -1;
    }
    /// <summary>
    /// \brief Метод отвечающий за прыжок
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// \brief Метод получения урона от противника
    /// </summary>
    /// <param name="damage"> Урон нанесенный противником</param>
    public void ReceiveDamageFromEnemy(int damage)
    {
        if (!_takesDamage)
        {
            _takesDamage = true;
            currentHealth -= damage;
            if (currentHealth <= 0)
                Destroy(gameObject);
            StartCoroutine(AfterDamage());
            scoreSystem.DeductPoints(5);
        }
    }

    private IEnumerator AfterDamage()
    {
        float step_sec = 0.1f;
        for (float i = 0; i < 1.2f; i += step_sec)
        {
            _sprite.color = new Color(1,1,1,0.3f);
            yield return new WaitForSeconds(step_sec/2);
            _sprite.color = new Color(1,1,1,0.6f);
            yield return new WaitForSeconds(step_sec/2);
        }
        _sprite.color = new Color(1,1,1,1);
        _takesDamage = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemy"))
            && col.otherCollider.gameObject.name == "Foots")
        {
            animatorHelper.SetAnimationState("Jump", false);
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
        else if (col.CompareTag("CheackPoint"))
        {
            CheckPoint checkPoint = col.GetComponent<CheckPoint>();
            checkPoint.SetPlayerPosition();
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


    public void PlaySoundAfterMakeDamage(int points)
    {
        hitSound.Play();
        scoreSystem.AddPoints(points);
    }
}