using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExploder : MonoBehaviour
{
    //THE EXPLOSION RANGE SHOULD BE HANDLED IN THE SPAWN/INITIALIZATION OF THE MISSILE WITH
    //AN SCRIPTABLE OBJECT TO SAVE THE DATA

    [SerializeField]
    private float explosionDamage = 10f;
    /// <summary> Missile explosion range </summary>
    [SerializeField]
    private float explosionRange = 10f;
    /// <summary> Explosion knockback </summary>
    [SerializeField]
    private float knockBack = 10f;
    /// <summary> Time before the missile self destructs. Setted by inspector. Recommended 3f/</summary>
    [SerializeField]
    private float autoDestructTime = 3f;
    /// <summary> Is the missile self destructing? </summary>
    private bool autoDestruct = false;
    
    public Coroutine SelfDestruction
    {
        set { autoDestructRoutine = value; }
    }

    /// <summary> Self Destruction coroutine </summary>
    private Coroutine autoDestructRoutine;

    /// <summary> Missile RigidBody2D </summary>
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void InitializeExplosionData(Missile missile)
    {
        explosionDamage = missile.missileDamage;
        explosionRange = missile.explosionRange;
        knockBack = missile.knockBack;
    }

    /// <summary> Starts missile self destruction </summary>
    public IEnumerator InitiateAutoDestruction()
    {
        yield return new WaitForSeconds(autoDestructTime);
        Explode(explosionRange);
        //Debug.Log("DESTRUIDO");
        StopCoroutine(autoDestructRoutine);
    }
  
    //Make the overlap ignore the missile!
    /// <summary> Detects rb2d in explosion radius </summary>
    /// <param name="explosionRange"> Explosion radius </param>
    public void Explode(float explosionRange)
    {
        Collider2D[] hitted = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D hit in hitted)
        {
            Debug.Log(hit.gameObject.name);           
            if (hit.gameObject != gameObject && hit.GetComponent<MultiTag>().HasTag("Damageable"))
            {
                AddExplosionForce(hit);
                hit.gameObject.GetComponent<HealthManager>().TakeDamage(explosionDamage);
            }

            //if (hit.CompareTag("Player") && hit.gameObject != gameObject)
            //{
            //    AddExplosionForce(hit);
            //    hit.gameObject.GetComponent<HealthManager>().TakeDamage(explosionDamage);
            //}
        }
        gameObject.SetActive(false);
    }

    /// <summary> Adds a force to a rb2d directed from a center </summary>
    /// <param name="hit"> Hitted rb2d's collider </param>
    void AddExplosionForce(Collider2D hit)
    {
        //I should make that hitted rb2d closer to the explosion radius gets affected by a bigger explosion force
        Vector2 forceDir = (hit.transform.position - transform.position).normalized;
        if (forceDir == Vector2.zero)
        {
            forceDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        }
        Debug.DrawRay(transform.position, forceDir, Color.magenta, 3f);
        hit.attachedRigidbody.AddForce(forceDir * knockBack, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MultiTag>().HasTag("Damageable"))
        {
            Explode(explosionRange);
        }
        //if (collision.collider.CompareTag("Player"))
        //{
        //    Explode(explosionRange);
        //}
    }
}
