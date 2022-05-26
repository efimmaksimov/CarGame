using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Player target;

    [SerializeField] private EnemyType type;

    [Header("Sound")]
    [Range(-3f, 3f)]
    [SerializeField] private float pitch;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip damageClip;
    [Space]

    //public Player player;
    [SerializeField] private GameObject ragdollPrefab;
    [Header("Stats")]
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;

    private bool mayAttack = true;
    private bool isFalling;
    private int frameCounter = 0;
    private EnemyGenerator generator;
    private Vector3 lastPos;
    private Vector3 newPos;
    private float speed;
    public NavMeshAgent navMeshAgent;
    private Animator anim;
    
    private void Awake()
    {
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
        if (frameCounter > 3)
        {
            navMeshAgent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < navMeshAgent.stoppingDistance)
            {
                if (target.Speed < 12 && mayAttack) StartAttackAnimation();
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
        AudioManager.Instance.PlaySound(attackClip, pitch, (SoundChanel)type);
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
        AudioManager.Instance.PlaySound(damageClip, pitch, (SoundChanel)type);
        isFalling = true;
    }

    public void Death()
    {
        Ragdoll ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation).GetComponent<Ragdoll>();
        ragdoll.Initialize(deathClip, type, pitch);
        gameObject.SetActive(false);
        Messenger<EnemyType>.Broadcast(GameEvents.enemyDeath, type);
        generator.DeathEnemy(this);
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
