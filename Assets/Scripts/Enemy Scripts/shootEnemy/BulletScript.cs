using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("obstacle") || collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Destroy(gameObject);
        }
    }
}
