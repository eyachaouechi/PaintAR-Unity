using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Line",menuName ="NewLine")]
public class LineDialogueSO : ScriptableObject
{
    public Sprite sprite;
    public string nameSpeackerText;
    [TextArea(10,20)]
    public string dialogueText;
}
