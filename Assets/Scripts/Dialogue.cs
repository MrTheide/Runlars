using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    public string name;
    public Sprite speakerArt;
    public Animation spriteAnimation;

    [TextArea(1,3)]
    public string[] sentences;
}
