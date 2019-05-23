using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
public class MissileMover : MonoBehaviour
{
    //Helper Period: When the missile is spawned, it starts in a helper period that makes it follow the target even
    //if it isn't in the missile range

    /// <summary> Missile speed </summary>
    [SerializeField]
    private float missileSpeed = 30f;
    /// <summary> Missile detection range</summary>
    [SerializeField]
    private float detectionRange = 30f;
    /// <summary> Helper period counter </summary>
    private float counter = 0f;
    /// <summary> Helper Period duration </summary>
    private float helperTime = 2f;
    /// <summary> Is the missile in the helper period? </summary>
    private bool helper = true;
    /// <summary> Is the target in range? </summary>
    private bool inRange;
    /// <summary> Is the missile spawned untargeted? </summary>
    private bool untargeted;
    /// <summary> Is the missile self destructing? </summary>
    private bool autoDestruct = false;
    
    /// <summary> Missile direciton </summary>
    private Vector2 direction = Vector2.zero;
    //Just for testing
    /// <summary> Missile target </summary>
    [SerializeField]
    private Transform target;
    /// <summary> Missile RigidBody2D </summary>
    private Rigidbody2D rb2d;
    private SpriteRenderer sprRenderer;
    /// <summary> Last shooter associated collider </summary>
    private Collider2D lastShooterColl = null;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        //Physics2D.IgnoreLayerCollision(0, 2);
    }
    
    private void Start()
    {
        gameObject.SetActive(false);   
    }
    
    void Update()
    {
        if (counter < helperTime) counter += Time.deltaTime;
        else if (counter >= helperTime && helper) helper = false;
        if (helper)
        {
            direction = (Vector2)(target.position - transform.position).normalized;
            rb2d.velocity = direction * missileSpeed; //* Time.deltaTime;
            RotateMissile();
        }
        else if (!helper)
        {
            if (TargetInRange())
            {
                direction = (Vector2)(target.position - transform.position).normalized;
                rb2d.velocity = direction * missileSpeed; //* Time.deltaTime;
                RotateMissile();
            }
            else if (!TargetInRange() && !autoDestruct && !untargeted)
            {
                rb2d.velocity = direction * missileSpeed;
                autoDestruct = true;
                MissileExploder exploder = GetComponent<MissileExploder>();
                exploder.SelfDestruction = StartCoroutine(exploder.InitiateAutoDestruction());
            }
            else if (!autoDestruct && untargeted)
            {
                direction = transform.right.normalized;
                rb2d.velocity = direction * missileSpeed;
                MissileExploder exploder = GetComponent<MissileExploder>();
                exploder.SelfDestruction = StartCoroutine(exploder.InitiateAutoDestruction());
            }
        }   
    }
    
    /// <summary> Sets some missile variables </summary>
    /// <param name="missile"> Missile </param>
    public void InitializeMissileMovement(Missile missile)
    {
        detectionRange = missile.detectionRange;
        missileSpeed = missile.speed;
        sprRenderer.sprite = missile.missileSprite;
    }

    //Se le puede pasar el SCRIPTABLE OBJECT DEL MISIL para que indique radio de explosion y eso.
    /// <summary> Spawns a missile with a target </summary>
    /// <param name="newTarget"> Missile target </param>
    /// <param name="spawnTransform"> Spawn position </param>
    public void SpawnTargetedMissile(Transform newTarget, Transform spawnTransform)
    {
        if (lastShooterColl != null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), lastShooterColl, false);
        lastShooterColl = spawnTransform.GetComponentInParent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), lastShooterColl);

        counter = 0f;
        helper = true;
        autoDestruct = false;
        untargeted = false;
        transform.position = spawnTransform.position;
        target = newTarget;
        gameObject.SetActive(true);

    }

    /// <summary> Spawns a missile with no target </summary>
    /// <param name="spawnTransform"> Spawn Position </param>
    public void SpawnUnTargetedMissile(Transform spawnTransform)
    {
        if (lastShooterColl != null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), lastShooterColl, false);
        lastShooterColl = spawnTransform.GetComponentInParent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), lastShooterColl);

        counter = 0f;
        helper = false;
        autoDestruct = false;
        untargeted = true;
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;
        target = null;
        gameObject.SetActive(true);
    }
    
    /// <summary> Rotates the missile to the target </summary>
    void RotateMissile()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, Vector2.Perpendicular(direction));
        //Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, maxRotation * Time.deltaTime);
        //transform.rotation = finalRotation;
        transform.rotation = desiredRotation;
    }
    
    /// <summary> Detects if the missile target is in range </summary>
    /// <returns> If the missile target in range </returns>
    bool TargetInRange()
    {
        if (target == null) return false;
        if (((Vector2)(target.position - transform.position)).magnitude <= detectionRange)
        {
            inRange = true;
            return true;
        }
        inRange = false;
        return false;
    }
}
