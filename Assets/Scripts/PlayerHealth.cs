using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int Health = 1;
    public int TempHealth = 1;
    
    public Animator playerAnimator;
    public float deathTimer;
    private float deathTimerTEMP;
    public GameObject spawnPoint;
    public bool death;
    public Vector2 respawnPoint;
    private meleeAttackManager MeleeAttackManager;
    public GameObject attackManagerObject;
    public int currentRoom;
    public GameObject Player;
    private GameObject gameManager;
    private CharecterController man;
    private void Start()
    {
        Player = GameObject.Find("player");
        gameManager = GameObject.Find("GameManager");
        playerAnimator = GetComponent<Animator>();
        MeleeAttackManager = Player.GetComponent<meleeAttackManager>();
        deathTimerTEMP = deathTimer;
        GameObject[] Rooms = GameObject.FindGameObjectsWithTag("room");
        foreach (GameObject room in Rooms)
        {
            if (room.GetComponent<ChangeZone>().zoneNum == currentRoom)
            {
                if (gameManager.GetComponent<GameManager>().isFlipped)
                {
                    Player.transform.position = room.GetComponent<ChangeZone>().zoneRespawnLocationFlipped.position;
                }
                else
                {
                    Player.transform.position = room.GetComponent<ChangeZone>().zoneRespawnLocation.position;
                }
            }
        }
    }
    private void Update()
    {
        if (TempHealth <= 0 && death != true)
        {
            death = true;
            StartDeathTimer();
            playerAnimator.SetBool("Death", true);
            playerAnimator.SetBool("Run", false);
            playerAnimator.SetBool("Jump", false);
        }
        if (death && deathTimerTEMP >= 0)
        {
            deathTimerTEMP -= Time.fixedDeltaTime;
        }
        if(deathTimerTEMP <= 0 && death)
        {
            RespawnPlayer();
        }
    }
    void StartDeathTimer()
    {
        MeleeAttackManager.canAction = false;
        death = true;
        deathTimerTEMP = deathTimer;
    }
    void RespawnPlayer()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject.Find("GameManager").GetComponent<TimeManager>().ResetTime();
        gameManager.GetComponent<roomReset>().restartroom(currentRoom);
        death = false;
        playerAnimator.SetBool("Death", false);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        man = Player.GetComponent<CharecterController>();
        man.isJumping = false;
        man.isDashing = false;
        man.isActuallyDashing = false;
        man.canDash = true;
        man.canResetJump = true;
        man.StopAllCoroutines();
           



        GameObject[] Rooms = GameObject.FindGameObjectsWithTag("room");
        foreach (GameObject room in Rooms)
        {
            if (room.GetComponent<ChangeZone>().zoneNum == currentRoom)
            {
                Player.transform.position = respawnPoint;
            }
        }
        TempHealth = 1;
        MeleeAttackManager.canAction = true;
        
        GameObject[] enemies = attackManagerObject.GetComponent<meleeAttack>().enemiesKilled.ToArray();
        foreach (GameObject Enemy in enemies)
        {
        Enemy.GetComponent<enemyHealth>().PlayerDied();
        }
        GameObject[] movingBlocks = GameObject.FindGameObjectsWithTag("movingBlock");
        foreach (GameObject Block in movingBlocks)
        {
            Block.GetComponent<ObstacleMove>().Reset();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("obstacle"))
        {
            TempHealth -= 1;
            Debug.Log("colision");
        }
    }
  private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("obstacle"))
        {
            TempHealth -= 1;
        }
    }
   
}
