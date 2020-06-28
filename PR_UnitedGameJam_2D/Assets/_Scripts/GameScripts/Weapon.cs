using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public LayerMask whatToHit;
    public Rigidbody2D bullet;
    public float speed = 10;

    private Vector2 AimDirection {
        get {
            if(Input.GetAxis("rightStickHorizontal") != 0 || Input.GetAxis("rightStickVertical") != 0)
            {
                return new Vector2(Input.GetAxis("rightStickHorizontal"), -Input.GetAxis("rightStickVertical"));
            } 
            else
            {
                return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;
            }
        }
    }

    float timeToFire = 0;
    Transform firePoint;

    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firePoint WHY?????");
        }

		Physics2D.IgnoreLayerCollision(8, 11);
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton ("Fire") && Time.time > timeToFire)
            {
                timeToFire = Time.time * 1 / fireRate;

                Shoot();
            }
        }
    }

    void Shoot()
	{
        Vector2 mousePostition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        // RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePostition - firePointPos, 100, whatToHit);
        Rigidbody2D instantiatedProjectile = Instantiate(bullet,
                                                          transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        instantiatedProjectile.velocity = AimDirection.normalized * speed;
        //Debug.DrawLine(firePointPos, (AimDirection)*100, Color.cyan);
        //if (hit.collider != null)
        //{

        //   Debug.DrawLine(firePointPos, hit.point, Color.red);
        //}
        Destroy(instantiatedProjectile.gameObject, 3);
    }
}
