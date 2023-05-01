using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private TransitionController transitionController;

    /*
     * Subscribes to the CurrentStateState event in the CombatStateManager script.
     * Invokes: Open()
     */
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInteract player = collision.gameObject.GetComponent<PlayerInteract>();
            player.Controller = transitionController;
        }
    }
}
