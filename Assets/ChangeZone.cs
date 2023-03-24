using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    public bool isFinalUnFlip;
    public bool inZone;
    public bool saveZone;
    public int zoneNum;
    public ChangeZone changeZone;
    public ChangeZone changeFlipZone;
    public float roomCamSize;
    public Transform zoneRespawnLocation;
    public Transform camRestrictRightXUpY;
    public Transform camRestrictLeftxDownY;
    private GameObject player;
    private GameObject Camera;
    private GameObject gameManager;
    private PlayerHealth pHealth;
    void Start()
    {
        Camera = GameObject.Find("Player Camera");
        player = GameObject.Find("PlayerAnim");
        gameManager = GameObject.Find("GameManager");
        pHealth = player.GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        if (pHealth.currentRoom == zoneNum)
        {
            inZone = true;
        }
        else
        {
            inZone = false;
        }
        if (inZone)
        {
            Camera.GetComponent<CameraController>().lowerXLimit = camRestrictLeftxDownY.position.x;
            Camera.GetComponent<CameraController>().upperXLimit = camRestrictRightXUpY.position.x;
            Camera.GetComponent<CameraController>().lowerYLimit = camRestrictLeftxDownY.position.y;
            Camera.GetComponent<CameraController>().upperYLimit = camRestrictRightXUpY.position.y;
            Camera.GetComponent<CameraController>().WantedCamSize = roomCamSize;
            player.GetComponent<PlayerHealth>().respawnPoint = new Vector2(zoneRespawnLocation.position.x, zoneRespawnLocation.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFinalUnFlip)
        {

        }
        else if (gameManager.GetComponent<GameManager>().isFlipped == false)
        {
            if (collision.gameObject.name == "player" && inZone)
            {
                Debug.Log(" Collide with gate");
                changeZone.inZone = true;
                inZone = false;
                player.GetComponent<PlayerHealth>().currentRoom = changeZone.zoneNum;
                if (changeZone.saveZone)
                {
                    player.GetComponent<PlayerDataManager>().checkpoint = changeZone.zoneNum;
                    gameManager.GetComponent<SaveFunction>().SavePlayer();

                }
                //Destroy(gameObject);
            }
        }
        else if (gameManager.GetComponent<GameManager>().isFlipped == false)
        {
            if (collision.gameObject.name == "player" && inZone)
            {
                player.GetComponent<PlayerHealth>().currentRoom = changeZone.zoneNum;
                changeFlipZone.inZone = true;
                inZone = false;
                player.GetComponent<PlayerHealth>().currentRoom = changeZone.zoneNum;
                if (changeFlipZone.saveZone)
                {
                    player.GetComponent<PlayerDataManager>().checkpoint = changeZone.zoneNum;
                    gameManager.GetComponent<SaveFunction>().SavePlayer();

                }
            }

        }

        else
                {
            if(collision.gameObject.name == "player" && !inZone)
            {
                Debug.Log(" Collide with gate");
                changeZone.inZone = false;
                inZone = true;
                if (saveZone)
                {
                    player.GetComponent<PlayerDataManager>().checkpoint = zoneNum;
                    gameManager.GetComponent<SaveFunction>().SavePlayer();

                }
                //Destroy(gameObject);
            }
        }
    }
}
