using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidMovement : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 7f;

    public Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Debug.Log(" End Awake rb2d: " + rb2d);
    }

    public void SpawnAsteroid(Vector2 position, Vector2 direction, float speed, float rotationSpeed)
    {
        Debug.Log("Start Spawn Asteroid from Movement");
        gameObject.SetActive(true);
        transform.position = position;
        rb2d.velocity = direction.normalized * speed;
        rb2d.angularVelocity = rotationSpeed;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 359));
        Debug.Log("End Spawn Asteroid from Movement");
    }

    void DisableAsteroid()
    {
        gameObject.SetActive(false);
    }
}
