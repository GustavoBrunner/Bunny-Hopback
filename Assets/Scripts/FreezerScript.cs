using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerScript : ObjectsScript , IInteractable
{
    public const string wordAnswer = "Trakinas";
    public delegate void InteractionHandler();
    public static event InteractionHandler OnPlayerInteract;

    private string _phase;
    public GamePhase Phase { get; set; }
    public Transform pos { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    protected override void Awake()
    {
        base.Awake();
        Debug.Log(wordAnswer);
        RabbitScript.FirstPuzzleCompleted += TurnInteractionOn;
        GameEvents.onUpdatePhase.AddListener(UpdateGameState);
        GameEvents.onRestartDayEvent.AddListener(RestartGame);
    }
    public void HighLightItem()
    {
        UiController._instance.ShowInteractionFeedback();
    }

    public void Interact()
    {
        UiController._instance.HideInteractionFeedback();
        switch (_phase)
        {
            
            case "SecondQuestPhaseLoop1":

                //DialogueManager.instance.CallDialogue(this.SecondPhaseDialogue);
                UiController._instance.UpdateTips("\n-> Deve ter a palavra secreta em algum lugar");
                break;
            case "SecondQuestPhaseLoop2":

                //DialogueManager.instance.CallDialogue(this.FirstPhaseDialogue);
                UiController._instance.UpdateTips("\n-> ONDE ESTÁ A PALAVRA????");
                break;


            default:

                //DialogueManager.instance.CallDialogue(this.NonePhaseDialogue);
                break;

        }
        if (this.canBeInteracted)
        {
            GameEvents.onFreezer.Invoke();
            Debug.Log("Interagindo com a geladeira");
        }
    }

    

    public void TurnInteractionOn()
    {
        cllidr.enabled = true;
    }
    protected override void UpdateGameState(GameLoop gl, GamePhase ph)
    {
        this._phase = GamePhaseChecker.PhaseChecker(gl,ph);
        Debug.Log(Phase);
        if (this._phase == GamePhaseChecker.SecondQuestPhase 
            || this._phase == GamePhaseChecker.SecondQuestPhaseLoop1 
            || this._phase == GamePhaseChecker.SecondQuestPhaseLoop2 )
        {
            ChangeInteraction();
            Debug.Log($"indo para {Phase}");
        }
    }
    protected override void ChangeInteraction()
    {
        this.canBeInteracted = true;
        Debug.Log(canBeInteracted);
    }
    private void RestartGame()
    {
        canBeInteracted = false;
    }

    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }
}
