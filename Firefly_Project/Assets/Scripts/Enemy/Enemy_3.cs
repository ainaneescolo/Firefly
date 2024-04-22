using UnityEngine;

public class Enemy_3 : Enemy_Base
{
    void Start()
    {
        InitializeWaypoint();
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
                ChasePlayer();
                Animator.SetBool("walk", true);
            }
            else if (playerInViewRange && playerInAttackRange)
            {
                Animator.SetBool("walk", false);
            }
        }
    }
    
    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.localPosition, Player.transform.position, speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        transform.rotation = (transform.position.x - Player.transform.position.x) > 0? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
    }

    public void MakeDamage()
    {
        if (!playerInAttackRange || pure) return;
        Player.transform.GetChild(1).GetComponent<PlayerSpecialAttack>().DamagePlayer(gameObject);
    }
}
