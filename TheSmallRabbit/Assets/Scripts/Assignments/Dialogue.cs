using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    private List<string> lines;
    private int currentLineIndex;
    private bool hasLoopingDialogue = false;
    private int loopStartIndex = 0;

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
        else if (hasLoopingDialogue)
        {
            currentLineIndex = loopStartIndex;
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

    public void ResetDialogue()
    {
        currentLineIndex = 0;
    }
}
