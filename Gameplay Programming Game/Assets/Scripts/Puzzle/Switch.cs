using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Transmitter
{
    float floPlayerDistance;
    public float floInteractDistance;
    public bool bolToggle = false;
    SpriteRenderer sprRenderer;
    public Sprite sprEnabledSprite;
    public Sprite sprDisabledSprite;

    void Start()
    {
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        FindPlayer();
    }

    void Update()
    {
        floPlayerDistance = Vector2.Distance(gameObject.transform.position, objPlayer.transform.position);
        if (floPlayerDistance <= floInteractDistance && Input.GetButtonDown("Submit"))
        {
            Activate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook"))
        {
            Activate();
        }
    }

    void Activate()
    {
        // Checks whether the switch can be toggled or not
        if (!bolToggle && !bolActivated)
        {
            bolActivated = true;
            sprRenderer.sprite = sprEnabledSprite;
        }
        else if (bolToggle)
        {
            bolActivated = !bolActivated;
            if (sprDisabledSprite != null && sprEnabledSprite != null)
            {
                if (bolActivated && sprRenderer.sprite != sprEnabledSprite)
                {
                    sprRenderer.sprite = sprEnabledSprite;
                }
                else if (sprRenderer.sprite != sprDisabledSprite)
                {
                    sprRenderer.sprite = sprDisabledSprite;
                }
            }
        }
    }
}
