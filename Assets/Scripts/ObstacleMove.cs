using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public float speed;
    public bool movingToPos1;
    private Vector2 pos1;
    private Vector2 pos2;
    public bool moveX;
    public bool moveY;
    [SerializeField] private bool playerOnPlatform;
    public GameObject player;
    public LayerMask playerLayer;
    public int room;
    public bool moveWithPlayer;
    private bool hasTouchedPlayer = false;
    private Vector2 startPos;
    private void Start()
    {
        startPos = transform.position;
        movingToPos1 = true;
        pos1 = object1.transform.position;
        pos2 = object2.transform.position;
    }
    void FixedUpdate()
    {
        if (GameObject.Find("player").GetComponent<PlayerHealth>().currentRoom == room)
        {
            if (!moveWithPlayer || hasTouchedPlayer)
            {
                float newSpeed;
                if (moveX || moveY)
                {
                    newSpeed = speed;
                }
                if (moveX && moveY)
                {
                    newSpeed = speed / 2;
                }
                if (moveY)
                {
                    if (movingToPos1)
                    {
                        Vector3 targetPosition = pos1;
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    }
                    if (!movingToPos1)
                    {
                        Vector3 targetPosition = pos2;
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    }
                    if (transform.position.y >= pos1.y - 0.1)
                    {
                        movingToPos1 = false;
                    }
                    if (transform.position.y <= pos2.y + 0.1)
                    {
                        movingToPos1 = true;
                    }
                }
                if (moveX)
                {
                    if (movingToPos1)
                    {
                        Vector3 targetPosition = pos1;
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                        if (playerOnPlatform && moveWithPlayer)
                        {
                            Vector3 playerOffset = transform.position - player.transform.position;
                            player.transform.position = Vector3.MoveTowards(transform.position - playerOffset, targetPosition - playerOffset, speed * Time.deltaTime);
                        }
                    }
                    if (!movingToPos1)
                    {
                        Vector3 targetPosition = pos2;
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                        if (playerOnPlatform && moveWithPlayer)
                        {
                            Vector3 playerOffset = transform.position - player.transform.position;
                            player.transform.position = Vector3.MoveTowards(transform.position - playerOffset, targetPosition - playerOffset, speed * Time.deltaTime);
                        }
                    }
                    if (transform.position.x >= pos1.x - 0.1)
                    {
                        movingToPos1 = false;
                    }
                    if (transform.position.x <= pos2.x + 0.1)
                    {
                        movingToPos1 = true;
                    }
                }
            }
        }
        if (moveWithPlayer)
        {
            Vector2 rayPos;
            rayPos = transform.position;
            rayPos.x = transform.position.x - 1.5f;
            rayPos.y = transform.position.y + 1f;
            RaycastHit2D raycastPlayer = Physics2D.Raycast(rayPos, Vector2.right, 3, playerLayer);
            if (raycastPlayer.collider != null)
            {
                Debug.DrawRay(rayPos, Vector2.right * raycastPlayer.distance, Color.red);
                playerOnPlatform = true;
                hasTouchedPlayer = true;
            }
            else
            {
                Debug.DrawRay(rayPos, Vector2.right * 3, Color.green);
                playerOnPlatform = false;
            }
        }
        
    }
    public void Reset()
    {
        transform.position = startPos;
        movingToPos1 = true;
        hasTouchedPlayer = false;
    }

}
