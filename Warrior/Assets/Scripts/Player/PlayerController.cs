using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Movements
    public float speed = 2f;
    private Rigidbody2D _rigidBody;
    private Vector2 _movement;
    private Animator _animator;
    private bool _faceRight = true;

    // Jump
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    private bool _isGrounded;
    public float jumpForce = 5f;

    // Long Idle
    public float logIndleTime = 5f;
    private float _currentTime;
    
    // Attack
    private bool _isAttacking;

    // Sounds
    private AudioSource _audioSource;
    public AudioClip jumpAudio;
    public AudioClip attackAudio;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isAttacking == false)
        {
            float HorizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(HorizontalInput, 0f);

            if (HorizontalInput < 0f && _faceRight == true)
            {
                Flip();
            }
            else if (HorizontalInput > 0f && _faceRight == false)
            {
                Flip();
            }
        }

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false)
        {
            _audioSource.clip = jumpAudio;
            _audioSource.volume = 0.5f;
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _audioSource.Play();
        }

        if(Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false)
        {
            _audioSource.clip = attackAudio;
            _audioSource.volume = 1f;
            _movement = Vector2.zero;
            _rigidBody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
            _audioSource.Play();

        }
       
    }

    void FixedUpdate()
    {
        if(_isAttacking == false)
        {
            float horizontaVelocity = _movement.normalized.x * speed;
            _rigidBody.velocity = new Vector2(horizontaVelocity, _rigidBody.velocity.y);
        }
        
        
    }

    void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidBody.velocity.y);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            _currentTime = _currentTime + Time.deltaTime;
            if(_currentTime > logIndleTime)
            {
                _animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            _currentTime = 0f;
        }
    }

    void Flip()
    {
        _faceRight = !_faceRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
