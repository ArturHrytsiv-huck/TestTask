using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    
    [Header("Bullet")]
    [SerializeField] private GameObject bulletSpawnPlace;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Grow Components")]
    [SerializeField] private DecreaseComponent decreaseComponent;
    [SerializeField] private IncreaseComponent increaseComponent;

    [Header("Road To Finish")]
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Vector3 roadScale;
    [SerializeField] private Vector3 roadPos;

    private Touch touch;
    private bool gameOver = false;
    private bool canShoot = true;
    public bool CanShoot { get { return canShoot; } }

    private void Start()
    {
        RoadToFinish();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            gameOver = true;
            canShoot = false;
        }
    }
    private void Update()
    {
        if (gameOver)
        {
            return;
        }
        if (player.transform.localScale.x < 0.1f)
        {
            gameOver = true;
            Debug.Log("You Lose!");
            SceneManager.LoadScene(0);
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && canShoot)
            {
                StartCoroutine(waitToShoot());
                StartHit();

            }
        }
        
    }

    private IEnumerator waitToShoot()
    {
        if(canShoot)
        {
            canShoot = false;
            yield return new WaitForSeconds(1.5f);
        }
        canShoot = true;
    }
    private void RoadToFinish()
    {
        roadPrefab.transform.localScale = roadScale;
        roadPrefab.transform.position = roadPos;
    }
    private void StartHit()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawnPlace.transform.position, bulletSpawnPlace.transform.rotation);
        float numOfGrowing = decreaseComponent.changeScale(player, player.transform.localScale, gameOver);
        increaseComponent.changeScale(bullet, numOfGrowing);  
    }
    
}
