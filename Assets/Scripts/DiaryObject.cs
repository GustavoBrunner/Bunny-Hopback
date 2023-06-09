using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryObject : MonoBehaviour, IInteractable
{
    public GamePhase Phase { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Transform pos { get => this.transform; set => throw new System.NotImplementedException(); }
    

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
        HideItemInteraction();
    }

    public void TurnInteractionOn()
    {
        throw new System.NotImplementedException();
    }
}
