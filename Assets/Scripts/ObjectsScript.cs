using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectsScript : MonoBehaviour
{
    protected Transform tf;
    protected string name;
    protected Vector3 IntancePosition;
    protected Quaternion IntanceRotation;
    protected Collider cllidr;
    protected GamePhase GamePhase;
    protected GameLoop Loop;

    [SerializeField]
    protected Sprite interactionFeedback;

    [SerializeField]
    protected Dialogue[] NonePhaseDialogues;

    protected bool canBeInteracted;

    protected virtual void Awake()
    {
        tf = GetComponent<Transform>();
        cllidr = GetComponent<Collider>();
        cllidr.isTrigger = true;
        interactionFeedback = Resources.Load<Sprite>("Sprites/InteractionFeedback");
        //placeHolder = GameObject.Find("InteractionPlaceHolder").GetComponent<Image>();
        //placeHolder.sprite = interactionFeedback;
    }
    protected virtual void Start()
    {

    }

    protected virtual void StartDialogue()
    {
        
    }
    protected virtual void UpdateGameState(GameLoop gl, GamePhase ph)
    {
        
    }

    protected virtual void ChangeInteraction()
    {
        this.canBeInteracted = true;
    }

    protected virtual void UpdateGameLoop(GameLoop loop)
    {

    }
}
