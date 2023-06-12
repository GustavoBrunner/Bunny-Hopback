using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public delegate void GameControllerHandler();
public delegate void PlayerPositionHandler(Vector3 position);
/// <summary>
/// O GamePhase é o que vai decidir qual diálogo a personagem terá ao interagir com alguns objetos
/// Cada objeto terá algunas variáveis do tipo diálogo, e uma GamePhase, dependendo da sua GamePhase
/// Diálogos diferentes serão liberados. O único outro diálogo que terá no jogo sem ser da menina
/// é o coelho no final.
/// </summary>
public enum GamePhase
{
    Start,
    StartFirstPuzzle,
    EndFirstPuzzle,
    StartSecondPuzzle,
    EndSecondPuzzle,
    StartThirdPuzzle,
    EndThirdPuzzle,
    Cutscene,
    Final
}

public enum GameLoop
{
    None,
    First,
    Second
}

public class GameController : MonoBehaviour
{
    public static bool isFirstPuzzleItemEnabled { get; private set;} = false;
    public static bool isSecondPuzzleItemEnabled { get; private set;} = false;
    public static bool isThirdPuzzleEnabled { get; private set;} = false;
    public static bool isThirdPuzzleItemEnabled { get; private set; } = false;
    public static bool isForthPuzzleItemEnabled { get; private set;} = false;
    public static bool isFifthPuzzleItemEnabled { get; private set;} = false;
    [SerializeField]private int[] RightCombination = new int[4];
    

    public static event GameControllerHandler FirstPuzzleOpened;
    public static event GameControllerHandler ThirdPuzzleOpened;
    public static event GameControllerHandler OnRightWord;
    public static event GameControllerHandler EnableThirdPuzzle;

    public static event PlayerPositionHandler UpdatePlayerPosition;
    private Vector3 newTransitionPosition = Vector3.zero;
    private string ActualScene = "SampleScene";
    public int sceneTest = 5;

    private static GameController instance;
    public static GameController _instance 
    { 
        get
        {
            return instance;
        }
    }
    [SerializeField]
    private List<Transform> positions = new List<Transform>();

    public GamePhase phase {  get; private set; }

    public GameLoop Loop { get; private set; }

    [SerializeField]
    private GameObject mainCam;

    private Animator camAnimator;

    public int DialogueToTrigger;

    public string _phase;

    [SerializeField]
    private Dialogue[] ARightCombination;

    [SerializeField]
    private Dialogue[] CombinationError;



    private void Awake()
    {
        if(instance) // impedindo a instância de ser destruída ao carregar outra cena sem multiplicar
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
        }
        positions.AddRange(GameObject.FindGameObjectWithTag("PlaceHolders")
            .GetComponentsInChildren<Transform>());
        
        RabbitScript.FirstPuzzleEnabled += FinishFirstPuzzle;
        NumericEnterButton.CodeSent += CheckCombination;
        
        UiController.OnRightAnswer += CheckWordPuzzle;
        RabbitScript.ThirdPuzzleEnabled += EnableThirdPuzzleItem;
        SceneManager.LoadSceneAsync("Bedroom", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Bathroom", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("ParentsRoom", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Attic", LoadSceneMode.Additive);

        //mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        RightCombination[0] = 1;
        RightCombination[1] = 4;
        RightCombination[2] = 1;
        RightCombination[3] = 0;
        Debug.Log($"A combinação correta é: {RightCombination[0]}, {RightCombination[1]}, {RightCombination[2]}, {RightCombination[3]} ");
    }
    public void Start()
    {
        phase = GamePhase.Start;
        Loop = GameLoop.None;
        GameEvents.onUpdatePhase.Invoke(Loop, phase);
        DialogueToTrigger = 1;
    }

    public void FinishFirstPuzzle()
    {
        //Recebera o evento de interacaoo do coelho, e liberara o primeiro puzzle
        isFirstPuzzleItemEnabled = true;
        if(FirstPuzzleOpened != null)
        {
            FirstPuzzleOpened();
        }
    }
    public void EnableThirdPuzzleItem()
    {
        //Receber� o evento de intera��o do coelho, e liberar� o segundo item do puzzle
        isThirdPuzzleEnabled = true;
        ScenesInfo.EnableThirdPuzzle = isThirdPuzzleEnabled;
    }
    public void CheckCombination(int[] _code)
    {
        Debug.Log($"a combinação foi {_code[0]}, {_code[1]}, {_code[2]}, {_code[3]}");
        Debug.Log($"A combinação correta é: {RightCombination[0]}, {RightCombination[1]}, {RightCombination[2]}, {RightCombination[3]} ");
        if (_code[0] == RightCombination[0] && _code[1] == RightCombination[1] && _code[2] == RightCombination[2] && _code[3] == RightCombination[3])
        {
            GameEvents.GetFlashLight.Invoke();
            //DialogueManager.instance.CallDialogue(PlayerScript.instance.RightCombination);
        }
        else
        {
            Debug.Log("Combina��o errada, tente novamente");
            //DialogueManager.instance.CallDialogue(CombinationError);
        }
    }
    public void CheckWordPuzzle(string s)
    {
        if(s.Contains(FreezerScript.puzzleAnswer))
        {
            GameEvents.GetSecondItem.Invoke();
        }
        else
        {
            Debug.Log("Palavra errada");
        }
    }
    public void UpdateGamePhase(GameLoop gl,GamePhase gp)
    {
        this.Loop = gl;
        this.phase = gp;
        _phase = GamePhaseChecker.PhaseChecker(gl, gp);
        Debug.Log($"Fase mudada para {_phase}");
        GameEvents.onUpdatePhase.Invoke(Loop, phase);

    }

    public void RestartDay()
    {
        GameEvents.onRestartDayEvent.Invoke();
        GameEvents.onUpdatePhase.Invoke(Loop, phase);
        Debug.Log(GamePhaseChecker.PhaseChecker(Loop, phase));
        DialogueManager.instance.isCutscene = false;
    }
    public void StartCutscene( int cs)
    {
        Debug.Log(mainCam.gameObject.name);
        if (cs == 1)
        {
            DialogueManager.instance.isCutscene = true;
            Debug.Log(mainCam.gameObject.name);
            camAnimator = mainCam.GetComponent<Animator>();
            camAnimator.SetTrigger("CutsceneStart");

        }
        else if(cs == 2) 
        {
            DialogueManager.instance.isCutscene = true;

            camAnimator.SetTrigger("CutsceneStart");
        }
        else
        {

        }
        
    }

    public void StartFirstCustsceneDialogue(int d)
    {
        GameEvents.onStartDialogue.Invoke(d);
    }
    public void StartWhiteEffect()
    {
        Debug.Log("começando transição");
        camAnimator.SetTrigger("StartLoop");
        RestartDay();
    }

    public void EnableFirstItemPuzzle()
    {
        GameEvents.EnableFirstItem.Invoke();
    }
    public void EnableSecondItemPuzzle()
    {
        GameEvents.GetSecondItem.Invoke();
    }
    public void EnableThirdItemPuzzle()
    {
        GameEvents.GetFlashLight.Invoke();
    }

    public void EndQuestFade()
    {

    }

    public void CheckWichDialogue(string s)
    {
        if(s == "FirstQuestPhaseLoop1")
        {
            DialogueToTrigger = 1;
            StartFirstCustsceneDialogue(DialogueToTrigger);
        }
        if (s == "FirstQuestPhaseLoop2")
        {
            DialogueToTrigger = 2;
            StartFirstCustsceneDialogue(DialogueToTrigger);
        }
    }

    public void QuestTurnOnCam()
    {
        StartCoroutine(QuestCamAction());
    }

    private IEnumerator QuestCamAction()
    {
        DialogueManager.instance.isCutscene = true;
        camAnimator.SetTrigger("QuestTurnOffCam");
        yield return new WaitForSeconds(7f);
        GameEvents.TransitionDialogue.Invoke();
        camAnimator.SetTrigger("QuestTurnOnCam");
        DialogueManager.instance.isCutscene = false;
        StopAllCoroutines();
    }
}
