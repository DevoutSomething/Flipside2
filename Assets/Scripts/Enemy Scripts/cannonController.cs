using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonController : MonoBehaviour
{
    private GameObject objectInCannon;
    private Camera cam;
    public float speed = 100f;
    private Transform shotPoint;
    private meleeAttackManager melee;
    private GameObject player;
    private SpriteRenderer sprend;
    private CharecterController charecter;
    float gScale;
    private void Start()
    {
        cam = Camera.main;
        shotPoint = gameObject.transform.GetChild(0);
        player = GameObject.Find("player");
        melee = player.GetComponent<meleeAttackManager>();
        sprend = GameObject.Find("PlayerAnim").GetComponent<SpriteRenderer>();
        charecter = player.GetComponent<CharecterController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Dash") && objectInCannon != null)
        {
            Fire(objectInCannon);
            objectInCannon = null;
            melee.canAction = false;
        }
        if(objectInCannon != false)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = (0f);
         //   transform.LookAt(mousePos);

        }
    }


    public void Fire(GameObject playerInCannon)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.constraints = ~RigidbodyConstraints2D.FreezePosition;
        sprend.forceRenderingOff = false;
        Rigidbody2D rbObj = playerInCannon.GetComponent<Rigidbody2D>();
        rbObj.AddForce(shotPoint.transform.right * speed,ForceMode2D.Impulse);
        melee.canAction = true;
        melee.isStuck = false;
        charecter.canBypassJump = false;
        charecter.canJump = false;
        charecter.canDash = false;
        rb.gravityScale = gScale;

    }
    public void PlayerEnterCannon(GameObject player)
    {
        objectInCannon = player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("player interacted with cannon");
        if(collision.gameObject.tag == "Player")
        {
            PlayerEnterCannon(collision.gameObject);
            sprend.forceRenderingOff = true;
            player.transform.position = gameObject.transform.position;
            melee.isStuck = false;
            charecter.canBypassJump = false;
            charecter.canJump = false;
            charecter.canDash = false;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            if (gScale != 0)
            {
                gScale = rb.gravityScale;
            }
            rb.gravityScale = 0;

        }
        else
        {
            return;
        }
    }
}
