using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    private bool _isAttacking = false;
    private Animator _animator;
    public GameObject score;
    private int _myScore = 0;
    private GameObject _enemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking == true)
        {
            //Debug.Log(collision.gameObject.layer);
            if (collision.CompareTag("Enemy") || collision.CompareTag("Bullet"))
            {
                collision.SendMessageUpwards("AddDamage");
                if (collision.CompareTag("Enemy"))
                {
                    //ToDo: Por essa parte do Script para um sistema de Score
                    int _enemyScore = collision.transform.gameObject.GetComponent<EnemyScore>().GetScore();
                    _myScore = _myScore + _enemyScore;
                    Debug.Log(_myScore);
                    Text scoreText = score.GetComponent<Text>();
                    scoreText.text = _myScore.ToString();
                }
            }
            
        }

    }

    private void OnEnable()
    {
        _myScore = 0;
        Text scoreText = score.GetComponent<Text>();
        scoreText.text = _myScore.ToString();
    }
}
