using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AsteroidExploder : MonoBehaviour
{
    //private enum AsteroidSize {Big, Medium, Small};
    //private AsteroidSize size = AsteroidSize.Big;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Player"))
        {
            //KA-POW
            ContactPoint2D point = collision.GetContact(0);
            Vector2 direction = (point.point - (Vector2)transform.position).normalized;
            Debug.DrawRay(transform.position, direction, Color.red, 3);
            Debug.DrawRay(transform.position, -direction, Color.green, 3);
            Debug.DrawRay(transform.position, RotateBetween(direction, -direction, 70, 180), Color.white,3);
            Debug.DrawRay(transform.position, RotateBetween(direction, -direction, 180, 290), Color.magenta, 3);
        }
    }

    public Vector2 RotateBetween(Vector2 start, Vector2 target, float minAngle, float maxAngle)
    {
        float finalRads = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
        //Rotation matrix mult
        Vector2 final = new Vector2(start.x * Mathf.Cos(finalRads) - start.y * Mathf.Sin(finalRads),
            start.x * Mathf.Sin(finalRads) + start.y * Mathf.Cos(finalRads));
        return final.normalized;
    }

}
