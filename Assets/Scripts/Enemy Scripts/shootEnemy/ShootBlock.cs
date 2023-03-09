using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBlock : MonoBehaviour
{
    private Transform shotPoint;
    public float timeBetweenShots;
    private bool canShoot = true;
    public GameObject bullet;
    public int whichRoom;
    private bool inScene;
    private void Start()
    {
        shotPoint = gameObject.transform.GetChild(0);
    }

    public void Update()
    {
        GameObject player = GameObject.Find("player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth.currentRoom == whichRoom)
        {
            inScene = true;
        }
        else
        {
            inScene = false;
        }
        if (canShoot && inScene)
        {
            canShoot = false;
            StartCoroutine(shootCooldown());
        }
    }

    private void shoot()
    {
        Instantiate(bullet, shotPoint.position, gameObject.transform.rotation);
    }
    public IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        shoot();
        canShoot = true;
    }
}
