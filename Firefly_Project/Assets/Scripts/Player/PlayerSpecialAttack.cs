using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpecialAttack : MonoBehaviour
{
    [Header("---- SPECIAL ATTACK ATTRIBUTES ----")] 
    [SerializeField, Tooltip("Int of the number of lives the player has")]
    private int attackEnergy;

    [SerializeField, Tooltip("Rango de ataque del enemigo")] 
    public float attackRange;
    public LayerMask enemyMask;
    public LayerMask floorMask;
    [SerializeField] private Animator Animator;
    [SerializeField] private Button specialAttackButton;
    [SerializeField] private RectTransform specialAttackImage;

    [SerializeField] private GameObject jumpZone;
    
    private Rigidbody2D playerParent;
    private PlayerController playerController;
    private Enemy_Base enemy;
    private Vector2 direction;

    public bool takeDamage;
    
    private void Start()
    {
        playerParent = transform.parent.GetComponent<Rigidbody2D>();
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    #region Special Attack
    
    public void SpecialAttackPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, attackRange, enemyMask) != null)
        {
            foreach (var enemy in Physics2D.OverlapCircle(transform.position, attackRange, enemyMask).GetComponents<Enemy_Base>())
            {
                enemy.MakeEnemyPure();
            }
        }
        
        if (Physics2D.OverlapCircleAll(transform.position, attackRange, floorMask) != null)
        {
            foreach (var floor in Physics2D.OverlapCircleAll(transform.position, attackRange, floorMask))
            {
                floor.GetComponent<FloorNode>().MakeFloorPure();
            }
        }
        
        attackEnergy = 130;
        specialAttackImage.anchoredPosition = new Vector2(0, -100);
        specialAttackButton.interactable = false;
        Animator.SetTrigger("attack");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<FloorNode>() && attackEnergy > -30)
        {
            if (!col.GetComponent<FloorNode>().pure) return;
            --attackEnergy;
            specialAttackImage.anchoredPosition = new Vector2(0, attackEnergy * -1);
            if (attackEnergy != -30) return;
            specialAttackButton.interactable = true;
        }
    }
    
    #endregion

    #region Purify

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "AttackZone")
        {
            col.transform.parent.GetComponent<Enemy_Base>().MakeEnemyPure();
        }
        else if (col.GetComponent<Enemy_Base>())
        {
            if (col.GetComponent<Enemy_Base>().pure) return;
            enemy = col.GetComponent<Enemy_Base>();

            DamagePlayer(col.gameObject);
        }
    }

    public void DamagePlayer(GameObject col)
    {
        direction = (col.transform.position - transform.position).x < 0? Vector2.right : Vector2.left;
        StartCoroutine("TakeDamage");
    }
    
    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.2f);
        if (enemy.pure || playerController.PlayerLives() || takeDamage) yield break;

        enemy.PlayerAttack = true;
        takeDamage = true;
        playerController.enabled = false;
        playerParent.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        playerParent.AddForce(direction * 5, ForceMode2D.Impulse);
        Animator.SetTrigger("Hit");
        jumpZone.SetActive(false);

        yield return new WaitForSeconds(1f);
        playerController.TakeDamagePlayer();
        playerController.enabled = true;
        takeDamage = false;
        enemy.PlayerAttack = false;
        jumpZone.SetActive(true);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
