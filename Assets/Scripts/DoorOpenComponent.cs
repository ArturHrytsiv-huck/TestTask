using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenComponent : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject partToRotate;
    [SerializeField] private float distanceToDoor;
    [SerializeField] private float degreeToRotate = -90;

    private bool doorClosed = true;
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < distanceToDoor && doorClosed)
        {
            doorClosed = false;
            transform.localEulerAngles = new Vector3(-90.0f, degreeToRotate, 0.0f);
        }        
    }
}
