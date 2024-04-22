using UnityEngine;

public class Enemy_1 : Enemy_Base
{
    [Header("---- IN ZONE ATTRIBUTES ----")]
    [SerializeField] private int forceMagnitude;
    private Rigidbody2D _rigidbodyPlayer;
    private PlayerSpecialAttack playerAttack;
    
    void Start()
    {
        InitializeWaypoint();
        _rigidbodyPlayer = Player.GetComponent<Rigidbody2D>();
        playerAttack = FindObjectOfType<PlayerSpecialAttack>();
    }

    void Update()
    {
        if (pure)
        {
            Patrolling();
        }
        else
        {
            playerInViewRange = Physics2D.OverlapCircle(transform.position, viewRange, playerMask);
            playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerMask);

            if (!playerInViewRange && !playerInAttackRange)
            {
                Patrolling();
                Animator.SetBool("walk", true);
            }
            else if (playerInViewRange && !playerInAttackRange)
            {
                Animator.SetBool("walk", false);
                if (playerAttack.takeDamage) return;
                AttractPlayer(forceMagnitude);
            }
            else if (playerInViewRange && playerInAttackRange)
            {
                if (playerAttack.takeDamage) return;
                AttractPlayer(forceMagnitude*0.5f);
            } 
        }
    }

    private void AttractPlayer(float force)
    {
        Vector3 direction = transform.position - Player.transform.position;
        direction.Normalize();
        _rigidbodyPlayer.AddForce(direction * force);
    }
}
