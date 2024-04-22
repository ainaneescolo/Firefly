using System;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    [Header("---- GENERAL ATTRIBUTES ----")]
    public bool pure;
    public float speed;
        
    [Space]
    [Header("--- PLAYER LAYER MASK ----")] 
    [SerializeField, Tooltip("Layer mask que tendrá que tener el player para poder detectarlo si se acerca" +
                             " lo suficiente al enemigo")]
    public LayerMask playerMask;
    
    [Space]
    [Header("---- RANGES ----")]
    [SerializeField, Tooltip("Rango de visión del enemigo")] 
    public float viewRange;
    [SerializeField, Tooltip("Rango de ataque del enemigo")] 
    public float attackRange;

    [Space] [Header("---- WAYPOINTS ----")] 
    [SerializeField, Tooltip("Array de waypoints que el personaje seguirá")]
    private Vector3[] waypointsArray;
    
    [Header("---- MOVEMENT ----")]
    private int currentIndex; // Index para determinar el waypoint a devolver de la array de Waypoints
    private Vector3 targetWaypoint;  // Waypoint al que se dirige el personaje

    // Private variables
    private GameObject player;
    private Animator _animator;
    public bool playerInViewRange;
    public bool playerInAttackRange;

    public GameObject Player => player;
    public Animator Animator => _animator;

    public bool PlayerAttack;
    private bool firstwayppoint;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeToPure();
    }

    public void InitializeWaypoint()
    {
        if (waypointsArray == null || waypointsArray.Length <= 0)
        {
            targetWaypoint = transform.position;
        }
        else
        {
            currentIndex = 0;
            targetWaypoint = waypointsArray[currentIndex];
        }
    }

    public void Patrolling()
    {
        if (Vector2.Distance(transform.position, targetWaypoint) < 0.5f)
        {
            targetWaypoint = GetNextWayPoint();
            firstwayppoint = !firstwayppoint;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
        transform.rotation = (transform.position.x - targetWaypoint.x) > 0? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
    }

    private Vector3 GetNextWayPoint()
    {
        //currentIndex = (currentIndex + 1) % waypointsArray.Length;

        if (currentIndex + 1 > waypointsArray.Length -1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        
        return waypointsArray[currentIndex];
    }
    
    public void MakeEnemyPure()
    {
        pure = true;
        LevelManager.instance.EnemiesLeftToDefeat();
        ChangeToPure();
    }

    public void ChangeToPure()
    {
        if (!pure) return;
        _animator.SetBool("pure", true);
        gameObject.layer = LayerMask.NameToLayer("Background");
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
