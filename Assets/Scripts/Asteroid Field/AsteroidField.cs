using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AsteroidField : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Recomendado un radio mayor a 40")]
    private float fieldRadius = 40f;
    /// <summary> Asteroid spawn radius around player </summary>
    [SerializeField]
    private float spawnRadius = 20f;

    /// <summary> Max number of asteroids in the field </summary>
    [SerializeField]
    private int asteroidNumber = 10;
    private int asteroidCounter = 0;
    private bool active = false;

    private float activationDelay = 3f;
    [SerializeField]
    private float spawnDelay = 2f;

    private CircleCollider2D fieldColl;
    private Bounds fieldBound;
    private Coroutine spawnRoutine;

    private Rigidbody2D playerRB;
    //public GameObject testObject;

    private void Awake() {

        if (!GetComponent<CircleCollider2D>()) {
            gameObject.AddComponent<CircleCollider2D>();
            fieldColl = GetComponent<CircleCollider2D>();
            fieldColl.isTrigger = true;
            fieldColl.radius = fieldRadius;
        }
        fieldColl = GetComponent<CircleCollider2D>();
        if (!fieldColl.isTrigger) {
            fieldColl.isTrigger = true;
        }   

        if (fieldRadius < 40f) {
            Debug.LogWarning("Field radius smaller than 40! Changing to 40");
            fieldColl.radius = 40f;
            fieldRadius = 40;
        }

        if (fieldRadius != fieldColl.radius)
        {
            fieldColl.radius = fieldRadius;
        }
        fieldBound = fieldColl.bounds;

        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        active = false;
        InitializeField();
    }

    void InitializeField()
    {
        //int indexO = 0;
        while (asteroidCounter < asteroidNumber) //&& //indexO < 100)
        {
            //int indexI = 0;
            Vector2 position = (Vector2)fieldBound.center + Random.insideUnitCircle * fieldRadius;
            Debug.Log((Vector2)fieldBound.center + Random.insideUnitCircle * fieldRadius);
            //Why 2.133333f + arbitraryValue? Because that is the minimun radium one of my asteroid sprites fits
            //Instantiate(testObject, new Vector3(position.x, position.y,0), Quaternion.identity);
            while (Physics2D.OverlapCircle(position, 2.133333f + 10, 1 << LayerMask.NameToLayer("Asteroid")) != null)
            {
                //Instantiate(testObject, new Vector3(position.x, position.y, 0), Quaternion.identity);
                Debug.Log("CACA");
                position = (Vector2)fieldBound.center + Random.insideUnitCircle * fieldRadius;
                //indexI += 1;
                //if (indexI >= 100)
                //{
                //    position = Vector2.zero;
                //    break;
                //}
            }
            SpawnAsteroid(position);
            asteroidCounter += 1;
            //indexI += 1;
        }
    }

    IEnumerator AsteroidSpawner()
    {
        yield return new WaitForSeconds(activationDelay);
        SpawnAsteroid();
        while (true) {
            yield return new WaitForSeconds(spawnDelay);
            SpawnAsteroid();
        }
        
    }

    private void SpawnAsteroid()
    {
        //Recuperar de una pool
        Debug.Log("Spawneando asteroide...");
        //GameObject asteroid = AsteroidPool.asteroidPool.GetFromPool();
        //Vector2 spawnPosition = (Vector2)fieldBound.center + GenerateDirection() * fieldRadius;
        //Debug.Log("Centro: " + (Vector2)fieldBound.center);
        //Debug.Log("Posicion final: " + spawnPosition);
        //Vector2 direction = (playerRB.position - spawnPosition).normalized;
        //asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(spawnPosition, direction, 20, 40);

        GameObject asteroid = AsteroidPool.asteroidPool.GetFromPool();
        Vector2 spawnPosition = playerRB.position + GenerateDirection() * spawnRadius;
        Vector2 fieldPoint = (Vector2)fieldBound.center + Random.insideUnitCircle * fieldRadius;
        Vector2 direction = fieldPoint - spawnPosition;
        asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(spawnPosition, direction, 20, 40);



    }

    private void SpawnAsteroid(Vector2 position)
    {
        GameObject asteroid = AsteroidPool.asteroidPool.GetFromPool();
        asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(position, Vector2.zero, 0, 40);
    }

    /// <summary> Generates a random position in the unitary circle </summary>
    /// <returns> Position in unitary circle </returns>
    Vector2 GenerateDirection()
    {
        float angle = Random.Range(0, 360);
        Debug.Log("Pos generada: " + "x: " + Mathf.Cos(angle * Mathf.Deg2Rad) + "y: " + Mathf.Sin(angle * Mathf.Deg2Rad));
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !active)
        {
            Debug.Log("Activando!");
            active = true;
            spawnRoutine = StartCoroutine(AsteroidSpawner());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            Debug.Log("Desactivando!");
            active = false;
            StopCoroutine(spawnRoutine);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 2.133333f + 2);
    }
}
