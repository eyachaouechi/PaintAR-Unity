using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public List<LineDialogueSO> dialogue = new List<LineDialogueSO>();
    public Text textArea;
    public Text speackerName;
    public Image imageSpeacker;
    public float speedAnim = 0.3f;
    public int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        ClearText();
    }
    private void OnEnable()
    {
        OnClickDialogue();
    }


    IEnumerator AnimRoutine(string textString)
    {
        foreach (char c in textString.ToCharArray())
        {
            textArea.text += c.ToString();
            yield return new WaitForSeconds(speedAnim);
        }
    }

    public void OnClickDialogue()
    {
       StopAllCoroutines();
        if (counter > dialogue.Count - 1)
        {
            return;
        }
        ClearText();
        imageSpeacker.sprite = dialogue[counter].sprite;
        speackerName.text = dialogue[counter].nameSpeackerText;
        StartCoroutine(AnimRoutine(dialogue[counter].dialogueText));

        counter++;
    }

    private void ClearText()
    {
        textArea.text = string.Empty;
    }

}
