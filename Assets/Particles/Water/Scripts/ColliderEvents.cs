using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvents : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public LayerMask colliderMask;


    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject gameObject)
    {
        if (gameObject.layer == Mathf.RoundToInt(Mathf.Log(colliderMask.value, 2))) {
            GameEvents.current.TriggerGameObjectHit(gameObject);
        }
    }
}
