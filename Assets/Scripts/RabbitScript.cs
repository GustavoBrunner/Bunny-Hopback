using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class RabbitScript : MonoBehaviour, IInteractable
{
    public delegate void OnPlayerInteracted(IInteractable rabbit);
    public delegate void OnQuestInteraction();
    public static event OnPlayerInteracted OnPlayerInteractionStart;
    public static event OnQuestInteraction FirstPuzzleEnabled;
    public static event OnQuestInteraction FirstPuzzleCompleted;
    public static event OnQuestInteraction ThirdPuzzleEnabled;

    private Vector3 newPos;

    private bool playerIn;
    [SerializeField]
    private Collider clldr;

    private static RabbitScript _instance;
    public static RabbitScript instance { get; private set; }

    [SerializeField]
    private Dialogue[] LastPhaseDialogues;
    [SerializeField]
    private Dialogue[] ThirdPhaseDialogues;
    [SerializeField]
    private Dialogue[] SecondPhaseDialogues;
    [SerializeField]
    private Dialogue[] ForthPhaseDialogues;


    [SerializeField]
    private Dialogue[] NonePhaseDialogues;
    private string _phase;
    public const string BUNNY_NAME = "Coelho Mágico";
    public GamePhase Phase { get; set; }
    public Transform pos { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        if(_instance != null)
        {
            DestroyImmediate(_instance);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
        GameEvents.onUpdatePhase.AddListener(ChangeGamePhase);
        
        //DontDestroyOnLoad(clldr);
        clldr = GetComponent<SphereCollider>();
        clldr.isTrigger = true;
        Debug.Log(Phase);
        
        
    }
    protected virtual void Start()
    {
    }
    
    public void Interact()
    {
        //try
        //{
        UiController._instance.HideInteractionFeedback();
        switch (_phase)
            {
                case "FirstPhase":
                    DialogueManager.instance.CallDialogue(this.NonePhaseDialogues);
                    GameController._instance.UpdateGamePhase(GameLoop.None,GamePhase.StartFirstPuzzle);
                    GameController._instance.EnableFirstItemPuzzle();
                    UiController._instance.UpdateTips("\n-> Onde será que deixei o brinquedo?");
                    break;
                case "FirstQuestPhase":

                    break;
                case "FirstQuestEndPhase":
                    GameEvents.onInventoryClear.Invoke();
                    GameController._instance.UpdateGamePhase(GameLoop.First,GamePhase.StartFirstPuzzle);
                    GameController._instance.StartCutscene(1);
                    break;
                case "FirstQuestEndLoop1Phase":

                    GameEvents.onInventoryClear.Invoke();
                    GameController._instance.EndQuestFade();

                    GameController._instance.QuestTurnOnCam();
                    GameController._instance.UpdateGamePhase(GameLoop.First, GamePhase.StartSecondPuzzle);
                    UiController._instance.UpdateTips("\n-> Converse com Bunny");
                break;
                case "FirstQuestLoop1Phase":

                    GameEvents.onInventoryClear.Invoke(); 
                    DialogueManager.instance.CallDialogue(this.NonePhaseDialogues);
                    GameController._instance.UpdateGamePhase(GameLoop.First, GamePhase.Start);
                    break;

                case "BetweenLoop1Phase":
                    DialogueManager.instance.CallDialogue(this.ThirdPhaseDialogues);
                    GameController._instance.UpdateGamePhase(GameLoop.First, GamePhase.StartSecondPuzzle);

                    break;

                case "SecondQuestPhaseLoop1": //libera o freezer para ser interagido
                    DialogueManager.instance.CallDialogue(this.ThirdPhaseDialogues);
                    GameController._instance.EndQuestFade();
                    UiController._instance.UpdateTips("\n-> Acho que a ração do Bunny fica na geladeira");
                    //GameController._instance.UpdateGamePhase(GameLoop.First, GamePhase.EndSecondPuzzle);
                
                    break;
                case "SecondQuestEndPhaseLoop1":
                    if(PlayerScript.instance.SecondPuzzleItemPicked)
                    {
                        GameEvents.onInventoryClear.Invoke();
                        GameController._instance.StartCutscene(2);
                        GameController._instance.UpdateGamePhase(GameLoop.Second,GamePhase.StartFirstPuzzle);
                        
                    }
                    else
                    {
                        DialogueManager.instance.CallDialogue(this.ThirdPhaseDialogues);
                    }

                    break;
                case "FirstQuestLoop2Phase":
                    DialogueManager.instance.CallDialogue(this.LastPhaseDialogues);

                    GameController._instance.UpdateGamePhase(GameLoop.Second, GamePhase.StartFirstPuzzle);
                    break;

                case "FirstQuestEndLoop2Phase": 
                    GameController._instance.UpdateGamePhase(GameLoop.Second, GamePhase.StartSecondPuzzle);
                    GameEvents.onInventoryClear.Invoke();
                    GameController._instance.QuestTurnOnCam();
                    break;

                case "SecondQuestPhaseLoop2":
                    DialogueManager.instance.CallDialogue(LastPhaseDialogues);
                    GameController._instance.UpdateGamePhase(GameLoop.Second, GamePhase.StartSecondPuzzle);

                    break;

                case "SecondQuestEndPhaseLoop2": //Libera o armário para ser interagido
                    if (PlayerScript.instance.SecondPuzzleItemPicked)
                    {
                        DialogueManager.instance.CallDialogue(LastPhaseDialogues);
                        GameController._instance.QuestTurnOnCam();
                        GameEvents.onInventoryClear.Invoke();
                        
                        GameController._instance.UpdateGamePhase(GameLoop.Second, GamePhase.StartThirdPuzzle);
<<<<<<< Updated upstream
                    }
=======
                        UiController._instance.UpdateTips("\n? Tenho que mesmo que ir até o sótão...?");
                    }
                    break;
                case "FinalPhase":
                    GameEvents.onInventoryClear.Invoke();
                    GameController._instance.StartCutscene(3);
>>>>>>> Stashed changes
                    break;

                default:
                    Debug.Log("Nenhuma das fases anteriores");
                    break;
            }
        //}
        //catch(UnityException e)
        //{
        //    var exception = e.Source;
        //    UiController._instance.UpdateDebugText(exception);
        //}
    }
    public void HighLightItem()
    {
        UiController._instance.ShowInteractionFeedback();
    }

    public void TurnInteractionOn()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (OnPlayerInteractionStart != null)
            {
                OnPlayerInteractionStart(this);
            }
        }
    }
    private void ChangeGamePhase(GameLoop gl, GamePhase ph)
    {
        this._phase = GamePhaseChecker.PhaseChecker(gl,ph);

        Debug.Log($"indo para {_phase}");
    }

    public void HideItemInteraction()
    {
        UiController._instance.HideInteractionFeedback();
    }

    public void UpdatePositionFinal()
    {
        //atualiza a posição pro banheiro, pra terminar o jogo. 
        //transform.position = new Vector3(24.8f, -0.67f,3f);
    }
}
