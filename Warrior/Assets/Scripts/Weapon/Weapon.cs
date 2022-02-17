using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shooter;
    private Transform _firepoint;

    public GameObject explosion;
    public LineRenderer lineRender;

    void Awake()
    {
        _firepoint = transform.Find("FirePoint");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (bullet != null && _firepoint != null && shooter != null)
        {
            GameObject myBullet = Instantiate(bullet, _firepoint.position, Quaternion.identity) as GameObject;
            Bullet bulletComponent = myBullet.GetComponent<Bullet>();

            if (shooter.transform.localScale.x > 0f)
            {
                bulletComponent.direction = Vector2.right;
            } else
            {
                bulletComponent.direction = Vector2.left;
                
            }
        }
    }

    public IEnumerator ShootWithRaycast()
    {
        if (explosion != null && lineRender != null)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(_firepoint.position, _firepoint.right);

            if (hitInfo)
            {
                //Example code:
                //if (hitInfo.colider.tag == "Player")
                //{
                //Transform player = hitInfo.point;
                //player.GetComponent<PlayerHealth>().ApplyDamage(5);
                //}

                GameObject explosionEffect = Instantiate(explosion, hitInfo.point, Quaternion.identity) as GameObject;

                lineRender.SetPosition(0, _firepoint.position);
                lineRender.SetPosition(1, hitInfo.point);
                Destroy(explosionEffect, 1f);
            }
            else
            {
                lineRender.SetPosition(0, _firepoint.position);
                lineRender.SetPosition(1, hitInfo.point + Vector2.right * 100);
            }

            lineRender.enabled = true;
            yield return null;
            lineRender.enabled = false;
        }
    }
}
