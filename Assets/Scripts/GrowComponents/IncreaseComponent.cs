using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IncreaseComponent : MonoBehaviour
{
    [SerializeField] private float startMulty;
    [SerializeField] private float percentToIncrease;

    [SerializeField] private DecreaseComponent decreaseComponent;

    private Coroutine corutine;

    private void Start()
    {
        decreaseComponent = GetComponentInParent<DecreaseComponent>();
        decreaseComponent.onGameOver += StopIncreasing;
    }
    private void StopIncreasing()
    {
        StopCoroutine(corutine);
    }
    public void changeScale(GameObject bulletPrefab, float numOfGrowing)
    {
        Vector3 startScaleChange = new Vector3(numOfGrowing * startMulty, numOfGrowing * startMulty, numOfGrowing * startMulty);
        
        bulletPrefab.transform.localScale = startScaleChange;

        corutine = StartCoroutine(Increasing(bulletPrefab, numOfGrowing));
    }

    private IEnumerator Increasing(GameObject bulletPrefab, float numOfGrowing)
    {
        while (Input.touchCount > 0)
        {
            bulletPrefab.transform.localScale = percentOfIncreasing(bulletPrefab.transform.localScale, numOfGrowing);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private Vector3 percentOfIncreasing(Vector3 objectToIncrease, float numOfGrowing)
    {
        objectToIncrease.x += numOfGrowing;
        objectToIncrease.y += numOfGrowing;
        objectToIncrease.z += numOfGrowing;
        return objectToIncrease;
    }
}
