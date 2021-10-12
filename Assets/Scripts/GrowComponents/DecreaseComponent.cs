using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseComponent : MonoBehaviour
{
    [SerializeField] private float percentToDecrease;
    [SerializeField] private float startMulty;

    [SerializeField] private GameObject roadPrefab;
    public Action onGameOver;

    private float decreationNum;

    public float changeScale(GameObject player, Vector3 playerStartScale, bool gameOver)
    {
        NumOfDecreation(playerStartScale);
        float numOfPercent = startMulty * decreationNum;
        Vector3 startScaleChange = new Vector3(playerStartScale.x - numOfPercent, playerStartScale.y - numOfPercent, playerStartScale.z - numOfPercent);
        player.transform.localScale = startScaleChange;
        StartCoroutine(Decreasing(player, playerStartScale, gameOver));
        return playerStartScale.x * percentToDecrease / playerStartScale.x / 100;
    }
    private void NumOfDecreation(Vector3 playerStartScale)
    {
        decreationNum = playerStartScale.x* percentToDecrease / playerStartScale.x  / 100;
    }
    private IEnumerator Decreasing(GameObject player, Vector3 playerStartScale, bool gameOver)
    {
        while (Input.touchCount > 0)
        {
            if(player.transform.localScale.x < 0.1f)
            {
                onGameOver.Invoke();
                gameOver = true;
                break;
            }
            player.transform.localScale = percentOfDecreasing(player.transform.localScale, playerStartScale);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector3 percentOfDecreasing(Vector3 objectToDecrease, Vector3 startScale)
    {
        Vector3 roadDecrease = roadPrefab.transform.localScale;
        roadDecrease.x -= decreationNum;
        roadPrefab.transform.localScale = roadDecrease;
        objectToDecrease.x -= decreationNum;
        objectToDecrease.y -= decreationNum;
        objectToDecrease.z -= decreationNum;
        return objectToDecrease;
    }
}
