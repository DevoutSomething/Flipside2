using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryPlayer : MonoBehaviour
{
    [SerializeField] private bool playerOnPlatform;
    public LayerMask playerLayer;
   
    void Start()
    {
        
    }

   
    void Update()
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
        }
        else
        {
            Debug.DrawRay(rayPos, Vector2.right * 3, Color.green);
            playerOnPlatform = false;
        }
    }
}
