using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AsteroidExploder : MonoBehaviour
{
    [SerializeField]
    private AsteroidPoolTest.AsteroidSize size = AsteroidPoolTest.AsteroidSize.Big;

    AsteroidPoolTest asteroidPool;

    void Awake()
    {
        //Haceri nicializacion en caso de que no exista
        asteroidPool = GameObject.FindGameObjectWithTag("Pool Manager").GetComponent<AsteroidPoolTest>();
    }

    public Vector2 RotateBetween(Vector2 start, Vector2 target, float minAngle, float maxAngle)
    {
        float finalRads = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
        //Rotation matrix mult
        Vector2 final = new Vector2(start.x * Mathf.Cos(finalRads) - start.y * Mathf.Sin(finalRads),
            start.x * Mathf.Sin(finalRads) + start.y * Mathf.Cos(finalRads));
        return final.normalized;
    }

    private void SpawnAsteroid(AsteroidPoolTest.AsteroidSize size, Vector2 position, Vector2 direction, float speed, float rotationSpeed)
    {
        GameObject asteroid = asteroidPool.GetAsteroidFromPool(size);
        asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(position, direction, speed, rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Player"))
        {
            //KA-POW
            ContactPoint2D point = collision.GetContact(0);
            Vector2 direction = (point.point - (Vector2)transform.position).normalized;
            Vector2 bounce1 = RotateBetween(direction, -direction, 70, 180);
            Vector2 bounce2 = RotateBetween(direction, -direction, 180, 290);
            Debug.DrawRay(transform.position, direction, Color.red, 3);
            Debug.DrawRay(transform.position, -direction, Color.green, 3);
            Debug.DrawRay(transform.position, bounce1, Color.white,3);
            Debug.DrawRay(transform.position, bounce2, Color.magenta, 3);
            if (size == AsteroidPoolTest.AsteroidSize.Big)
            {
                SpawnAsteroid(AsteroidPoolTest.AsteroidSize.Medium, transform.position, bounce1, 20, 25);
            }
            else if (size == AsteroidPoolTest.AsteroidSize.Medium)
            {
                SpawnAsteroid(AsteroidPoolTest.AsteroidSize.Small, transform.position, bounce1, 20, 25);
                SpawnAsteroid(AsteroidPoolTest.AsteroidSize.Small, transform.position, bounce2, 20, 25);
            }
            gameObject.SetActive(false);
        }
    }



}
