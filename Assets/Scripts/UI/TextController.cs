using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The class is attached to a Pooled Object and used inconjuction with an Object Pooler. This script
 * is attached to the objects stored in the Object Pooler class. In this case, this script is attached
 * to the Text object which is stored in a list in the TextPooler class. Once a class has retrived
 * a bullet object and enables it, a animation will be play and Disable() will be invoked for 2 seconds.
 * Once it's disabled, the text will disappear, simulating a destroyed object. Since the text objects 
 * have been pre-instantiated, classes can resuse the same object and re-enable them as the animation
 * will restart once they set them active. This creates a seemingly infinite amount of text objects 
 * without taxing the user's system.
 */
public class TextController : MonoBehaviour
{
    [SerializeField] private Animator textAnimator;

    private void OnEnable()
    {
        /* Resets velocity on rigid body after re-enabling. */
        textAnimator.Play("FloatingText");

        /* Invokes Disable(). Disables object after 2 seconds. */
        Invoke("Disable", 2f);
    }

    private void Start()
    {
        textAnimator.Play("FloatingText");
    }

    private void OnDisable()
    {
        /* Stops Disable() from invoking after disabling object. */
        CancelInvoke();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
