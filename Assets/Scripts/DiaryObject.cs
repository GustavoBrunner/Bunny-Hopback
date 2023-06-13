using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryObject : MonoBehaviour, IInteractable
{
    public GamePhase Phase { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Transform pos { get => this.transform; set => throw new System.NotImplementedException(); }

    [SerializeField]
    private Dialogue[] diaryDialogue;

    [SerializeField]
    private Dialogue[] secondDiaryDialogue;

    private void Awake()
    {
        GameEvents.onEndDiaryInteraction.AddListener(LastDiaryDialogue);
    }
    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }

    public void HighLightItem()
    {
        UiController._instance.ShowInteractionFeedback();
    }

    public void Interact()
    {
        GameEvents.onDiaryInteracted.Invoke();
        gameObject.SetActive(false);
        Debug.Log("interagindo com Diário");
        HideItemInteraction();
        //DialogueManager.instance.CallDialogue(diaryDialogue);
    }

    public void TurnInteractionOn()
    {
        throw new System.NotImplementedException();
    }
    private void LastDiaryDialogue()
    {
        DialogueManager.instance.CallDialogue(secondDiaryDialogue);
    }
}
