using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string name;
    public Sprite icon;
    [TextArea(3,10)]
    public string sentences;
}
