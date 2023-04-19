using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
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
        animator = GetComponent<Animator>();
        spriteRendrer = GetComponent<SpriteRenderer>();
        //spriteRendrer.enabled = false;
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
            SetNextPoint(currentPoint.up);
            direction = 1;
            animationSet = false; 
        }
        else if (currentPoint.right != null && currentPoint.right != prevPoint)
        {
            SetNextPoint(currentPoint.right);
            direction = 2;
            animationSet = false;
        }
        else if (currentPoint.down != null && currentPoint.down != prevPoint)
        {
            SetNextPoint(currentPoint.down);
            direction = 3;
            animationSet = false;
        }
        else if (currentPoint.left != null && currentPoint.left != prevPoint)
        {
            SetNextPoint(currentPoint.left);
            direction = 4;
            animationSet = false;
        }
    }

    void CheckInput()
    {
        if(y > 0.5f)
        {
            if(currentPoint.up != null)
            {
                SetNextPoint(currentPoint.up);
                direction = 1;
                animationSet = false;
            }
        }
        if(x > 0.5f)
        {
            if (currentPoint.right != null)
            {
                SetNextPoint(currentPoint.right);
                direction = 2;
                animationSet = false;
            }
        }
        if(y < -0.5f)
        {
            if (currentPoint.down != null)
            {
                SetNextPoint(currentPoint.down);
                direction = 3;
                animationSet = false;
            }
        }
        if(x < -0.5f)
        {
            if (currentPoint.left != null)
            {
                SetNextPoint(currentPoint.left);
                direction = 4;
                animationSet = false;
            }
        }
    }

    void CheckMapPoint()
    {
         if(currentPoint.isWarp && !currentPoint.hasWarped)
        {
                if(direction != 0)
            {
                direction = 0;
                SetAnimation();
            }
                if (currentPoint.autoWarp)
            {
                StartCoroutine(TeleportPlayer(teleportTime));
            }
        }
        if (currentPoint.isCorner && currentPoint.isWarp)
        {
            if (direction != 0)
            {
                direction = 0;
                SetAnimation();
            }
            CheckInput();
            SelectLevel();
        }
        if (currentPoint.isCorner)
        {
            AutoMove();
        }
        else
        {
            if(direction != 0)
            {
                direction = 0;
                SetAnimation();
            }
            CheckInput();
            SelectLevel();
        }
    }
    void SetAnimation()
    {
        animationSet = true;
        switch (direction)
        {
            case 0:
                animator.Play("Idle");
                break;
            case 1:
                animator.Play("Up");
                break;
            case 2:
                animator.Play("Right");
                break;
            case 3:
                animator.Play("Down");
                break;
            case 4:
                animator.Play("left");
                break;  


        }
    }
    void SetNextPoint(MapPoint nextPoint)
    {
        //playerSprite.localPosition = Vector2.zero;
        prevPoint = currentPoint;
        currentPoint = nextPoint;


    }
    void SetPlayerPos()
    {
        if(DataManager.instance.gameData.currentLevelName == currentPoint.name)
        {
            transform.position = startPoint.transform.position;
            spriteRendrer.enabled = true;
            currentPoint = startPoint;
            prevPoint = currentPoint;
            canMove = true; 
        }
        else
        {
            foreach(MapPoint point in allPoints)
            {
                if (point.isLevel)
                {
                    if (point.sceneToLoad == DataManager.instance.gameData.currentLevelName)
                    {
                        transform.position = point.transform.position;
                        spriteRendrer.enabled = true;
                        currentPoint = point;
                        prevPoint = currentPoint;
                        canMove = true; 
                    }
                }
            }
        }      
    }
    public void GetMovement()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        x = movement.x;
        y = movement.y; 
    }
    public void SelectLevel()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(currentPoint != null)
            {
                if(!currentPoint.isLocked && currentPoint.isLevel)
                {
                    DataManager.instance.gameData.currentLevelName = currentPoint.sceneToLoad;
                    DataManager.instance.SaveGameData();
                    SceneManager.LoadScene(currentPoint.sceneToLoad);
                }
                else if (currentPoint.isWarp && !currentPoint.autoWarp)
                {
                    StartCoroutine(TeleportPlayer(teleportTime));
                }
            }
        }
    }
    IEnumerator TeleportPlayer(float time)
    {
        currentPoint.hasWarped = true;

        canMove = false;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(spriteRendrer.color.r, spriteRendrer.color.g, spriteRendrer.color.b, Mathf.Lerp(1, 0, t));
            spriteRendrer.color = newColor;
            yield return null; 

        }

        transform.position = currentPoint.warpPoint.transform.position;

        yield return new WaitForSeconds(time);

        for(float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(spriteRendrer.color.r, spriteRendrer.color.g, spriteRendrer.color.b, Mathf.Lerp(0, 1, t));
            spriteRendrer.color = newColor;
            yield return null;
        }

        currentPoint = currentPoint.warpPoint;
        currentPoint.hasWarped = true;
        canMove = true;


        
    }













}
