using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    private float shootInterval;

    public GameObject BulletPrefab
    {
        get { return bulletPrefab; }
        set { bulletPrefab = value; }
    }

    public float ShootInterval
    {
        get { return shootInterval; }
        set { shootInterval = value; }
    }

    public ShootGenerator(GameObject bulletPrefab, float shootInterval)
    {
        this.bulletPrefab = bulletPrefab;
        this.shootInterval = shootInterval;
    }

    public virtual void Shoot(Vector3 shootDirection)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * 20;
    }

}
