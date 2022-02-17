using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolOld : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float speed = 1f;
    public float waitTime = 2f;

    private Animator _animator;
    private GameObject _target;
    private Weapon _weapon;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creates the target object
        UpdateTarget();
        // Start the routine
        StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateTarget()
    {
        //If null create the target in the left
        if (_target == null)
        {
            if (transform.localScale.x > 0f)
            {
                _target = new GameObject("Target");
                _target.transform.position = new Vector2(maxX, transform.position.y);
                transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            else
            {
                _target = new GameObject("Target");
                _target.transform.position = new Vector2(minX, transform.position.y);
                transform.localScale = new Vector3(-1, 1, 1);
                return;
            }
        }

        // When target reaches the min position the change the position of the target do the right
        if (_target.transform.position.x == minX)
        {
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if (_target.transform.position.x == maxX)
        {
            // When target reaches the min position the change the position of the target do the right
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator PatrolToTarget()
    {
        while(Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
        {
            // While Enemy didn't  reache the target move the enemy to the target
            Vector2 direction = _target.transform.position - transform.position;
            float xDirection = direction.x;

            _animator.SetBool("idle", false);
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            yield return null;
        }

        _animator.SetBool("idle", true);
        Debug.Log("Target reached");
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);

        Debug.Log("Waiting for " + waitTime + " seconds");

        UpdateTarget();
        _animator.SetTrigger("shooting");
        
        yield return new WaitForSeconds(waitTime);

        Debug.Log("Wait enought");
        StartCoroutine("PatrolToTarget");
    }

    public void CanShoot()
    {
        if (_weapon != null)
        {
            _weapon.Shoot();
        }
    }
}
