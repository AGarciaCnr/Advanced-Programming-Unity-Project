using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    private float _shootInterval;

    public GameObject BulletPrefab
    {
        get { return _bulletPrefab; }
        set { _bulletPrefab = value; }
    }

    public float ShootInterval
    {
        get { return _shootInterval; }
        set { _shootInterval = value; }
    }

    public ShootGenerator(GameObject bulletPrefab, float shootInterval)
    {
        this._bulletPrefab = bulletPrefab;
        this._shootInterval = shootInterval;
    }

    void Start()
    {
        PoolManager.Load(_bulletPrefab, 10);
    }

    public virtual void Shoot(Vector3 shootDirection)
    {
        PoolManager.Spawn(_bulletPrefab, transform.position + Vector3.up + transform.forward, transform.rotation);
    }

}
