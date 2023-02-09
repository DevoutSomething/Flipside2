using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttackManager : MonoBehaviour
{
    public float defaultForce;
    public float upwardsForce;
    public float movementTime = .1f;
    private bool meleeAttack;
    public GameObject player;
    public Animator meleeAnimator;
    public float attackCooldown;
    public float timeCantAction;
    public bool canAction;
    public bool canAttack;
    private Animator anim;
    public GameObject Nuke;
    public bool isStuck = false;
    private CharecterController charecterController;
    private Rigidbody2D rb;
    public bool canTransitionState;
    public float timeCantTransition;
    public GameObject gameManager;
    private float baseSpeed = 0f;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canAttack = true;
        canAction = true;
        anim = player.GetComponentInChildren<Animator>();
        charecterController = GetComponent<CharecterController>();
        gameManager = GameObject.Find("GameManager");
        baseSpeed = charecterController.moveSpeed;


    }
    private void Update()
    { 
        if (gameManager.GetComponent<GameManager>().isFlipped)
        {
             CheckInput();
        }
        checkDash();
    }
    private void CheckInput()
    {

        if (Input.GetButtonDown("Fire2") && canAction)    
        {
             
            meleeAttack = true;
            canAction = false;
            Debug.Log("working melee");
            canAction = false;
            StartCoroutine(AttackNoAction());
            if (charecterController.isGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
        }

        else if (Input.GetButtonDown("Fire2") && canTransitionState)
        {
            bool canTrans = anim.GetBool("CanTransition");
            bool forAttack = anim.GetBool("ForwardAttack");
            bool downAttack = anim.GetBool("DownwardAttack");
            bool upAttack = anim.GetBool("UpwardAttack");
            bool upAttackAir = anim.GetBool("UpwardAttackAir");
            bool jump = anim.GetBool("Jump");
            bool run = anim.GetBool("Run");
            bool sideDash = anim.GetBool("SideDash");
            bool upDash = anim.GetBool("UpDash");
            bool downDash = anim.GetBool("DownDash");
            bool crouch = anim.GetBool("Crouch");
            StartCoroutine(AttackNoAction());
           /* if(canTrans && forAttack && Input.GetAxis("Vertical") != 0)
            {
                anim.SetBool("UpwardAttack", true);
                meleeAnimator.SetTrigger("AttackUp");
            }
            if (canTrans && upAttack && meleeAttack && Input.GetAxis("Vertical") < 0 && !charecterController.isGrounded)
            {
                anim.SetBool("UpwardAttackAir", false);
                anim.SetBool("DownwardAttack", true);
                meleeAnimator.SetTrigger("AttackDown");
            }            */
          
        }
        else
        {
            meleeAttack = false;
            anim.SetBool("UpwardAttack", false);
            anim.SetBool("DownwardAttack", false);
            anim.SetBool("ForwardAttack", false);                                               
            anim.SetBool("ForwardAttack", false);
            //anim.SetBool("UpwardAttackAir", false);   

        }


        if (meleeAttack && Input.GetAxis("Vertical") > 0 && !charecterController.isGrounded)
        {
            
            anim.SetBool("UpwardAttackAir", true);
            meleeAnimator.SetTrigger("AttackUpAir");
           
           
        }
        
       
        
        
        if (meleeAttack && Input.GetAxis("Vertical") < 0 && !charecterController.isGrounded)
        {
           
            anim.SetBool("DownwardAttack", true);
            meleeAnimator.SetTrigger("AttackDown");
        }
        if(meleeAttack && Input.GetAxis("Vertical") == 0 || meleeAttack && (Input.GetAxis("Vertical") < 0 && charecterController.isGrounded))
        {
            
            anim.SetBool("ForwardAttack", true);
            meleeAnimator.SetTrigger("AttackSide");
        }
        if(Input.GetAxis("Horizontal") != 0 && charecterController.isGrounded && meleeAttack == true)
        {
            Debug.Log("archer left me");
            //ResetAnim();
            
            //anim.SetBool("UpwardAttack", false);
            anim.ResetTrigger("UpwardAttack");
            anim.SetBool("Run", true);
            //meleeAnimator.SetBool("Idle2",true);
        }
        if (Input.GetButtonDown("Jump") && charecterController.isGrounded)
        {
            Debug.Log("Archer saved me");
            //ResetAnim();
            anim.SetBool("Jump", true);
           // meleeAnimator.SetBool("Idle2", true);
        }
        if (Input.GetButtonUp("Dash") && !charecterController.isGrounded)
        {
            anim.SetBool("Jump", false);
            //meleeAnimator.SetBool("Idle2", true);

        }
        if (meleeAttack && Input.GetAxis("Vertical") > 0 && charecterController.isGrounded)
        {

            anim.SetBool("UpwardAttack", true);
            meleeAnimator.SetTrigger("AttackUp");


        }
        
        if (charecterController.isGrounded && meleeAttack == false)
        {

            resetBadAnim();
          //  Debug.Log("help me");

        }

    }

    private void checkDash()
    {
        if (Input.GetButtonUp("Dash") && Input.GetAxis("Vertical") == 0 && meleeAttack == false && gameManager.GetComponent<GameManager>().isFlipped == false)
        {

            anim.SetTrigger("SideDash");
        }
        if (Input.GetButtonUp("Dash") && Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0 && gameManager.GetComponent<GameManager>().isFlipped == false)
        {
            anim.SetTrigger("SideDash");
        }
        if (Input.GetButtonUp("Dash") && Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0 && gameManager.GetComponent<GameManager>().isFlipped == false)
        {
            anim.SetTrigger("SideDash");
        }
        if (Input.GetButtonUp("Dash") && Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0 && gameManager.GetComponent<GameManager>().isFlipped == false)
        {
            anim.SetTrigger("SideDash");
        }
        if (Input.GetButtonUp("Dash") && Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0 && gameManager.GetComponent<GameManager>().isFlipped == false)
        {
            anim.SetTrigger("SideDash");
        }
        if (Input.GetButtonUp("Dash") && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0 && gameManager.GetComponent<GameManager>().isFlipped == false)
        {
            anim.SetTrigger("UpDash");
            if (charecterController.isGrounded)
            {
                anim.ResetTrigger("UpDash");
                anim.SetTrigger("SideDash");
            }
        }
        if (Input.GetButtonUp("Dash") && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0 && gameManager.GetComponent<GameManager>().isFlipped == false)
        {

            anim.SetTrigger("DownDash");
            if (charecterController.isGrounded)
            {
                anim.ResetTrigger("DownDash");
                
            }
        }

        if (Input.GetAxis("Vertical") < 0 && charecterController.isGrounded && meleeAttack == false && Input.GetButtonDown("Crouch"))
        {

            anim.SetBool("Crouch", true);
            charecterController.moveSpeed = 0.1f;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            anim.SetBool("Crouch", false);
            charecterController.moveSpeed = baseSpeed;
        }
        if (charecterController.isGrounded && meleeAttack == false)
        {

            resetBadAnim();
            //  Debug.Log("help me");

        }
    }

    private void resetBadAnim ()
    {
        //meleeAttack = false;
        //anim.SetBool("DownwardAttack", false);
        //anim.SetBool("UpwardAttackAir", false);
        //anim.SetBool("idle", true);
        anim.ResetTrigger("DownwardAttack");
        anim.ResetTrigger("UpwardAttackAir");
        meleeAnimator.ResetTrigger("AttackUpAir");
        meleeAnimator.ResetTrigger("AttackDown");
        anim.ResetTrigger("UpDash");
        anim.ResetTrigger("DownDash");
        //meleeAnimator.SetBool("Idle2", true);

    }

    private IEnumerator AttackNoAction()
    {
        yield return new WaitForSecondsRealtime(timeCantAction);
        canAction = true;

    }
    private IEnumerator AttackTransition()
    {
        yield return new WaitForSecondsRealtime(timeCantTransition);
        canTransitionState = true;
    }
    //archer fucked up
    //archer fucked up again
}
