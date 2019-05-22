using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AsteroidRain : MonoBehaviour
{
    /// <summary> Delay between asteroid spawn </summary>
    [SerializeField]
    private float spawnDelay = 2f;
    /// <summary> Delay before activating the rain </summary>
    [SerializeField]
    private float activationDelay = 4f;
    /// <summary> Is the player in the rain range? </summary>
    private bool active = false;

    /// <summary> Radius around player where asteroids spawns </summary>
    [SerializeField]
    private float spawnRadius = 30f;

    /// <summary> Player RigidBody2D </summary>
    private Rigidbody2D playerRB2D;
    /// <summary> Shoot asteroids coroutine </summary>
    private Coroutine shootingRoutine;
    /// <summary> Asteroid pool </summary>
    private AsteroidPoolTest asteroidPool;

    private void Awake()
    {
        playerRB2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        asteroidPool = GameObject.FindGameObjectWithTag("Pool Manager").GetComponent<AsteroidPoolTest>();

        Collider2D zoneCollider = GetComponent<Collider2D>();
        if (!zoneCollider.isTrigger) zoneCollider.isTrigger = true;
    }

    /// <summary> Shoots the player an asteroid every spawnDelay seconds </summary>
    IEnumerator ShootPlayerAsteroids()
    {
        yield return new WaitForSeconds(activationDelay);
        ShootAsteroid();
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            ShootAsteroid();
        }
    }

    /// <summary> Shoots an asteroid to the player </summary>
    void ShootAsteroid()
    {
        GameObject asteroid = asteroidPool.GetAsteroidFromPool((AsteroidPoolTest.AsteroidSize)Random.Range(0, 2));
        //Asteroid spawn pos
        Vector2 spawnPosition = playerRB2D.position + GenerateDirection() * spawnRadius;
        //Point inside spawn radius
        Vector2 fieldPoint = playerRB2D.position + Random.insideUnitCircle * spawnRadius;
        //Direciton to shoot at
        Vector2 direction = (fieldPoint - spawnPosition).normalized;
        asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(spawnPosition, direction, Random.Range(20, 40), Random.Range(20, 40));
    }

    /// <summary> Generates a random position in the unitary circle </summary>
    /// <returns> Position in unitary circle </returns>
    Vector2 GenerateDirection()
    {
        float angle = Random.Range(0, 360);
        //Debug.Log("Pos generada: " + "x: " + Mathf.Cos(angle * Mathf.Deg2Rad) + "y: " + Mathf.Sin(angle * Mathf.Deg2Rad));
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !active)
        {
            Debug.Log("Activando!");
            shootingRoutine = StartCoroutine(ShootPlayerAsteroids());
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            Debug.Log("Desactivando");
            if (shootingRoutine != null)
            {
                StopCoroutine(shootingRoutine);
            }
            active = false;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position, spawnRadius);
    //}
}
