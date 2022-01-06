using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float floMoveSpeed = 5;
    public Transform tranMovePoint;
    public Transform tranDirectionIndicator;
    public Transform tranHookShot;
    public LayerMask mskCollide;
    public bool bolCanMove = true;
    private int intLastDir = 2;

    public float floHookLength = 8f;
    private RaycastHit2D hit;
    public GameObject Hook;


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

        if (Input.GetButtonDown("Fire1"))
        {
            //for raycast hookshot
            //Hookshot();
            Instantiate(Hook, tranDirectionIndicator.position, tranDirectionIndicator.rotation);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook") && bolCanMove == true)
        {
            bolCanMove = false;
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            intLastDir = 1;
            tranDirectionIndicator.position = new Vector3(-0.5f, 0f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            intLastDir = 2;
            tranDirectionIndicator.position = new Vector3(0.5f, 0f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            intLastDir = 3;
            tranDirectionIndicator.position = new Vector3(0f, 0.5f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            intLastDir = 4;
            tranDirectionIndicator.position = new Vector3(0f, -0.5f, 0f) + transform.position;
            tranDirectionIndicator.eulerAngles = new Vector3(0f, 0f, 270f);
        }
    }

    //Raycast hookshot
    //private void Hookshot()
    //{
    //    if (intLastDir == 1 || intLastDir == 2)
    //    {
    //        hit = Physics2D.Raycast(transform.position, tranDirectionIndicator.right, floHookLength, mskCollide);
    //    }
    //    else
    //    {
    //        hit = Physics2D.Raycast(transform.position, tranDirectionIndicator.up, floHookLength, mskCollide);
    //    }

    //    if(hit.collider)
    //    {
    //        Debug.Log("hit wall");
    //        Vector3 point = hit.point;
    //        transform.position = Vector3.MoveTowards(transform.position, point, floMoveSpeed * Time.deltaTime);
    //        tranHookShot.position = point;
    //    }
    //}
}
