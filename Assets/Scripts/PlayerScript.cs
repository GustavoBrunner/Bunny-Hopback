using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField]
    private float Vertical;
    [SerializeField]
    private float Horizontal;
    private Vector3 direction;
    private float speed = 3.5f;
    private bool CanMove;
    private float RoomTransitionTime = 1f;
    

    public const string PLAYER_NAME = "Menina";
    //------------------------------------------------
    //puzzles
    public static int GamePhase { get; private set; }
    
    public static float _horizontal { get; private set;}
    public static float _vertical { get; private set;}
    public static bool playerInTrigger;
    public bool _playerInTrigger;
    private string ActualRoom;
    private static PlayerScript _instance;
    public static PlayerScript instance
    {
        get => _instance;
    }
    private CapsuleCollider clldr;

    [SerializeField]
    private Dialogue[] FirstPhaseDialogues;

    [SerializeField]
    private Dialogue[] CutsceneDialogues1;

    [SerializeField]
    private Dialogue[] CutsceneDialogues2;

    [SerializeField]
    private Dialogue[] CutsceneDialogues3;

    [SerializeField]
    private Dialogue[] SecondPhaseDialogues;

    [SerializeField]
    private Dialogue[] ThirdPhaseDialogues;

    [SerializeField]
    private Dialogue[] LastPhaseDialogues;

    [SerializeField]
    private Dialogue[] AtticTriggerDialogues;

    [SerializeField]
    private Dialogue[] ARightCombination;

    [SerializeField]
    private Dialogue[] CombinationError;
    [SerializeField]
    private Dialogue[] DialoguePlaceHolder;

    private Vector3 initialPosition;

    private Vector3 atticPosition, 
        bathroom, pBedroom, corridor1, corridor2, corridor3, corridor4, bedroom;

    private GameObject flashLight;

    public bool hasFlashLight { get; private set; }

    private bool isFlashLightOn;

    private Vector3 flip;
    private Vector3 unFlip;

    [SerializeField]
    private IInteractable interactable;

    [SerializeField]
    private bool PlayerInteracting;

    public bool SecondPuzzleItemPicked { get; set; } = false;
    public bool ThirdPuzzleItemPicked { get; set; } = false;


    private bool playerMoving;
    private Animator animator;
    void Awake()
    {
        ActualRoom = "CameraContainerBedroom";
        if(_instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
        }
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        gameObject.tag = "Player";
        CanMove = true;
        //RabbitScript.OnPlayerInteractionStart += RabbitInteraction;
        //FirstPuzzleObject.OnFirtPuzzleObjectPicked += GetItem;
        //SecondPuzzleItem.OnPlayerInteract += GetItem;
        GamePhase = 0;
        playerInTrigger = false;
        initialPosition = new Vector3(0f,0f,9.5f);
        atticPosition = new Vector3(57.2f,4.88f,19.26f);
        bedroom = new Vector3(5.7f, -0.14f, 4.1f);
        bathroom = new Vector3(21.6f, -0.26f, 4.1f);
        pBedroom = new Vector3(33.7f, -0.2f, 4.1f);
        corridor1 = new Vector3(5.7f, -0.2f, 0.23f);
        corridor2 = new Vector3(21.6f, -0.24f, 0.23f);
        corridor3 = new Vector3(33.8f, -0.24f, 0.23f);
        corridor4 = new Vector3(57.22f, -0.24f, 7.6f);

        transform.position = initialPosition;

        flashLight = GameObject.Find("FlashLight");
        flashLight.SetActive(false);

        hasFlashLight = false;

        isFlashLightOn = false;

        flip = new Vector3(-1, 1, 1);
        unFlip = new Vector3(1, 1, 1);

        playerMoving = false;
        animator = gameObject.GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        clldr = GetComponent<CapsuleCollider>();
        SceneTriggers.StopPlayer += WalkCooldownCheck;
        SceneTriggers.BedroomEntered += Move;
        SceneTriggers.BathroomEntered += Move;

        GameEvents.onRestartDayEvent.AddListener(RestartDay);
        GameEvents.onStartDialogue.AddListener(CutsceneDialogue);

        GameEvents.TransitionDialogue.AddListener(TransitionDialogues);

        GameEvents.GetFlashLight.AddListener(GetFlashLight);
        
    }
    void Update()
    {
        Move("", ActualRoom);
        _playerInTrigger = playerInTrigger;

        


        if (UnityEngine.Input.GetKeyDown(KeyCode.F))
        {
            if(!isFlashLightOn)
            {
                TurnFlashLightOn();
                isFlashLightOn = true;
            }
            else
            {
                TurnFlashLightOff();
                isFlashLightOn= false;
            }
        }

        Interaction();
        CheckMotion();
        SetAnimation();
    }
    private void Move(string _lastRoom, string _actualRoom)
    {

        
        //Respons�vel por fazer o jogador se mover pelo mapa
        AudioController.instance.UpdatePosition(this.transform);
        if(CanMove)
        {
            ActualRoom = _actualRoom;
            _horizontal = Horizontal;
            _vertical = Vertical;
            if(ActualRoom == "CameraContainerCorridor")
            {
                Vertical = UnityEngine.Input.GetAxis("Vertical");
                Horizontal = UnityEngine.Input.GetAxis("Horizontal");
                direction = new Vector3(Horizontal, 0f, Vertical);
                rb.velocity = direction * speed;

                if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                {
                    FlipChar();
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                {
                    UnflipChar();
                }
            }
            else
            {
                Vertical = UnityEngine.Input.GetAxis("Vertical");
                Horizontal = UnityEngine.Input.GetAxis("Horizontal");
                direction = new Vector3(-Horizontal, 0f, -Vertical);
                rb.velocity = direction * speed;

                if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                {
                    SecondaryFlipChar();
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                {
                    SecondaryUnflipChar();
                }
            }
        }
    }
    private void CheckMotion()
    {
        if (rb.velocity.magnitude > 0)
        {
            playerMoving = true;
            AudioController.instance.PlayStep();
        }
        else
        {
            playerMoving = false;
            AudioController.instance.StopStep();
        }
    }
    private void SetAnimation()
    {
        if(playerMoving)
        { 
            animator.SetTrigger("walking");
            
        }
        else
        {
            animator.SetTrigger("idle");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerInteracting = true;
        interactable = other.gameObject.GetComponent<IInteractable>();
        Debug.Log($"Interagindo com {other.gameObject.name}");
        if(interactable != null)
        interactable.HighLightItem();
    }
    private void OnTriggerExit(Collider other)
    {
        interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.HideItemInteraction();
            PlayerInteracting = false;
            interactable = null;
        }
    }

    private void Interaction()
    {
        if (interactable != null)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
        
    }
    
    private void RestartPosition()
    {
        
    }
    private void WalkCooldownCheck()
    {
        CanMove = false;
        StartCoroutine(WalkCooldown());
    }
    private IEnumerator WalkCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        CanMove = true;
    }
    private void TurnFlashLightOn()
    {
        if (hasFlashLight)
        {
            flashLight.SetActive(true);
        }
        
    }
    private void TurnFlashLightOff()
    {
        if (hasFlashLight)
        {
            flashLight.SetActive(false);
        }
    }
    private void GetFlashLight()
    {
        this.hasFlashLight = true;
        UiController._instance.UpdateTips("\n-> Vá até o sótão!");

        //trocará também as sprites da personagem principal.
    }

    public void FlipChar()
    {
        transform.localScale = flip;
    }
    public void UnflipChar()
    {
        transform.localScale = unFlip;
    }
    public void SecondaryFlipChar()
    {
        transform.localScale = unFlip;
    }
    public void SecondaryUnflipChar()
    {
        transform.localScale = flip;
    }

    private void RestartDay()
    {
        transform.position = initialPosition;
        ActualRoom = "CameraContainerBedroom";
    }

    public void CutsceneDialogue(int d)
    {
        switch (d)
        {
            case 1:
                DialogueManager.instance.CallDialogue(this.CutsceneDialogues1);
                break;
            case 2:
                DialogueManager.instance.CallDialogue(this.CutsceneDialogues2);
                Debug.Log("Segundo Diálogo");
                break;
            case 3:
                DialogueManager.instance.CallDialogue(this.CutsceneDialogues3);
                Debug.Log("Final Diálogo");
                break;
            default:
                Debug.Log("Teste Cutscene");
            break;
        }
    }

    public void TransitionDialogues()
    {
        DialogueManager.instance.CallDialogue(this.DialoguePlaceHolder);
    }
    public void AtticDialogueTrigger()
    {
        DialogueManager.instance.CallDialogue(this.AtticTriggerDialogues);
    }
    public void UpdatePosition(string room)
    {
        switch (room)
        {
            case "Attic":
                transform.position = atticPosition;
                break;
            case "Corridor1":
                transform.position = corridor1;
                break;
            case "Bedroom":
                transform.position = bedroom;
                break;
            case "PBedroom":
                transform.position = pBedroom;
                break;
            case "Bathroom":
                transform.position = bathroom;
                break;
            case "Corridor2":
                transform.position = corridor2;
                break;
            case "Corridor3":
                transform.position = corridor3;
                break;
            case "Corridor4":
                transform.position = corridor4;
                break;
            default:
                Debug.LogWarning("Update de posição não funcionando");
                break;
        }
    }
    private IEnumerator WalkSound()
    {
        AudioController.instance.PlayStep();
        yield return new WaitForSeconds(4f);
    }

    public void Combination()
    {
        DialogueManager.instance.CallDialogue(this.ARightCombination);
    }
    public void ChangeMoviment(bool m)
    {
        this.CanMove = m;
    }
}
