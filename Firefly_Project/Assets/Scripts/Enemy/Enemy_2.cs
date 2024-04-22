using UnityEngine;

public class Enemy_2 : Enemy_Base
{
    private float timer;

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
            playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerMask);
            playerInViewRange = Physics2D.OverlapCircle(transform.position, viewRange, playerMask);
            
            if (!playerInViewRange && !playerInAttackRange)
            {
                Patrolling();
            }
            else if (playerInViewRange && !playerInAttackRange && !PlayerAttack)
            {
                ChasePlayer(speed);
            }
            else if (playerInViewRange && playerInAttackRange && !PlayerAttack)
            {
                ChasePlayer(speed*0.5f);
            }
        }
    }
    
    private void ChasePlayer(float speedTMP)
    {
        transform.position = Vector2.MoveTowards(transform.localPosition, Player.transform.position, speedTMP * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        transform.rotation = (transform.position.x - Player.transform.position.x) > 0? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
    }
}
