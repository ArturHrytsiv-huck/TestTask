using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReportComponent : MonoBehaviour
{
    public Action onFinish { get; set; }

    public Vector3 GetFinishPos { get => transform.position; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            onFinish.Invoke();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You WIN!");
            SceneManager.LoadScene(0);
        }
    }
}
