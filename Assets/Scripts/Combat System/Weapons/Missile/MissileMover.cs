using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
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

      
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
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
            else if (!TargetInRange() && !autoDestruct)
            {
                rb2d.velocity = direction * missileSpeed;
                autoDestruct = true;
                MissileExploder exploder = GetComponent<MissileExploder>();
                exploder.SelfDestruction = StartCoroutine(exploder.InitiateAutoDestruction());
            }   
        }   
    }
    
    public void InitializeMissileMovement(Missile missile)
    {
        detectionRange = missile.detectionRange;
        missileSpeed = missile.speed;
        sprRenderer.sprite = missile.missileSprite;
    }

    //Se le puede pasar el SCRIPTABLE OBJECT DEL MISIL para que indique radio de explosion y eso.
    //Asegurarse de que no le haga dano a quien lo tire
    //Pero si pasa del counter, que ademas va a permitir que el misil siga al enemigo un tiempo aunque no este en rango al inicio. si le hace dano la explosion
    public void SpawnMissile(Transform newTarget, Transform spawnTransform, float speed)
    {
        counter = 0f;
        helper = true;
        autoDestruct = false;
        transform.position = spawnTransform.position;
        missileSpeed = speed;
        target = newTarget;
        //sprite y eso
    
    }
    
    /// <summary> Rotates the missile to the target </summary>
    void RotateMissile()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, Vector2.Perpendicular(direction));
        transform.rotation = desiredRotation;
    }
    
    /// <summary> Detects if the missile target is in range </summary>
    /// <returns> If the missile target in range </returns>
    bool TargetInRange()
    {
        if ( ((Vector2)(target.position - transform.position)).magnitude <= detectionRange )
        {
            inRange = true;
            return true;
        }
        inRange = false;
        return false;
    }
}
