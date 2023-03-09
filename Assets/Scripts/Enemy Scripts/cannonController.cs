using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonController : MonoBehaviour
{
    private GameObject objectInCannon;
    private Camera cam;
    public float speed = 100f;
    public Transform shotPoint;
    public meleeAttackManager melee;
    public GameObject player;
    private void Start()
    {
        cam = Camera.main;
        shotPoint = gameObject.transform.GetChild(0);
        player = GameObject.Find("player");
        melee = player.GetComponent<meleeAttackManager>(); 
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && objectInCannon != null)
        {
            Fire(objectInCannon);
            objectInCannon = null;
            melee.canAction = false;
        }
        if(objectInCannon != false)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = (0f);
            transform.LookAt(mousePos);
            melee.canAction = true;

        }
    }


    public void Fire(GameObject playerInCannon)
    {
        Rigidbody2D rbObj = playerInCannon.GetComponent<Rigidbody2D>();
        rbObj.AddForce(shotPoint.transform.right * speed,ForceMode2D.Impulse);
    }
    public void PlayerEnterCannon(GameObject player)
    {
        objectInCannon = player;
    }
}
