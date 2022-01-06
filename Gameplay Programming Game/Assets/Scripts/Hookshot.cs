using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public LayerMask mskCollide;
    public bool bolReturn = false;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(bolReturn)
        {
            rb.velocity = transform.right * (speed - (speed * 2));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, mskCollide))
        {
            bolReturn = true;
        }

        if(collision.CompareTag("Player") && bolReturn == true)
        {
            Destroy(gameObject);
        }
    }
}
