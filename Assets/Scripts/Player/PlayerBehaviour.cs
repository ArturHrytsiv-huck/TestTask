using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float distanceToJump;
    [SerializeField] private float speed;
    
    [SerializeField] private GameObject placeToSpawn;

    [SerializeField] private Camera cam;

    [SerializeField] private ReportComponent reportComponent;
    [SerializeField] private PlayerController playerController; 
    
    private Vector3 localPos;
    private Vector3 camOffset;
    private Vector3 spawnPlaceOffset;

    private bool goStraight = false;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        reportComponent.onFinish += MoveToFinish;
        localPos = transform.position;
        camOffset = cam.transform.position;
        spawnPlaceOffset = placeToSpawn.transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                localPos.z += distanceToJump;
                camOffset.z += distanceToJump;
                spawnPlaceOffset.z += distanceToJump;

                goStraight = true;
                StartCoroutine(waitToShoot());
            }

        }
        if (goStraight)
        {
            
            if (Vector3.Distance(transform.position, localPos) < 0.1f)
            {
                goStraight = !goStraight;
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, localPos, speed * Time.deltaTime);
            placeToSpawn.transform.position = Vector3.MoveTowards(placeToSpawn.transform.position, spawnPlaceOffset, speed * Time.deltaTime);
        }
    }
    private void LateUpdate()
    {
        if (goStraight)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camOffset, speed * Time.deltaTime);
        }
        
    }
    private IEnumerator waitToShoot()
    {
        if (goStraight)
        {
            goStraight = false;
            yield return new WaitForSeconds(1.5f);
        }
        goStraight = true;
    }
    private void MoveToFinish()
    {
        localPos.z = reportComponent.GetFinishPos.z;
        Destroy(reportComponent.GetComponent<GameObject>(), 0.5f);
        goStraight = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Debug.Log("You Lose!");
            SceneManager.LoadScene(0);
        }
    }

}
