using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class ADialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public GameObject portraitAnimator;
        public string myName;
        [TextArea(3, 8)]
        public string myText;
    }
    [Header("Insert Dialogue Information below")]
    public Info[] dialogueInfo;
}
