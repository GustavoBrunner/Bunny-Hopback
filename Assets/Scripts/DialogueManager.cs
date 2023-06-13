using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Linq;
public class DialogueManager : MonoBehaviour
{
    //private Queue<Dialogue[]> dialogues;

    public List<Dialogue[]> dialogues;

    private static DialogueManager _instance;
    public static DialogueManager instance 
    {
        get => _instance;
        private set { } 
    }
    private RectTransform tf;

    private string _name;

    private TMP_Text name;

    private TMP_Text text;

    private int index;

    public float TextSpeed;
    private Dialogue[] nextsentences;


    public bool IsDialogueHappn;
    [SerializeField]
    private UnityEngine.UI.Button btn;

    public bool isCutscene;

    void Awake()
    {
        //dialogues = new Queue<string>();
        
        if (_instance != null)
        {
            DestroyImmediate(instance);
        }
        else
        {
            _instance = this;
        }
        name = GameObject.Find("name").GetComponentInChildren<TMP_Text>();
        text = GameObject.Find("Dialogue").GetComponent<TMP_Text>();
        tf = GetComponent<RectTransform>();
        _name = " ";
        TextSpeed = 0.05f;
        TurnDialogueOff();
        index = 0;
        btn = GameObject.Find("NextBtn").GetComponent<UnityEngine.UI.Button>();
        IsDialogueHappn = false;
        isCutscene = false;
    }

    public void UpdateDialogue(Dialogue[] d)
    {
        Debug.Log(IsDialogueHappn);
        if(!IsDialogueHappn)
        {
            IsDialogueHappn = true;
            StartCoroutine(TypeSentence(d));
        }
    }

    private IEnumerator TypeSentence(Dialogue[] _d)
    {
        var actualSentence = _d[index];
        nextsentences = _d;
        this.name.text = actualSentence.name;
        foreach (var letter in actualSentence.sentence)
        {
            text.text += letter;
            yield return new WaitForSeconds(TextSpeed);

        }
        if(isCutscene)
        {
            var check = CheckCutsceneEnd(_d);
            if (check)
            {
                Debug.Log(GameController._instance._phase);
                if (GameController._instance._phase == "FirstQuestPhaseLoop1" || GameController._instance._phase == "SecondQuestEndPhaseLoop1")
                {
                    GameController._instance.StartWhiteEffect();
                }
                else if (GameController._instance._phase == "FirstQuestPhaseLoop2" || GameController._instance._phase == "SecondQuestEndPhaseLoop2")
                {
                    GameController._instance.StartWhiteEffect();
                }
                else if(GameController._instance._phase == GamePhaseChecker.FinalPhase)
                {
                    //iniciará a transição para o final do game
                    GameController._instance.GoToEndGame();
                }
            }
        }
    }
    public void TurnDialogueOn()
    {
        this.tf.localScale = new Vector3(1, 1, 1);
    }
    public void TurnDialogueOff()
    {
        this.tf.localScale = new Vector3(0, 0, 0);
        StopAllCoroutines();
        index = 0;
        text.text = "";
        IsDialogueHappn = false;
    }
    public void NextBtn()
    {
        if (index < nextsentences.Length - 1)
        {
            StopAllCoroutines();
            text.text = "";
            index++;
            StartCoroutine(TypeSentence(nextsentences));
        }
        else
        {
            TurnDialogueOff();
        }

    }

    public bool CheckCutsceneEnd(Dialogue[] d)
    {
        return this.index == d.Length-1;

    }
    public void CallDialogue(Dialogue[] d)
    {
        TurnDialogueOn();
        UpdateDialogue(d);
    }
}
