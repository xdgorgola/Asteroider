using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : MonoBehaviour
{
    public float projSpeed = 25f;
    private float boltDamage = 0f;
    [HideInInspector]
    public Sprite projSprite;

    private SpriteRenderer sprRenderer;

    private void Awake()
    {
        sprRenderer = transform.Find("VFX").GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke("DestroyP", 5f);
    }

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
