using UnityEngine;
public interface IInteractable
{
    public Transform pos { get;  set; }
    GamePhase Phase { get; set; }
    void Interact();    
    void HighLightItem();
    void TurnInteractionOn();
    void HideItemInteraction();
}
