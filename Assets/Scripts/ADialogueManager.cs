using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADialogueManager : MonoBehaviour
{
    public static ADialogueManager instance;
    public Animator animator;
    public GameObject button;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }
    public Text dialogueName;
    public Text dialogueText;
    public DialoguePortrait dialoguePortrait;
    [Range(0,0.005f)]
    public float delay = 0.001f;

    public Queue<ADialogueBase.Info> dialogueInfoQueue = new Queue<ADialogueBase.Info>();

    private bool isCurrentlyTyping;
    private string completeText;
    public bool isInDialogue;
    public void EnqueueDialogue(ADialogueBase db)
    {
        dialogueInfoQueue.Clear();
        ShowDialoguePanel();
        foreach (ADialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfoQueue.Enqueue(info);
        }
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (isCurrentlyTyping == true)
        {
            ShowFullText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }
        
        if (dialogueInfoQueue.Count == 0)
        {
            HideDialogPanel();
            return;
        }
        ClearAnimationPortrait();

        //Fetch and display data
        ADialogueBase.Info info = dialogueInfoQueue.Dequeue();
        completeText = info.myText;
        dialogueName.text = info.myName;
        dialogueText.text = info.myText;
        DisplayAnimatedPortrait(info);
       
        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(ADialogueBase.Info info)
    {
        dialogueText.text = "";
        isCurrentlyTyping = true;
        foreach(char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
        }
        isCurrentlyTyping = false;
    }

    private void ShowFullText()
    {
        dialogueText.text = completeText;
    }

    private void ShowDialoguePanel()
    {
        animator.SetBool("isOpen", true);
        //dialogueText.text = "";
        //button.SetActive(true);
        isInDialogue = true;
    }

    public void HideDialogPanel()
    {
        animator.SetBool("isOpen", false);
        button.SetActive(false);
        isInDialogue = false;
    }

    private void ClearAnimationPortrait()
    {
        if (null != dialoguePortrait.Animator)
        {
            Object.Destroy(dialoguePortrait.Animator.gameObject);
        }
    }

    private void DisplayAnimatedPortrait(ADialogueBase.Info info)
    {
        var portraitAnimatorInstance = Object.Instantiate(info.portraitAnimator.gameObject);
        portraitAnimatorInstance.transform.SetParent(dialoguePortrait.transform, false);
        var dialoguePortraitRectTrans = dialoguePortrait.GetComponent<RectTransform>();
        var portraitAnimatorInstanceRectTrans = portraitAnimatorInstance.GetComponent<RectTransform>();
        portraitAnimatorInstanceRectTrans.sizeDelta = dialoguePortraitRectTrans.sizeDelta;
        dialoguePortrait.Animator = portraitAnimatorInstance.GetComponent<Animator>();
    }
}
