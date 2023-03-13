using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public bool playerOn;
    public LayerMask playerLayer;

    private void Start()
    {
        enemy = gameObject.transform.parent.gameObject;
        player = GameObject.Find("player");
    }
    private void FixedUpdate()
    {
        Vector2 rayPos;
        rayPos = transform.position;
        rayPos.x = transform.position.x - 1.5f;
        rayPos.y = transform.position.y + 1f;
        RaycastHit2D raycastPlayer = Physics2D.Raycast(rayPos, Vector2.right, 3, playerLayer);
        if (raycastPlayer.collider != null)
        {
            playerOn = true;
            Debug.DrawRay(rayPos, Vector2.right * raycastPlayer.distance, Color.red);
        }
        else
        {
            playerOn = false;
            Debug.DrawRay(rayPos, Vector2.right * 3, Color.green);
        }
        if (playerOn)
        {
            float moveSpeed = enemy.GetComponent<EnemyControllerCow>().speed;
            if (enemy.GetComponent<EnemyControllerCow>().facingRight)
            {
                player.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            if (!enemy.GetComponent<EnemyControllerCow>().facingRight)
            {
                player.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
            {
            player.gameObject.transform.parent = gameObject.transform;
            }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            player.gameObject.transform.parent = null;
        }
    }
}