using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PreloadUI : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        DialogueManager.PreloadDialogueUI();
        //anim.SetTrigger("HideDP");
    }

}
