using UnityEngine;
using System.Collections;

public enum GameState { FreeRoam, Dialogue, Study }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameState state;

    private void Start()
    {
        DialogueManager.Instance.OnShowDialogue += () =>
        {
            state = GameState.Dialogue;
        };
        DialogueManager.Instance.OnCloseDialogue += () =>
        {
            if (state == GameState.Dialogue)
            {
                state = GameState.FreeRoam;
            }
        };
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }

        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }
    }
}
