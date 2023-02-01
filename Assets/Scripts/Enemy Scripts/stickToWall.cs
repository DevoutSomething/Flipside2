using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickToWall : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "meleeAnim")
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            player.GetComponent<meleeAttackManager>().canAction = false;
            player.GetComponent<CharecterController>().canBypassJump = true;
            player.GetComponent<meleeAttackManager>().isStuck = true;
            player.GetComponent<CharecterController>().isJumping = false;
            player.GetComponent<CharecterController>().isActuallyDashing = false;
            player.GetComponent<CharecterController>().isDashing = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            rb.velocity = new Vector3(0, 0, 0);
            Debug.Log("playerHitStickWall");
            return;
        }
    }
}
