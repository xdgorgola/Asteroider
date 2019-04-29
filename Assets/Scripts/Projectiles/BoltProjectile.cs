using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : MonoBehaviour
{
    /// <summary>
    /// Projectile movement speed
    /// </summary>
    [SerializeField]
    private float projSpeed = 25f;
    /// <summary>
    /// Bolt damage
    /// </summary>
    private float boltDamage = 0f;
    /// <summary>
    /// Bolt sprite
    /// </summary>
    private Sprite projSprite;

    /// <summary>
    /// Sprite renderer component
    /// </summary>
    private SpriteRenderer sprRenderer;

    private void Awake()
    {
        sprRenderer = transform.Find("VFX").GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke("DestroyP", 5f);
    }

    /// <summary>
    /// Spawn a projectile in spawnTrans
    /// </summary>
    /// <param name="spawnTrans">Position/Rotation for the projecitle to spawn</param>
    /// <param name="spr">Projectile sprite</param>
    /// <param name="speed">Projectile speed</param>
    public void SpawnProjectile(Transform spawnTrans, Sprite spr, float speed)
    {
        gameObject.transform.position = spawnTrans.position;
        gameObject.transform.rotation = spawnTrans.rotation;
        projSpeed = speed;
        projSprite = spr;
        sprRenderer.sprite = spr;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * projSpeed * Time.deltaTime, Space.Self);
    }

    void DestroyP()
    {
        gameObject.SetActive(false);
    }
}
