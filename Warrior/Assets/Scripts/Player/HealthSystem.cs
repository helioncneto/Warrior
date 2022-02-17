using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float totalHealth = 3f;
    private float health;
    private SpriteRenderer _spriteRender;

    //Health
    public RectTransform HearthMenu;
    public RectTransform gameOverMenu;
    private float _heartSize = 16f;
    public AudioClip shot;

    private Animator _animator;
    private AudioSource _audioSource;
    private PlayerController _playerControler;
    public GameObject horde;
    public GameObject levelSound;

    private Retry _retry;


    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _playerControler = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _retry = GetComponent<Retry>();
        _audioSource = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }

    void AddDamage(float damage)
    {
        health = health - damage;
        HearthMenu.sizeDelta = new Vector2(health * _heartSize, _heartSize);
        _audioSource.clip = shot;
        _audioSource.Play();

        if(health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);

        }

        if (this.enabled == true)
        {
            StartCoroutine("VisualDamage");
        }
        
        Debug.Log("You Got Damage: " + health);
    }

    void AddHealth(float amountHealth)
    {
        health = health + amountHealth;

        if (health > totalHealth)
        {
            health = totalHealth;
        }
        HearthMenu.sizeDelta = new Vector2(health * _heartSize, _heartSize);
        Debug.Log("You Got Health: " + health);
    }

    IEnumerator VisualDamage()
    {
        _spriteRender.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRender.color = Color.white;
    }

    void OnEnable()
    {
        health = totalHealth;
        HearthMenu.sizeDelta = new Vector2(health * _heartSize, _heartSize);
        _retry.RestartPlay();
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }

    void OnDisable()
    {
        health = 0;
        if(gameOverMenu != null)
        {
            gameOverMenu.gameObject.SetActive(true);
        }
        
        _animator.enabled = false;
        _playerControler.enabled = false;
        horde.SetActive(false);
        HearthMenu.gameObject.SetActive(false);
        levelSound.SetActive(false);
    }
}
