using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Receiver
{
    public Sprite sprOpenSprite;
    public Sprite sprClosedSprite;
    public bool isInverse;
    BoxCollider2D boxDoorCollider;

    void Start()
    {
        //Get this object's collider and sprite renderer (this will need to be changed later when we implement the sprites)
        GetCollSprite();
        EffectorCheck();
        boxDoorCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Each frame, check if the unlock conditions have been met, if so, unlock
        ConditionCheck();
        GateOpen();
    }

    // Modified version of the Unlock() Receptor function that changes sprites instead of disabling them
    void GateOpen()
    {
        if (isInverse == false)
        {
            if (!isLocked && boxDoorCollider.enabled == true && sprRender.sprite == sprClosedSprite) //If the condition is met, change the object's sprite to open and disable the collider (can be changed later for changing sprites)
            {
                boxDoorCollider.enabled = false;
                if (sprOpenSprite != null)
                {
                    sprRender.sprite = sprOpenSprite;
                    sprRender.sortingLayerName = "Default";
                }

            }
            else if (isLocked && sprRender.enabled == false && sprRender.sprite == sprOpenSprite) // If the condition is not met, enable box collider and change to closed sprite (if they aren't already)
            {
                boxDoorCollider.enabled = true;
                if (sprClosedSprite != null)
                {
                    sprRender.sprite = sprClosedSprite;
                    sprRender.sortingLayerName = "Collision";
                }
            }
        }
        else if (isInverse == true)
        {
            if (!isLocked && boxDoorCollider.enabled == false && sprRender.sprite == sprOpenSprite) //If the condition is met, change the object's sprite to open and disable the collider (can be changed later for changing sprites)
            {
                boxDoorCollider.enabled = true;
                if (sprOpenSprite != null)
                {
                    sprRender.sprite = sprClosedSprite;
                    sprRender.sortingLayerName = "Collision";
                }
            }
            else if (isLocked && boxDoorCollider.enabled == true && sprRender.sprite == sprClosedSprite) // If the condition is not met, enable box collider and change to closed sprite (if they aren't already)
            {
                boxDoorCollider.enabled = false;
                if (sprClosedSprite != null)
                {
                    sprRender.sprite = sprOpenSprite;
                    sprRender.sortingLayerName = "Default";
                }
            }
        }
    }
}
