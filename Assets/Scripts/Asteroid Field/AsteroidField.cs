using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidFieldEvents : UnityEvent{}

[RequireComponent(typeof(CircleCollider2D))]
public class AsteroidField : MonoBehaviour
{
    /// <summary> Asteroid field activation radius </summary>
    [SerializeField]
    [Tooltip("Recomendado un radio mayor a 40 y 40 unidades mayor al spawnRadius")]
    private float fieldRadius = 120f;
    /// <summary> Asteroid inside field spawn radius </summary>
    [SerializeField]
    private float spawnRadius = 80f;

    /// <summary> Max number of asteroids in the field </summary>
    [SerializeField]
    private int asteroidNumber = 10;
    /// <summary> Number of asteroids in field </summary>
    private int asteroidCounter = 0;
    /// <summary> Is the player in the asteroid field? </summary>
    private bool active = false;

    /// <summary> Field collider </summary>
    private CircleCollider2D fieldColl;
    /// <summary> Field collider bounds </summary>
    private Bounds fieldBound;

    /// <summary> Asteroid pool </summary>
    private AsteroidPoolTest asteroidPool;

    /// <summary> Number of asteroids in field </summary>
    public int _AsteroidCounter { get => asteroidCounter; set => asteroidCounter = value; }

    public AsteroidFieldEvents onFieldExit = new AsteroidFieldEvents();

    void Awake() {

        //Circle Collider 2D initialization
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

        //Asteroid pool initialization
        asteroidPool = GameObject.FindGameObjectWithTag("Pool Manager").GetComponent<AsteroidPoolTest>();
    }

    void Start()
    {
        active = false;
        //Invoke("InitializeField", 5f);
    }

    /// <summary> Spawns all asteroids inside the field </summary>
    void InitializeField()
    {
        Debug.Log("Inicializando...");
        while (asteroidCounter < asteroidNumber)
        {
            //El overlap no esta furulando
            Vector2 position = (Vector2)fieldBound.center + Random.insideUnitCircle * spawnRadius;
            //Why 2.133333f + arbitraryValue? Because that is the minimun radium one of my asteroid sprites fits
            //while (Physics2D.OverlapCircle(position, 2.133333f + 10, 1 << LayerMask.NameToLayer("Asteroid")) != null)
            //{
            //    position = (Vector2)fieldBound.center + Random.insideUnitCircle * fieldRadius;
            //}
            SpawnAsteroidInsideField(position);
            asteroidCounter += 1;
        }
    }

    /// <summary> Spawns an asteroid in the field </summary>
    /// <param name="position">Spawn position</param>
    private void SpawnAsteroidInsideField(Vector2 position)
    {
        GameObject asteroid = asteroidPool.GetAsteroidFromPool((AsteroidPoolTest.AsteroidSize)Random.Range(0,2));
        //asteroid.GetComponent<AsteroidExploder>().associatedField = this;
        asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(position, Vector2.zero, 0, 40);
    }

    public void SpawnAsteroidAroundPlayerField()
    {
        GameObject asteroid = asteroidPool.GetAsteroidFromPool(AsteroidPoolTest.AsteroidSize.Small);
        Vector2 fieldPoint = (Vector2)fieldBound.center + Random.insideUnitCircle * fieldRadius;
        //asteroid.GetComponent<AsteroidExploder>().associatedField = this;
        asteroid.GetComponent<AsteroidMovement>().SpawnAsteroid(fieldPoint, Vector2.zero, 0, 20);
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
            active = true;
            InitializeField();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            Debug.Log("Desactivando!");
            active = false;
            onFieldExit.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fieldRadius);
    }
}
