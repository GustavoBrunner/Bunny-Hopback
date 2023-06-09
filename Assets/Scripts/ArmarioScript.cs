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

    [SerializeField]
    private Dialogue[] NotCompletedPuzzle;

    [SerializeField]
    private Dialogue[] NotCompletedPuzzle2;
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
            UiController._instance.UpdateTips("\n-> Hm... Onde ser� que eu posso achar essa senha?");
        }

        switch (_phase)
        {
            case "ThirdQuestPhase":
                DialogueManager.instance.CallDialogue(this.NotCompletedPuzzle);
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
