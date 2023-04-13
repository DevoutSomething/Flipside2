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
    public Transform zoneRespawnLocationFlipped;

    public Transform camRestrictRightXUpY;
    public Transform camRestrictLeftxDownY;
    public Transform camRestrictRightXUpYFlipped;
    public Transform camRestrictLeftxDownYFlipped;
    private GameObject player;
    private GameObject Camera;
    private GameObject gameManager;
    private PlayerHealth pHealth;
    public Transform tempSpawnPoint;
    
    void Start()
    {
        Camera = GameObject.Find("Player Camera");
        player = GameObject.Find("PlayerAnim");
        gameManager = GameObject.Find("GameManager");
        pHealth = player.GetComponent<PlayerHealth>();
        if (camRestrictLeftxDownYFlipped == null)
        {
            camRestrictLeftxDownYFlipped = camRestrictLeftxDownY;
        }
        if (camRestrictRightXUpYFlipped == null)
        {
            camRestrictRightXUpYFlipped = camRestrictRightXUpY;
        }
        if (zoneRespawnLocationFlipped == null)
        {
            zoneRespawnLocationFlipped = zoneRespawnLocation;
        }
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
        if (inZone && gameManager.GetComponent<GameManager>().isFlipped != true && player.GetComponent<PlayerHealth>().respawnPoint != new Vector2(zoneRespawnLocation.position.x, zoneRespawnLocation.position.y))
        {
            Camera.GetComponent<CameraController>().lowerXLimit = camRestrictLeftxDownY.position.x;
            Camera.GetComponent<CameraController>().upperXLimit = camRestrictRightXUpY.position.x;
            Camera.GetComponent<CameraController>().lowerYLimit = camRestrictLeftxDownY.position.y;
            Camera.GetComponent<CameraController>().upperYLimit = camRestrictRightXUpY.position.y;
            Camera.GetComponent<CameraController>().WantedCamSize = roomCamSize;
            player.GetComponent<PlayerHealth>().respawnPoint = new Vector2(zoneRespawnLocation.position.x, zoneRespawnLocation.position.y);
            Debug.Log("no flip spawn point");
        }
        if (inZone && gameManager.GetComponent<GameManager>().isFlipped == true && player.GetComponent<PlayerHealth>().respawnPoint != new Vector2(zoneRespawnLocationFlipped.position.x, zoneRespawnLocationFlipped.position.y))
        {
            Camera.GetComponent<CameraController>().lowerXLimit = camRestrictLeftxDownY.position.x;
            Camera.GetComponent<CameraController>().upperXLimit = camRestrictRightXUpY.position.x;
            Camera.GetComponent<CameraController>().lowerYLimit = camRestrictLeftxDownY.position.y;
            Camera.GetComponent<CameraController>().upperYLimit = camRestrictRightXUpY.position.y;
            Camera.GetComponent<CameraController>().WantedCamSize = roomCamSize;
            player.GetComponent<PlayerHealth>().respawnPoint = new Vector2(zoneRespawnLocationFlipped.position.x, zoneRespawnLocationFlipped.position.y);
            Debug.Log("flip spawn point");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player" && inZone && isFinalUnFlip)
        {
           gameManager.GetComponent<GameManager>().isFlipped = true;
            gameManager.GetComponent<GameManager>().SpawnFlippedEnemies();
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

        if (gameManager.GetComponent<GameManager>().isFlipped == true)
        {
            if(collision.gameObject.name == "player" && !inZone)
            {
                Debug.Log(" Collide with gate");
                changeZone.inZone = false;
                inZone = true;
                player.GetComponent<PlayerHealth>().currentRoom = zoneNum;
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
