using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public bool damageable = true;
    public bool giveUpwardForce = true;
    public bool bounceCollide;
    public bool giveDashReset;
    public int health = 1;
    public float bounceMult;
    private int currentHealth;
    public bool makePlayerDash;
    public bool onlyFirstSide;
    private GameObject gameManager;
    private void Start()
    {
        currentHealth = health;
        gameManager = GameObject.Find("GameManager");
    }
    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().isFlipped && onlyFirstSide)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        } 
    }
    public void Damage(int amount)
    {
        //checks if enemy can take damage
        if(damageable && currentHealth > 0)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //make death animation
                gameObject.SetActive(false);
            }
        }
    }
    public void PlayerDied()
    {
        currentHealth = health;
        gameObject.SetActive(true);
        if (gameObject.GetComponent<EnemyControllerAlpaca>() != null)
        {
            gameObject.GetComponent<EnemyControllerAlpaca>().Respawn();
        }
    }
}
