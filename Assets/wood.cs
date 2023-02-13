using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wood : MonoBehaviour
{
    public GameObject Log;
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        bool logBreak = anim.GetBool("LogBreak");
        bool logBreak2 = anim.GetBool("LogBreak2");
        bool logBreakgone = anim.GetBool("Gone");
        Debug.Log("pain");
        anim.SetBool("LogBreak", false);
    }

    // Update is called once per frame
    void Update()
    {
        



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("LogBreak", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("LogBreak", false);
    }
}
