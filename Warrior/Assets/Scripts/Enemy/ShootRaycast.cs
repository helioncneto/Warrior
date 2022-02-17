using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour
{
  

    private Weapon _weapon;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("shooting");
        }
    }

    void CanShoot()
    {
        if (_weapon != null)
        {
            StartCoroutine(_weapon.ShootWithRaycast());
        }
    }
}
