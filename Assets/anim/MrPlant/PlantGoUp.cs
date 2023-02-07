using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGoUp : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject gameManger;
    public Animator plantAnim;
    public float distance = 6f;
    [SerializeField] private bool attacking;
    public float timeNoTarget = .5f;
    public bool isRotRight;
    public bool isRotLeft;
    public bool isRotDown;
    void Start()
    {
        attacking = false;
        gameManger = GameObject.Find("GameManger");
    }
    private void Update()
    {
        if (isRotRight)
        {
            RaycastHit2D raycastPlantRight = Physics2D.Raycast(transform.position, Vector2.right, distance, playerLayer);
            if (raycastPlantRight.collider != null)
            {
                Vector2 plantStartPoint;
                plantStartPoint = transform.position;
                Debug.DrawRay(plantStartPoint, Vector2.right * raycastPlantRight.distance, Color.red);
                if (!attacking)
                {
                    StartCoroutine(SendAttackUp());
                }
            }
            else
            {
                Debug.DrawRay(transform.position, Vector2.right * distance, Color.green);
            }
        }
       
        if (isRotLeft)
        {
            RaycastHit2D raycastPlantLeft = Physics2D.Raycast(transform.position, Vector2.left, distance, playerLayer);
            if (raycastPlantLeft.collider != null)
            {
                Vector2 plantStartPoint;
                plantStartPoint = transform.position;
                Debug.DrawRay(plantStartPoint, Vector2.left * raycastPlantLeft.distance, Color.red);
                if (!attacking)
                {
                    StartCoroutine(SendAttackUp());
                }
            }
            else
            {
                Debug.DrawRay(transform.position, Vector2.left * distance, Color.green);
            }
        }
        if (isRotDown)
        {
            RaycastHit2D raycastPlantDown = Physics2D.Raycast(transform.position, Vector2.down, distance, playerLayer);
            if (raycastPlantDown.collider != null)
            {
                Vector2 plantStartPoint;
                plantStartPoint = transform.position;
                plantStartPoint.y += 3;
                Debug.DrawRay(plantStartPoint, Vector2.down * raycastPlantDown.distance, Color.red);
                if (!attacking)
                {
                    StartCoroutine(SendAttackUp());
                }
            }
            else
            {
                Debug.DrawRay(transform.position, Vector2.up * distance, Color.green);
            }
        }
        if (!isRotLeft && !isRotRight && !isRotDown)
        {
            RaycastHit2D raycastPlant = Physics2D.Raycast(transform.position, Vector2.up, distance, playerLayer);
            if (raycastPlant.collider != null)
            {
                Vector2 plantStartPoint;
                plantStartPoint = transform.position;
                plantStartPoint.y -= 3;
                Debug.DrawRay(plantStartPoint, Vector2.up * raycastPlant.distance, Color.red);
                if (!attacking)
                {
                    StartCoroutine(SendAttackUp());
                }
            }
            else
            {
                Debug.DrawRay(transform.position, Vector2.up * distance, Color.green);
            }
        } 
    }

    public IEnumerator SendAttackUp()
    {
        attacking = true;
        plantAnim.SetTrigger("plantussy");
        yield return new WaitForSecondsRealtime(timeNoTarget);
        attacking = false;
    }
}
