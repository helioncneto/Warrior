using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float waitTime = 2f;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;
    public float aimingTime = 0.5f;
    public float shootingTime = 0.5f;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private GameObject _target;
    private Weapon _weapon;
    private bool _isFaceRight = true;
    private bool _isAttacking = false;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start the routine
        if(transform.localScale.x > 0f)
        {
            _isFaceRight = true;
        } else if(transform.localScale.x < 0f)
        {
            _isFaceRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.right;
        if(_isFaceRight == false)
        {
            direction = Vector2.left;
        }

        if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        float horizontalSpeed = speed;

        if(_isFaceRight == false)
        {
            horizontalSpeed = horizontalSpeed * -1f;
        }

        if (_isAttacking)
        {
            horizontalSpeed = 0f;
        }

        _rigidBody.velocity = new Vector2(horizontalSpeed, _rigidBody.velocity.y);
    }

    void LateUpdate()
    {
        _animator.SetBool("idle", _rigidBody.velocity == Vector2.zero);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(_isAttacking == false && collision.CompareTag("Player"))
        {
            StartCoroutine(AimAndShoot());
        }
    }

    void Flip()
    {
        _isFaceRight = !_isFaceRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator AimAndShoot()
    {
        _isAttacking = true;

        yield return new WaitForSeconds(aimingTime);
        _animator.SetTrigger("shooting");
        yield return new WaitForSeconds(shootingTime);
        _isAttacking = false;

    }

    public void CanShoot()
    {
        if (_weapon != null)
        {
            _weapon.Shoot();
        }
    }

    void OnEnable()
    {
        _isAttacking = false;
    }

    void OnDisable()
    {
        StopCoroutine("AimAndShoot");
        _isAttacking = false;
    }
}
