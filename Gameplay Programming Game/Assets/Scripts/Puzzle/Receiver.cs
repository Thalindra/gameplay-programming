using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    // Template script for all 'receptor' objects to pull from

    public GameObject[] objTransmitters; // Assign effectors to the object in the inspectors
    public bool isLocked = true; // Used to indicate whether all effectors are active or not
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer sprRender;

    // Check if all effectors conditions have been met or not
    protected void ConditionCheck()
    {
        int activeCount = 0;
        foreach (GameObject t in objTransmitters) //Checks each object in the array
        {
            if (t.gameObject == null)
            {
                activeCount++;
                if (activeCount >= objTransmitters.Length) //once the points are equal to the amount of objects in the array, unlocks the receptor
                {
                    isLocked = false;
                }
                else
                {
                    isLocked = true;
                }
            }
            else if (t.gameObject.tag != "Enemy")
            {
                if (t.gameObject.GetComponent<Transmitter>().bolActivated) //if an object's condition is met, adds a point towards the receptor unlock
                {
                    activeCount++;
                }
                if (activeCount >= objTransmitters.Length) //once the points are equal to the amount of objects in the array, unlocks the receptor
                {
                    isLocked = false;
                }
                else
                {
                    isLocked = true;
                }
            }
        }
    }

    // Get the game object's box collider and sprite renderer
    protected void GetCollSprite()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        sprRender = gameObject.GetComponent<SpriteRenderer>();
    }

    protected void Unlock()
    {
        if (!isLocked) //If the condition is met, disable the object's sprite renderer and collider (can be changed later for changing sprites)
        {
            boxCollider.enabled = false;
            sprRender.enabled = false;
        }
        else if (isLocked && boxCollider.enabled == false && sprRender.enabled == false) // If the condition is not met, and the components are disabled, re-enable them (prevents it enabling every frame)
        {
            boxCollider.enabled = true;
            sprRender.enabled = true;
        }
    }

    // Check if any effectors have been assigned to the object, if not, automatically unlock it (unless it's a hazard, as it's assumed you'd want it to stay on)
    protected void EffectorCheck()
    {
        if (objTransmitters.Length <= 0)
        {
            if (isLocked && !gameObject.CompareTag("Hazard"))
            {
                isLocked = false;
                Debug.Log(gameObject.name + " was unlocked. " + "No Effectors connected.");
            }
        }
    }
}
