using UnityEngine;
using System.Collections.Generic;

[System.Serializable] // This makes it editable in the Inspector
public class Dialogue
{
    [TextArea(2, 5)] // Optional: lets you write multi-line dialogue in inspector
    [SerializeField] List<string> lines = new List<string>();

    public List<string> Lines
    {
        get { return lines; }
    }
}
