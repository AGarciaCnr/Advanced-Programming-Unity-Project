using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : ShootGenerator
{
    private Vector3 _shootDirection;

    public PlayerWeapon(GameObject bulletPrefab, float shootInterval) : base(bulletPrefab, shootInterval) { }

    private void Update()
    {
        // Se obtiene la direcci�n del disparo
        _shootDirection = this.transform.forward;
        // Al hacer click izquierdo, se dispara
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(_shootDirection);
        }
    }
}