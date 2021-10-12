using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComponent : MonoBehaviour
{
    [SerializeField] private int enemyLayer = 6;

    [SerializeField] private float secondsToDestroy;

    public Action onCollision;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == enemyLayer)
        {
            onCollision.Invoke();
            Destroy(gameObject, secondsToDestroy);
        }
    }
}
