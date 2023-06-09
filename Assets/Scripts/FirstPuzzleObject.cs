using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstPuzzleObject : ObjectsScript, IInteractable
{
    public delegate void FirstPuzzleObjectPicked(GameObject go);
    public static event FirstPuzzleObjectPicked OnFirtPuzzleObjectPicked;
    private string[] puzzleText;

    public GamePhase Phase { get ; set ; }
    public Transform pos { get => this.transform; set => throw new System.NotImplementedException(); }

    [SerializeField]
    private Dialogue[] NonePhaseDialogue;
    [SerializeField]
    private Dialogue[] FirstPhaseDialogue;
    [SerializeField]
    private Dialogue[] SecondPhaseDialogue;
    [SerializeField]
    private Dialogue[] ThirdPhaseDialogue;
    [SerializeField]
    private string _phase;
    protected override void Awake()
    {
        base.Awake();
        //GameController.FirstPuzzleOpened += TurnInteractionOn;
        GameEvents.onUpdatePhase.AddListener(UpdateGameState);
        //GameEvents.onPuzzleEnabled.AddListener(ChangeInteraction);
        this.canBeInteracted = false;
        Debug.Log(canBeInteracted);
        GameEvents.EnableFirstItem.AddListener(ChangeInteraction);

        GameEvents.onRestartDayEvent.AddListener(RestartDay);
    }
    public void HighLightItem()
    {
        //Código que fará com que esse objeto receba um highlight na tela
        UiController._instance.ShowInteractionFeedback();
    }

    public void Interact()
    {
        UiController._instance.HideInteractionFeedback();
        //Código que fará o item fazer algo ao ser interagido.
        if(!DialogueManager.instance.IsDialogueHappn)
        {

            switch (_phase)
            {
                case "FirstPhase":
                    DialogueManager.instance.CallDialogue(this.NonePhaseDialogues);
                    break;
                
                case "FirstQuestPhase":

                    DialogueManager.instance.CallDialogue(this.FirstPhaseDialogue);
                    UiController._instance.UpdateTips("\n-> Eba! Hora de brincar");
                    break;
                case "FirstQuestPhaseLoop1":

                    DialogueManager.instance.CallDialogue(this.SecondPhaseDialogue);
                    UiController._instance.UpdateTips("\n-> Hora de brincar com o Bunny");
                    break;
                case "FirstQuestPhaseLoop2":

                    DialogueManager.instance.CallDialogue(this.ThirdPhaseDialogue);
                    UiController._instance.UpdateTips("\n-> @¨$@#(!@)@#@!#) Bunny");
                    break; 
                

                default:

                    DialogueManager.instance.CallDialogue(this.NonePhaseDialogues);
                    break;

            }
        }
        if(this.canBeInteracted)
        {
            this.gameObject.SetActive(false);
            GameEvents.onItemPicked.Invoke(this.gameObject);
            if(this.Loop == GameLoop.None)
            {
                GameController._instance.UpdateGamePhase(GameLoop.None, GamePhase.EndFirstPuzzle);
            }
            else if(this.Loop == GameLoop.First)
            {
                GameController._instance.UpdateGamePhase(GameLoop.First, GamePhase.EndFirstPuzzle);
            }
            else
            {
                GameController._instance.UpdateGamePhase(GameLoop.Second, GamePhase.EndFirstPuzzle);
            }
            Debug.Log(this._phase);
        }
    }

    public void TurnInteractionOn()
    {
        //fará com que a Interação dos objetos seja ativada
        cllidr.enabled = true;
    }
    
    protected override void UpdateGameState(GameLoop gl, GamePhase ph)
    {
        this._phase = GamePhaseChecker.PhaseChecker(gl, ph);
        this.Loop = gl;
        Debug.Log(_phase);
        if(_phase == GamePhaseChecker.FirstQuestPhase 
            || _phase == GamePhaseChecker.FirstQuestPhaseLoop1 || _phase == GamePhaseChecker.FirstQuestPhaseLoop2)
        {
            ChangeInteraction();
        }
        Debug.Log(this._phase);
        
    }

    protected override void UpdateGameLoop(GameLoop loop)
    {
        
        this.Loop = loop;
        
    }
    protected override void ChangeInteraction()
    {
        this.canBeInteracted = true;
    }

    protected void RestartDay()
    {
        gameObject.SetActive(true);
    }

    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }
}
