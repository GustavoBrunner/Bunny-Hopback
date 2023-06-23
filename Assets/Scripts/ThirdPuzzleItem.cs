using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPuzzleItem : ObjectsScript, IInteractable
{
    public delegate void ThirdPuzzleHandler(GameObject go);
    public static event ThirdPuzzleHandler OnPlayerInteract;

    [SerializeField]
    private Dialogue[] NonePhaseDialogue;
    [SerializeField]
    private Dialogue[] FirstPhaseDialogue;
    [SerializeField]
    private Dialogue[] SecondPhaseDialogue;
    [SerializeField]
    private Dialogue[] ThirdPhaseDialogue;
    public GamePhase Phase { get; set; }
    public Transform pos { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private string _phase;
    protected override void Awake()
    {
        base.Awake();
        GameEvents.onUpdatePhase.AddListener(UpdateGameState);
        GameEvents.EnableThirdItem.AddListener(TurnInteractionOn);
        this.canBeInteracted = false;
    }
    public void HighLightItem()
    {
        UiController._instance.ShowInteractionFeedback();
        
    }
    private void Update()
    {
        TurnInteractionOn();
    }

    public void Interact()
    {
        if (this.canBeInteracted)
        UiController._instance.HideInteractionFeedback();
        if (!DialogueManager.instance.IsDialogueHappn)
        {
            switch (_phase)
            {
                case "ThirdQuestPhase":
                    DialogueManager.instance.CallDialogue(this.ThirdPhaseDialogue);
                    Debug.Log("Primeira fase");
                    GameController._instance.UpdateGamePhase(GameLoop.Second, GamePhase.Final);
                    GetThirdItem();
                    break;
                

                default:
                    DialogueManager.instance.CallDialogue(this.NonePhaseDialogue);
                    break;

            }
        }

    }


    public void TurnInteractionOn()
    {
        this.canBeInteracted = true;
    }
    protected override void UpdateGameState(GameLoop gl, GamePhase gp)
    {
        this._phase = GamePhaseChecker.PhaseChecker(gl,gp);
        
    }

    private void GetThirdItem()
    {
        GameEvents.onItemPicked.Invoke(this.gameObject);
        PlayerScript.instance.ThirdPuzzleItemPicked = true;
        gameObject.SetActive(false);
        UiController._instance.UpdateTips("\n-> Ache Bunny");
    }

    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }
}
