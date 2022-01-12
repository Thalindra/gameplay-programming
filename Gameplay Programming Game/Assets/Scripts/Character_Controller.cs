using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float floMoveSpeed = 5;
    public Transform tranMovePoint;
    public Transform tranDirectionIndicator;
    public LayerMask mskCollide;
    public bool bolCanMove = true;

    public float floHookLength = 8f;
    public GameObject Hook;
    public bool bolCanFire = true;


    void Start()
    {
        tranMovePoint.parent = null;
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            CheckLastDirection();
        }

        if (Input.GetButtonDown("Fire1") && bolCanMove == true)
        {
            Hookshot();
        }

        float movementAmount = floMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, tranMovePoint.position, movementAmount);

        if (Vector3.Distance(transform.position, tranMovePoint.position) <= 0.05f && bolCanMove == true)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                Move(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0));
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                Move(new Vector3(0, Input.GetAxisRaw("Vertical"), 0));
            }
        }
    }

    private void Move(Vector3 direction)
    {
        Vector3 newPosition = tranMovePoint.position + direction;
        if (!Physics2D.OverlapCircle(newPosition, 0.2f, mskCollide))
        {
            tranMovePoint.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook") && bolCanMove == false)
        {
            bolCanMove = true;
        }
    }

    private void CheckLastDirection()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            tranDirectionIndicator.position = new Vector3(-0.5f, 0f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            tranDirectionIndicator.position = new Vector3(0.5f, 0f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            tranDirectionIndicator.position = new Vector3(0f, 0.5f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            tranDirectionIndicator.position = new Vector3(0f, -0.5f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 270f);
        }
    }
    private void Hookshot()
    {
        bolCanMove = false;
        Instantiate(Hook, tranDirectionIndicator.position, tranDirectionIndicator.rotation);
    }
}
