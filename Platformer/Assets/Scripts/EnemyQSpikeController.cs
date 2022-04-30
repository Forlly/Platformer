using System.Collections;
using UnityEngine;
/// <summary>
/// \brief Класс динамечиского противника
/// </summary>
public class EnemyQSpikeController : MonoBehaviour, IEnemyController
{
    /// <summary>
    /// \param startingHP Количество жизней противника
    /// \param damage Урон который может нанести противник
    /// </summary>
    [SerializeField] private GameObject topEnemyPos;
    [SerializeField] private int startingHP = 2;
    [SerializeField] private int currentHP;
    public EnemyHPController enemyHpController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int damage = 2;
    public UnitStats UnitStats;
    private bool _takeDamage;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyHpController = GetComponent<EnemyHPController>();
        currentHP = startingHP;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (player.BottomPos.transform.position.y > GetTopPos().y)
            {
                ReceiveDamage(player);
                StartCoroutine(AfterDamage());
            }
            else
            {
                MakeDamage(player, damage);
                Debug.Log(player.BottomPos.transform.position.y );
                Debug.Log(topEnemyPos.transform.position.y);
            }
        }
    }
    
    public Vector2 GetTopPos()
    {
        return topEnemyPos.transform.position;
    }
/// <summary>
/// \brief Метод получения урона от игрока
/// </summary>
/// <param name="playerDamage"> Урон нанесенный игроком</param>
    public void ReceiveDamage( PlayerController player)
    {
        if (!_takeDamage)
        {
            _takeDamage = true;
            player.hitSound.Play();
            currentHP -= player.damage;
            if (currentHP <= 0)
                Destroy(gameObject);
            Debug.Log(startingHP);
            Debug.Log(currentHP);
            enemyHpController.UpdateSpriteHP(currentHP, startingHP);
        }
    }

/// <summary>
/// \brief Метод проверки дистанции между игроком и противником
/// </summary>
/// <param name="player"> Позиция игрока</param>
/// <returns></returns>
    public Vector2 CheckDistanceToPlayer(Transform player)
    {
        Vector2 distance = new Vector2();
        distance = player.position - transform.position;
        return distance;
    }
/// <summary>
/// \brief Метод следования за игроком
/// </summary>
/// <param name="player"> Позиция игрока</param>
    public void FollowToPlayer(Transform player)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, UnitStats.speed * Time.deltaTime);

    }

/// <summary>
/// \brief Метод нанесения урона
/// </summary>
/// <param name="player"> Персонаж, которому будет нанесен урон</param>
/// <param name="_damage"> Количество урона, которое будет нанесено</param>
    public void MakeDamage(PlayerController player,int _damage)
    {
        player.ReceiveDamageFromEnemy(_damage);
        player.healthController.UpdateCurrentHealthbar(player.currentHealth, player.startingHealth);
    }
    
    private IEnumerator AfterDamage()
    {
        float step_sec = 0.1f;
        for (float i = 0; i < 1; i += step_sec)
        {
            _spriteRenderer.color = new Color(1,0.3f,0.3f,0.8f);
            yield return new WaitForSeconds(step_sec/2);
            _spriteRenderer.color = new Color(1,0.3f,0.3f,0.9f);
            yield return new WaitForSeconds(step_sec/2);
        }
        _spriteRenderer.color = new Color(1,1,1,1);
        _takeDamage = false;
    }
}
