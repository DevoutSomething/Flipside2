using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    

    [Header("Edits")]
    public float rollSpeed;
    public float delayTime;

    [Header("Checks")]
    [SerializeField]private bool rolling;
    [SerializeField] private bool canRoll;
    private Animator me;
    private Rigidbody2D rb;

    public Sprite StartSprite;
    public Sprite RollSprite;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        me = gameObject.GetComponent<Animator>();
        rolling = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    private void Update()
    {
        if (canRoll)
        {
            rb.velocity = new Vector2(rollSpeed, rb.velocity.y);
        }
       
    }
    public void runBarrel()
    {
        Debug.Log("Player Hit Activation Point");
        rolling = true;
        StartCoroutine(barrelStartRoll());
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public IEnumerator barrelStartRoll()
    {
        me.SetBool("IsStart", true);
        yield return new WaitForSeconds(delayTime);
        me.SetBool("IsStart", false);
        me.SetBool("RollingBarrel", true);
        canRoll = true;
        rb.gravityScale = 1;
    }
}
