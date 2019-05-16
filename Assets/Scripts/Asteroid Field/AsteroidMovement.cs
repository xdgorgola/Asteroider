using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidMovement : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 7f;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //SpawnAsteroid(Vector2.zero, Vector2.right, 1, 100);
    }

    public void SpawnAsteroid(Vector2 position, Vector2 direction, float speed, float rotationSpeed)
    {
        gameObject.SetActive(true);
        //Debug.Log("Posicion a spawnear: " + position);
        transform.position = position;
        rb2d.velocity = direction.normalized * speed;
        rb2d.angularVelocity = rotationSpeed;
        //Realmente pasar a una corutina, porque hay que cancelar el destroy si se divide
        //Ver como haremos el divide

        Invoke("DisableAsteroid", lifeTime);
    }

    void DisableAsteroid()
    {
        gameObject.SetActive(true);
    }
}
