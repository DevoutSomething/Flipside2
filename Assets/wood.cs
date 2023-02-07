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
        anim.SetBool("LogBreak", true);
        Debug.Log("pain");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            anim.SetBool("LogBreak2", true);
            anim.SetBool("LogBreak", false);
            Debug.Log("pain2");
        }
        if (Input.GetKeyDown("n"))
        {
            anim.SetBool("Gone", true);
            anim.SetBool("LogBreak2", false);
            Debug.Log("pain3");
        }
        if (Input.GetKeyDown("b"))
        {
            anim.SetBool("LogBreak", true);
            anim.SetBool("Gone", false);
            Debug.Log("pain4");
        }



    }
}
