using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private Coroutine destroy;

    private void OnEnable()
    {
        destroy = StartCoroutine(DestroyParticle());
    }

    /// <summary> Destroys the particle in its lifeTime seconds</summary>
    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().main.startLifetime.constant);
        gameObject.SetActive(false);
    }

}
