using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Next : MonoBehaviour
{
    public void NextDialogue()
    {
        ADialogueManager.instance.DequeueDialogue();
    }
}
