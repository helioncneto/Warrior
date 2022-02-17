using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float lifeTime = 2f;
    public Color initialColor = Color.white;
    public Color finalColor;
    public float damage = 0.5f;
    public GameObject explosion;

    private SpriteRenderer _render;
    private float _startTime;
    private Rigidbody2D _rigidBody;
    private bool _isReturning = false;

   private void Awake()
    {
        _render = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        //transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
        //transform.Translate(movement);

        //Change Color
        float _currentTime = Time.time - _startTime;
        float _percentFinish = _currentTime / lifeTime;
        _render.color = Color.Lerp(initialColor, finalColor, _percentFinish);

    }

    void FixedUpdate()
    {
        // Movement with RigidBody2D
        Vector2 movement = direction.normalized * speed;
        _rigidBody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 explosionPosition = new Vector3(collision.transform.position.x, collision.transform.position.y, -0.1f);
            GameObject explosionParticle = Instantiate(explosion, explosionPosition, Quaternion.identity, collision.gameObject.transform) as GameObject;
            if (collision.transform.gameObject.activeSelf)
            {
                collision.SendMessageUpwards("AddDamage", damage);
            }
            Destroy(gameObject);
            Destroy(explosionParticle, 1f);
        }

        if (collision.CompareTag("Enemy") && _isReturning == true)
        {
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(gameObject);
        }
    }

    public void AddDamage()
    {
        _isReturning = true;
        direction = direction * -1f;
    }
}
