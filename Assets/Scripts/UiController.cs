using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    public delegate void WordPuzzleHandler(string answer);
    public static event WordPuzzleHandler OnRightAnswer;
    private Vector3 UiScaleZero = new Vector3(0f,0f,0f);
    private Vector3 UiScaleOne = new Vector3(1f,1f,1f);

    private RectTransform FirstPuzzleInput;
    private TMP_InputField PuzzleInput;
    private string PuzzleAnswer = "Trakinas";
    private RectTransform WordPuzzleTf;
    private static UiController instance;

    public static UiController _instance 
    {
        get => instance;
    }

    private GameObject listClue;

    private GameObject drawClue;

    private GameObject picFramClue;

    private Transform interactionFeedback;

    private TMP_Text tipsText;
    private Transform tipsUi;

    private string initialText;
    private bool isTipsOpened;


    private Animator animator;

    private Image tipsImage;
    protected virtual void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
        }
        FirstPuzzleInput = transform.Find("FirstPuzzleInput").GetComponent<RectTransform>();
        PuzzleInput = transform.Find("WordPuzzle").GetComponentInChildren<TMP_InputField>();
        WordPuzzleTf = transform.Find("WordPuzzle").GetComponent<RectTransform>();
        listClue = GameObject.Find("List");
        picFramClue = GameObject.Find("FramePic");
        drawClue = GameObject.Find("Draw");

        interactionFeedback = GameObject.Find("Interaction").GetComponent<Transform>();

        tipsUi = GameObject.Find("TipsUi").GetComponent<Transform>();

        tipsText = GameObject.Find("TipsUi").GetComponentInChildren<TMP_Text>();
        isTipsOpened = false;
        animator = GetComponent<Animator>();
        tipsImage = GameObject.Find("TipsImage").GetComponent<Image>();
        CloseTerminal();
        CloseWordUi();
        HideDraw();
        HideList();
        HidePicFrame();
        
    }
    
    protected virtual void Start()
    {
        GameEvents.onCabinet.AddListener(OpenTerminal);
        GameEvents.onFreezer.AddListener(OpenWordUi);
        GameEvents.onCloseTerminal.AddListener(CloseTerminal);
        initialText = tipsText.text;
        GameEvents.onRestartDayEvent.AddListener(RestartTips);
    }

    
    //Abre o terminal num�rico
    private void OpenTerminal()
    {
        FirstPuzzleInput.localScale = UiScaleOne;
    }

    //fecha o terminal num�rico
    private void CloseTerminal()
    {
        FirstPuzzleInput.localScale = UiScaleZero;
    }

    //pega a palavra digitada no puzzle, e armazena numa vari�vel.
    //depois essa vari�vel vai ser enviada para o gamecontroller
    public void WordPuzzleBtn()
    {
        var answer = PuzzleInput.text;
        if (answer.ToLower() == PuzzleAnswer.ToLower())
        {
            PuzzleInput.text = "Certa resposta!";
            GameEvents.GetSecondItem.Invoke();
        }
        else
        {
            PuzzleInput.text = "Resposta errada";
        }
        
    }

    //Abrir� o puzzle ao interagir com a geladeira. 
    private void OpenWordUi()
    {
        WordPuzzleTf.localScale = UiScaleOne;
    }
    
    //fechar� o puzzle da palavra secreta ao clicar no bot�o
    public void CloseWordUi()
    {
        WordPuzzleTf.localScale = UiScaleZero;
        PuzzleInput.text = "";
    }

    public void ShowList()
    {
        listClue.transform.localScale = UiScaleOne;
    }
    public void HideList()
    {
        listClue.transform.localScale = UiScaleZero;
    }
    public void ShowPicFrame()
    {
        picFramClue.transform.localScale = UiScaleOne;
    }
    public void HidePicFrame()
    {
        picFramClue.transform.localScale = UiScaleZero;
    }
    public void ShowDraw()
    {
        drawClue.transform.localScale = UiScaleOne;
    }
    public void HideDraw()
    {
        drawClue.transform.localScale = UiScaleZero;
    }
    public void ShowInteractionFeedback()
    {
        interactionFeedback.transform.localScale = Vector3.one;
    }
    public void HideInteractionFeedback()
    {
        interactionFeedback.transform.localScale = Vector3.zero;
    }

    public void UpdateTips(string newTip)
    {
        if (!tipsText.text.Contains(newTip))
        {
            animator.SetTrigger("NewTip");
            var tips = tipsText.text;
            tips += newTip;
            tipsText.text = tips;
        }
        else
        {
            return;
        }
    }

    public void ShowTips()
    {
        animator.SetBool("TipRead", false);
        if (!isTipsOpened && !DialogueManager.instance.IsDialogueHappn)
        {
            animator.SetBool("TipRead", true);
            tipsUi.transform.localScale = Vector3.one;
            isTipsOpened = true;
            
        }
        else
        {
            tipsUi.transform.localScale = Vector3.zero;
            isTipsOpened = false;
            animator.SetBool("TipRead", false);
        }
    }
    public void HideTips()
    {
        tipsUi.transform.localScale = Vector3.zero;
    }
     private void RestartTips()
    {
        if(GameController._instance._phase == GamePhaseChecker.FirstQuestPhaseLoop1)
        {
            tipsText.text = initialText + "\n→ Tenho que achar o... brinquedo? ";
        }
        if(GameController._instance._phase == GamePhaseChecker.FirstQuestPhaseLoop2)
        {
            tipsText.text = initialText + "\n→ ⍖ ⍗ ⍘ ⍙ ⍚ ⍛ ⍜ ⍝ ⍞ ⍟ ⍠ ⍡ ⍢ ⍣ ⍤  Bunny?";
        }
    }
}
