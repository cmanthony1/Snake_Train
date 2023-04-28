using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptTransition : MonoBehaviour
{
    private SpriteRenderer arrowSprend;
    private bool canOpen;


    private void Awake()
    {
        arrowSprend = transform.Find("SpriteArrow").GetComponent<SpriteRenderer>();
    }

    /*
     * Subscribes to the OnInteraction event in the PlayerMovement script.
     * Invokes: Open()
     */
    private void OnEnable()
    {
        EnemyHandler.OnEnemiesDefeated += Open;
    }

    /* Unsubscribes from the OnEnemiesDefeated event in the EnemyHandler script (if destroyed). */
    private void OnDisable()
    {
        EnemyHandler.OnEnemiesDefeated -= Open;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInteract player = collision.gameObject.GetComponent<PlayerInteract>();
            player.PromptState = true;
            player.Prompt(canOpen);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInteract player = collision.gameObject.GetComponent<PlayerInteract>();
            player.PromptState = false;
            player.Prompt(canOpen);
        }
    }

    private void Open()
    {
        canOpen = true;
        arrowSprend.color = ColorUtility.TryParseHtmlString("#00A619", out Color color) ? color : arrowSprend.color;
    }

    //Gizmos
    public void OnDrawGizmos()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        Gizmos.color = new Color(1f, 1f, 1f, 0.25f);
        Gizmos.DrawCube(gameObject.transform.position, new Vector3(collider.size.x, collider.size.y, 0f));
    }
}
