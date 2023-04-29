using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneData sceneData;
    [SerializeField] private Animator crossFadeAnimator;
    [SerializeField] private int nextSceneIndex;

    private float animationDuration;
    private bool canLoad;

    private void Awake()
    {
        /* Gets the duration of FadeIn duration. Both FadeIn and FadeOut should be the same. */
        animationDuration = crossFadeAnimator.runtimeAnimatorController.animationClips[0].length;
    }

    /*
     * Subscribes to the OnEnemiesDefeated event in the EnemyHandler script.
     * Subscribes to the OnInteraction event in the PlayerMovement script.
     * Invokes: LoadState()
     */
    private void OnEnable()
    {
        EnemyHandler.OnEnemiesDefeated += LoadState;
        PlayerInteract.OnInteraction += LoadNextScene;
    }

    /* 
     * Unsubscribes from the OnEnemiesDefeated event in the EnemyHandler script (if destroyed).
     * Unsubscribes from the OnInteraction event in the PlayerMovement script (if destroyed).
     */
    private void OnDisable()
    {
        EnemyHandler.OnEnemiesDefeated -= LoadState;
        PlayerInteract.OnInteraction -= LoadNextScene;
    }

    private void LoadState() 
    { 
        canLoad = true; 
    }

    private void LoadNextScene() 
    { 
        if (canLoad)
        {
            StartCoroutine(LoadScene());
        }    
    }
    
    /* 
     * Triggers the FadeOut animation. After the animation is done, the scene changes to the 
     * LoadScreen scene and transitions to the desired scene. 
     */
    private IEnumerator LoadScene()
    {
        crossFadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(animationDuration);
        sceneData.SceneToLoad = nextSceneIndex;
        SceneManager.LoadScene(0);
    }

    /* Getter/Setter. */
    public bool CanLoad
    {
        get { return canLoad; }
        set { canLoad = value; }
    }
}
