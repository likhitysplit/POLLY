using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{

    [SerializeField] Dialogue dialogue;
    public void Interact()
    {
        Debug.Log("interacting with npc");
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
}
