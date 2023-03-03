using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] MapPoint startPoint = null;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float teleportTime = 3f;
    [SerializeField] Transform playerSprite = null;

    MapPoint[] allPoints;
    MapPoint prevPoint, currentPoint;

    Animator animator;
    SpriteRenderer spriteRendrer;

    float x, y;
    bool canMove = true;
    int direction; 
    bool animationSet = false;
    Vector2 movement;


    void Awake()
    {
        allPoints = FindObjectsOfType<MapPoint>();
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRendrer = GetComponentInChildren<SpriteRenderer>();
        spriteRendrer.enabled = false;
        canMove = false;
        SetPlayerPos();
    }
    void Update()
    {
      if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f)
            {
                CheckMapPoint();
            }
            else
            {
                if (!animationSet)
                    SetAnimation();
            }
        } 
    }
    void FixedUpdate()
    {
        GetMovement(); 
    }

    void AutoMove()
    {
        if (currentPoint.up != null && currentPoint.up != prevPoint)
        {

        }
        else if (currentPoint.right != null && currentPoint.right != prevPoint)
        {

        }
        else if (currentPoint.down != null && currentPoint.down != prevPoint)
        {

        }
        else if (currentPoint.left != null && currentPoint.left != prevPoint)
        {

        }
    }

    void CheckInput()
    {
        if(y > 0.5f)
        {

        }
        if(x > 0.5f)
        {

        }
        if(y < -0.5f)
        {

        }
        if(x < -0.5f)
        {

        }
    }

    void CheckMapPoint()
    {

    }
    void SetAnimation()
    {

    }
    void SetNextPoint(MapPoint nextPoint)
    {

    }
    void SetPlayerPos()
    {

    }
    public void GetMovement()
    {

    }
    public void SelectLevel()
    {

    }














}
