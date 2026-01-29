using System;
using UnityEngine;

[Serializable]
public class DialogueSettings
{
    [TextArea] public string Dialogue;
    [TextArea] public string CorrectReaction;
    [TextArea] public string IncorrectReaction;
}
