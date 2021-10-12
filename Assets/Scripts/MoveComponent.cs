using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject finish;

    [SerializeField] private string finishName = "Finish";
    
    private float startSpeed = 0;
    private ColliderComponent colliderComponent;

    private void Start()
    {
        colliderComponent = GetComponentInChildren<ColliderComponent>();
        colliderComponent.onCollision += ChangeSpeed;
        finish = GameObject.Find(finishName);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ChangeSpeed();
            }
        }
        
        
    }
    private void ChangeSpeed()
    {
        float speedChange = startSpeed;
        startSpeed = speed;
        speed = speedChange;
    }
    private void Move()
    {
        Vector3 dir = finish.transform.position - transform.position;
        float distanceThisFrame = startSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            return;
        }
        transform.LookAt(finish.transform.position);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

}
