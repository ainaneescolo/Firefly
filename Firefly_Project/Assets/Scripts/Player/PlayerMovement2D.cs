using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [Header("----  ATTRIBUTES ----")] 

    
    [Header("---- MOVEMENT ATTRIBUTES ----")] 
    [SerializeField] private Joystick _joystick;    
    
    [SerializeField, Tooltip("Velocidad del jugador")]
    private float speed;
    [SerializeField, Tooltip("Velocidad del jugador en el aire")]
    private float speedInAir;
    [SerializeField, Tooltip("Fuerza de salto")]
    private float jumpForce;
    
    [Space]
    [Header("--- FOOT ----")]
    [SerializeField, Tooltip("Transform que representa la posición de los pies del jugador")]
    private Transform foot;
    [SerializeField, Tooltip("Radio de los pies para determinar si el player está tocando el suelo o no")]
    private float radius;
        
    [SerializeField, Tooltip("Layer mask para determinar la colisión contra el suelo")]
    private LayerMask layerMaskFloor;

    // Private variables //
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool canJump;
    private bool doubleJump;

    public bool death;

    public Animator Animator => _animator;
    
    void Start()
    {
        // Obtenemos los componentes del player
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    #region Movement & Jump

    void Update()
    {
        if (death) return;
        canJump = Physics2D.OverlapCircle(foot.transform.position, radius, layerMaskFloor);
        
        //############# CONTROL IDLE ################
        
        // Ni no nos estamos moviendo lo indicamos en el animator para representar las animacioenes de Idle
        if (_joystick.Horizontal == 0)
        {
            _animator.SetBool("walk", false);
            return;
        }
        else
        {
            _animator.SetBool("walk", true);
        }
        
        //############# CONTROL ORIENTACIÓN PLAYER ################

        // Miramos el valor de la X para saber en que dirección tiene que apuntar el jugador
        if (_joystick.Horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {            
            // Flipeamos al personaje para que mire a la izquierda
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    
    void FixedUpdate()
    {
        if (death) return;
        if (canJump)
        {
            _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * speed, _rigidbody2D.velocity.y);
            _animator.SetBool("floor", true);
            _animator.SetBool("doublejump", false);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * speedInAir, _rigidbody2D.velocity.y);
            _animator.SetBool("floor", false);
        }
    }

    public void Jump()
    {
        if (death) return;
        switch (canJump)
        {
            case true:
                _animator.SetTrigger("jump");
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = true;
                break;
            case false:
                if (!doubleJump)return;
                _animator.SetBool("doublejump", true);
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
                break;
        }
    }
    
    #endregion
}