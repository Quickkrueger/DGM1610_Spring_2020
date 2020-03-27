using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    private List<string> lines;
    private int currentLineIndex;

    Dialogue()
    {
        currentLineIndex = 0;
        lines = new List<string>();
    }

    public string GetCurrentLine()
    {
        if (currentLineIndex < lines.Count)
        {
            return lines[currentLineIndex];
        }
        return "";
    }

    public void AdvanceDialogue()
    {
        currentLineIndex++;
    }

    public void LoadDialogue(string characterName)
    {

    }
}
