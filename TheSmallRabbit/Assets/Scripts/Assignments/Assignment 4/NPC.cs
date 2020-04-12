using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    Text dialogueText;
    public string characterName = "Mario";
    Dialogue dialogue;
    void Start()
    {
        dialogue.LoadDialogue(characterName);
    }

    public void Speak()
    {
        dialogueText.text = dialogue.GetCurrentLine();
        dialogue.AdvanceDialogue();
    }
    
}
