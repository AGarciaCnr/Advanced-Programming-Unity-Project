using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed = 10;
    public float timeToDespawn = 5;
    public LayerMask targetLayers;
    public int damageAmount = 10;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position += transform.forward * (movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PoolManager.Despawn(gameObject);

        if ((targetLayers & (1 << collision.gameObject.layer)) != 0)
        {
            collision.gameObject.GetComponent<Character>()?.TakeDamage(damageAmount);
        }
    }

    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(timeToDespawn);
        PoolManager.Despawn(gameObject);
    }
}
