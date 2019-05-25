using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : MonoBehaviour
{
    /// <summary> Projectile movement speed </summary>
    [SerializeField]
    private float projSpeed = 25f;
    /// <summary> Bolt damage </summary>
    private float boltDamage = 0f;
    /// <summary> Bolt life time </summary>
    [SerializeField]
    private float boltLife = 5f;
    /// <summary> Bolt sprite </summary>
    private Sprite projSprite;

    /// <summary> Sprite renderer component </summary>
    private SpriteRenderer sprRenderer;

    /// <summary> Collider of the last one that shooted this GameObject bolt </summary>
    private Collider2D lastShooterColl = null;
    private Coroutine destroyBolt;

    private void Awake()
    {
        sprRenderer = transform.Find("VFX").GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        destroyBolt = StartCoroutine(DestroyBolt());
    }

    /// <summary>
    /// Spawn a projectile in spawnTrans
    /// </summary>
    /// <param name="spawnTrans">Position/Rotation for the projecitle to spawn</param>
    /// <param name="spr">Projectile sprite</param>
    /// <param name="speed">Projectile speed</param>
    public void SpawnProjectile(Transform spawnTrans, Bolt bolt)
    {
        if (lastShooterColl != null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), lastShooterColl, false);
        lastShooterColl = spawnTrans.GetComponentInParent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), lastShooterColl);
        gameObject.transform.position = spawnTrans.position;
        gameObject.transform.rotation = spawnTrans.rotation;
        projSpeed = bolt.speed;
        projSprite = bolt.boltSprite;
        sprRenderer.sprite = projSprite;
        boltDamage = bolt.boltDamage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * projSpeed * Time.deltaTime, Space.Self);
    }

    /// <summary> Destroys the bolt in boltLife seconds</summary>
    IEnumerator DestroyBolt()
    {
        yield return new WaitForSeconds(boltLife);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MultiTag>().HasTag("Damageable"))
        {
            Debug.Log("PEWWWW");
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(boltDamage);
        }
        StopCoroutine(destroyBolt);
        gameObject.SetActive(false);
    }
}
