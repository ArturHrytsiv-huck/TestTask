using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeComponent : MonoBehaviour
{

    [SerializeField] private ColliderComponent colliderComponent;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float multyOfExplotion;
    [SerializeField] private int enemyLayerMask;

    [SerializeField] private Material colorToExplode;

    private void Start()
    {
        colliderComponent = GetComponent<ColliderComponent>();
        colliderComponent.onCollision += Explode;
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, bullet.transform.localScale.x * multyOfExplotion);

        foreach  (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.layer == enemyLayerMask)
            {
                nearbyObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Destroy(nearbyObject.gameObject, 0.7f);
            }
        }

    }
}