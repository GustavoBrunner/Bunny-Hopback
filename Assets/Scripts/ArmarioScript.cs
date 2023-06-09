using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ArmarioScript : ObjectsScript, IInteractable
{
    public delegate void FreezerInteractionHandler();
    public static event FreezerInteractionHandler OnCabinetInteractionStarted;
    public GamePhase Phase { get; set; }
    public Transform pos { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField]
    private Dialogue[] dialogues;

    private string _phase;
    protected override void Awake()
    {
        base.Awake();
        //RabbitScript.ThirdPuzzleEnabled += TurnInteraction;
        GameEvents.onUpdatePhase.AddListener(UpdateGameState);
    }
    
    
    void TurnInteractionOff()
    {
        cllidr.enabled = false;
    }

    public void Interact()
    {
        UiController._instance.HideInteractionFeedback();
        if (this.canBeInteracted)
        {
            GameEvents.onCabinet.Invoke();
        }

        switch (_phase)
        {
            case "ThirdQuestPhase":



                break;
            default:

                DialogueManager.instance.CallDialogue(this.NonePhaseDialogues);
                break;
        }
    }

    public void HighLightItem()
    {
        UiController._instance.ShowInteractionFeedback();
    }

    public void TurnInteractionOn()
    {
        throw new System.NotImplementedException();
    }
    protected override void ChangeInteraction()
    {
        this.canBeInteracted = true;
    }
    protected override void UpdateGameState(GameLoop gl, GamePhase ph)
    {
        this._phase = GamePhaseChecker.PhaseChecker(gl,ph);
        if(this._phase == GamePhaseChecker.ThirdQuestPhase)
        {
            ChangeInteraction();
        }
    }

    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }
}
