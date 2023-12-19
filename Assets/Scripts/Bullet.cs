using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Velocidad y tiempo de vida de la bala.
    public float movementSpeed = 10;
    public float timeToDespawn = 5;

    // Capas objetivo y daño infligido por la bala.
    public LayerMask targetLayers;
    public int damageAmount = 10;

    private Transform _transform;

    // Inicializa la referencia al componente Transform.
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    // Se activa al habilitarse el objeto y comienza la rutina de despawn.
    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
    }

    // Actualiza la posición de la bala en cada frame.
    void Update()
    {
        _transform.position += transform.forward * (movementSpeed * Time.deltaTime);
    }

    // Se activa al colisionar con otro objeto.
    private void OnCollisionEnter(Collision collision)
    {
        // Despawnea la bala y aplica daño si el objeto colisionado es de una capa objetivo.
        PoolManager.Despawn(gameObject);

        if ((targetLayers & (1 << collision.gameObject.layer)) != 0)
        {
            collision.gameObject.GetComponent<Character>()?.TakeDamage(damageAmount);
        }
    }

    // Rutina de despawn después de un tiempo de vida.
    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(timeToDespawn);
        PoolManager.Despawn(gameObject);
    }
}
