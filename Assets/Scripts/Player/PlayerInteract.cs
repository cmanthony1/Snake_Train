using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Prompt Object")]
    [SerializeField] private GameObject promptObj;

    public static Action OnInteraction;

    private Animator promptAnimator;
    private bool canInteract;

    private void Awake()
    {
        promptAnimator = promptObj.GetComponent<Animator>();
    }

    public void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnInteraction?.Invoke();
            }
        }
    }

    public void Prompt(bool value)
    {
        promptAnimator.SetBool("Prompt", value);
        canInteract = promptAnimator.GetBool("Prompt");
    }

    public bool PromptState
    {
        get { return promptObj.activeInHierarchy; }
        set { promptObj.SetActive(value); }
    }
}
