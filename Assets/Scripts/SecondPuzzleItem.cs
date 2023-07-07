using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPuzzleItem : ObjectsScript, IInteractable
{
    public delegate void InteractionHandler(GameObject go);
    public static event InteractionHandler OnPlayerInteract;

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
        //GameController.OnRightWord += TurnInteractionOn;
        
        //cllidr.enabled = false;
        
    }
    protected override void Start()
    {
        base.Start();
        //GameController.OnRightWord += TurnInteractionOn;
        GameEvents.onUpdatePhase.AddListener(UpdateGameState);

        GameEvents.GetSecondItem.AddListener(GetSecondItem);
    }
    public void HighLightItem()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        if (!DialogueManager.instance.IsDialogueHappn)
        {

            if (this.canBeInteracted)
            {
                this.gameObject.SetActive(false);
                
            }
        }
    }

    public void TurnInteractionOn()
    {
        cllidr.enabled = true;
    }

    protected override void UpdateGameState(GameLoop gl, GamePhase gp)
    {
        this._phase = GamePhaseChecker.PhaseChecker(gl,gp);
        this.Loop = gl;
    }
    protected override void ChangeInteraction()
    {
        Debug.Log("Ativando segundo item");
        this.canBeInteracted = true;
        cllidr.enabled = true;
    }

    public void GetSecondItem()
    {
        GameEvents.onItemPicked.Invoke(this.gameObject);
        PlayerScript.instance.SecondPuzzleItemPicked = true;
        Debug.Log("Pegando segundo item");
        Debug.Log(PlayerScript.instance.SecondPuzzleItemPicked);
        if (this.Loop == GameLoop.First)
        {
            GameController._instance.UpdateGamePhase(GameLoop.First, GamePhase.EndSecondPuzzle);
            Debug.Log("Mudando o loop para: " + this.Loop);
            UiController._instance.UpdateTips("\n? Bunny está com muita fome. Devo me apressar!");
        }
        else
        {
            GameController._instance.UpdateGamePhase(GameLoop.Second, GamePhase.EndSecondPuzzle);
            UiController._instance.UpdateTips("\n? Bunny @#$%¨$##@# devo... me @#$@#% ?");
        }
    }

    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }
    private void OnDisable()
    {
        Debug.Break();
    }
}
