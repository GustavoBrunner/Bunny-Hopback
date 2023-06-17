using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueItems : MonoBehaviour, IInteractable


{
    public GamePhase Phase { get ; set ; }
    public Transform pos { get => this.transform; set => throw new System.NotImplementedException(); }

    [SerializeField]
    private Dialogue[] ListDialogue;
    
    [SerializeField]
    private Dialogue[] PicDialogue;

    [SerializeField]
    private Dialogue[] DrawDialogue;
    private void Awake()
    {
        
    }
    public void HighLightItem()
    {
        UiController._instance.ShowInteractionFeedback();
        
    }

    public void Interact()
    {
        UiController._instance.HideInteractionFeedback();
        switch(this.gameObject.name)
        {
            case "PicFrame":
                UiController._instance.ShowPicFrame();
                DialogueManager.instance.CallDialogue(PicDialogue);
                break;
            case "Draw":
                UiController._instance.ShowDraw();
                DialogueManager.instance.CallDialogue(DrawDialogue);
                break;
            case "ListClue":
                UiController._instance.ShowList();

                DialogueManager.instance.CallDialogue(ListDialogue);
                break;
            default:
                Debug.Log("Objeto desconhecido");
                break;
        }
    }

    public void TurnInteractionOn()
    {
        throw new System.NotImplementedException();
    }
    private void TurnClueOff()
    {
        transform.localScale = Vector3.zero;
    }
    private void TurnClueOn()
    {
        this.gameObject.SetActive(true);
    }

    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }
}
