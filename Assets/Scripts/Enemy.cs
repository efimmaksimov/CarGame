using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Player target;

    [SerializeField] private EnemyType type;

    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip attack;
    [Space]

    //public Player player;
    [SerializeField] private GameObject ragdollPrefab;
    [Header("Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private float cooldown;

    private bool mayAttack = true;
    private bool isFalling;
    private AudioSource audioSource;
    private int frameCounter = 0;
    private EnemyGenerator generator;
    private Vector3 lastPos;
    private Vector3 newPos;
    private float speed;
    public NavMeshAgent navMeshAgent;
    private Animator anim;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        generator = GetComponentInParent<EnemyGenerator>();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastPos = transform.position;
    }

    private void Update()
    {
        if (isFalling)
        {
            return;
        }
        frameCounter++;
        if (frameCounter > 1)
        {
            navMeshAgent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < navMeshAgent.stoppingDistance)
            {
                if (target.Speed < 3 && mayAttack) StartAttackAnimation();
            }
            frameCounter = 0;
        }

        newPos = transform.position;
        speed = Mathf.Abs(Vector3.Distance(newPos, lastPos)/Time.deltaTime);
        anim.SetFloat("MoveSpeed", speed);
        lastPos = newPos;
    }

    private void StartAttackAnimation()
    {
        transform.LookAt(target.transform.position);
        anim.SetTrigger("Attack");
        Attack();
    }

    public void Attack()
    {
        audioSource.pitch = 1 + Random.Range(-0.1f, 0.1f);
        audioSource.PlayOneShot(attack);
        target.TakeDamage(damage);
        mayAttack = false;
        StartCoroutine(CooldownAttack());
    }

    private IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(cooldown);
        mayAttack = true;
    }

    public void Fall()
    {
        anim.SetTrigger("Fall");
        isFalling = true;
    }

    public void Death()
    {
        Instantiate(ragdollPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
        generator.DeathEnemy();
        Messenger<EnemyType>.Broadcast(GameEvents.enemyDeath, type);
    }

    public void StandUp()
    {
        isFalling = false;
    }
}

public enum EnemyType
{
    Blue,
    Purple,
    Black
}
