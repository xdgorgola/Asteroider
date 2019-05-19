using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidEvents : UnityEvent { }

[RequireComponent(typeof(CircleCollider2D))]
public class AsteroidExploder : MonoBehaviour
{
    /// <summary> Asteroid size </summary>
    [SerializeField]
    private AsteroidPoolTest.AsteroidSize size = AsteroidPoolTest.AsteroidSize.Big;

    /// <summary> Asteroid Pool </summary>
    private AsteroidPoolTest asteroidPool;

    public AsteroidField associatedField = null;

    public AsteroidEvents onAsteroidDestroy = new AsteroidEvents();

    void Awake()
    {
        //Initializes asteroid pool
        asteroidPool = GameObject.FindGameObjectWithTag("Pool Manager").GetComponent<AsteroidPoolTest>();
    }

    /// <summary> Rotates a vector between min and max angles</summary>
    /// <param name="start">Vector to rotate</param>
    /// <param name="minAngle">Min rotation angle</param>
    /// <param name="maxAngle">Max rotation angle</param>
    /// <returns>Start vector rotated between min and max angle</returns>
    public Vector2 RotateBetween(Vector2 start, float minAngle, float maxAngle)
    {
        float finalRads = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
        //Rotation matrix mult
        Vector2 final = new Vector2(start.x * Mathf.Cos(finalRads) - start.y * Mathf.Sin(finalRads),
            start.x * Mathf.Sin(finalRads) + start.y * Mathf.Cos(finalRads));
        return final.normalized;
    }

    /// <summary> Spawns an asteroid </summary>
    /// <param name="size">Asteroid size</param>
    /// <param name="position">Asteroid position</param>
    /// <param name="direction">Asteroid direction</param>
    /// <param name="speed">Asteroid speed</param>
    /// <param name="rotationSpeed">Asteroid rotation speed</param>
    private void SpawnAsteroid(AsteroidPoolTest.AsteroidSize size, Vector2 position, Vector2 direction, float speed, float rotationSpeed)
    {
        GameObject asteroid = asteroidPool.GetAsteroidFromPool(size);
        asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(position, direction, speed, rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Player") || collider.CompareTag("Projectile"))
        {

            //Gets contact point
            ContactPoint2D point = collision.GetContact(0);
            //Direction from transform position to contact point
            Vector2 direction = (point.point - (Vector2)transform.position).normalized;
            //Bounce directions
            Vector2 bounce1 = RotateBetween(direction, 70, 180);
            Vector2 bounce2 = RotateBetween(direction, 180, 290);

            Debug.DrawRay(transform.position, direction, Color.red, 3);
            Debug.DrawRay(transform.position, -direction, Color.green, 3);
            Debug.DrawRay(transform.position, bounce1, Color.white,3);
            Debug.DrawRay(transform.position, bounce2, Color.magenta, 3);

            //Big asteroids explodes in a single medium asteroid
            if (size == AsteroidPoolTest.AsteroidSize.Big)
            {
                SpawnAsteroid(AsteroidPoolTest.AsteroidSize.Medium, transform.position, bounce1, Random.Range(5,20), 25);
            }
            //Medium asteroids explodes in a pair of small asteroids
            else if (size == AsteroidPoolTest.AsteroidSize.Medium)
            {
                SpawnAsteroid(AsteroidPoolTest.AsteroidSize.Small, transform.position, bounce1, Random.Range(5, 20), 25);
                SpawnAsteroid(AsteroidPoolTest.AsteroidSize.Small, transform.position, bounce2, Random.Range(5, 20), 25);
            }

            onAsteroidDestroy.Invoke();

            //Deactivates asteroid
            gameObject.SetActive(false);
            //Deactives projectile
            if (collider.CompareTag("Projectile"))
            {
                collider.gameObject.SetActive(false);
            }
        }
    }



}
