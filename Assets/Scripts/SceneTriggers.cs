using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneTriggers : MonoBehaviour
{
    public delegate void SceneManagerHandler(string lasCam, string nexCam);
    public static event SceneManagerHandler BedroomEntered;
    public static event SceneManagerHandler BedroomExited;
    public static event SceneManagerHandler ParentsRoomEntered;
    public static event SceneManagerHandler ParentsRoomExited;
    public static event SceneManagerHandler DispenseEntered;
    public static event SceneManagerHandler DispenseExited;
    public static event SceneManagerHandler BathroomEntered;
    public static event SceneManagerHandler BathroomExited;
    public delegate void PlayerMovementHandler();
    public static event PlayerMovementHandler StopPlayer;
    private Collider clldr;
    [SerializeField]
    public List<GameObject> exitTriggers = new List<GameObject>();
    [SerializeField]
    private GameObject nextTrigger;
    [SerializeField]
    private List<GameObject> enterTriggers = new List<GameObject>();
    private void Awake()
    {
        clldr = GetComponent<Collider>();
        clldr.isTrigger = true;
        exitTriggers.AddRange(GameObject.FindGameObjectsWithTag("ExitTrigger"));
        enterTriggers.AddRange(GameObject.FindGameObjectsWithTag("EnterTrigger"));
        
    }
    private void Start()
    {
        foreach (var trigger in this.exitTriggers)
        {
            //trigger.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch(this.gameObject.name)
            {
                case "BedroomTrigger":
                    nextTrigger = exitTriggers[0];
                    Debug.Log(nextTrigger);
                    if (PlayerScript._vertical > 0)
                    {
                        if (BedroomEntered != null)
                        {
                            BedroomEntered("CameraContainerCorridor", "CameraContainerBedroom");
                        }
                        if(StopPlayer != null)
                        {
                            StopPlayer();
                        }
                        TurnTriggerOn();
                        
                    }
                    break;
                case "BathroomTrigger":
                    nextTrigger = exitTriggers[1];
                    Debug.Log("teste1");
                    if (PlayerScript._vertical > 0)
                    {
                        Debug.Log("teste2");
                        if (BedroomEntered != null)
                        {
                            Debug.Log("teste3");
                            BedroomEntered("CameraContainerCorridor", "CameraContainerBathroom");
                        }
                        if (StopPlayer != null)
                        {
                            StopPlayer();
                        }
                        TurnTriggerOn();

                    }
                    break;
                case "ParentsRoomTrigger":
                    nextTrigger = exitTriggers[2];
                    if (PlayerScript._vertical > 0)
                    {
                        if (BedroomEntered != null)
                        {
                            BedroomEntered("CameraContainerCorridor", "CameraContainerParents");
                        }
                        if (StopPlayer != null)
                        {
                            StopPlayer();
                        }
                        TurnTriggerOn();
                    }
                    break;
                case "BedroomTriggerExit":
                    nextTrigger = enterTriggers[0];
                    if (PlayerScript._vertical > 0)
                    {
                        if (BedroomEntered != null)
                        {
                            BedroomEntered("CameraContainerBedroom", "CameraContainerCorridor");
                        }
                        if (StopPlayer != null)
                        {
                            StopPlayer();
                        }
                        TurnTriggerOn();
                    }
                    break;
                case "BathroomTriggerExit":
                    nextTrigger = enterTriggers[1];
                    if (PlayerScript._vertical > 0)
                    {
                        if (BedroomEntered != null)
                        {
                            BedroomEntered("CameraContainerBathroom",  "CameraContainerCorridor");
                        }
                        if (StopPlayer != null)
                        {
                            StopPlayer();
                        }
                        TurnTriggerOn();
                    }
                    break;
                case "ParentsRoomTriggerExit":
                    nextTrigger = enterTriggers[2];
                    if (PlayerScript._vertical > 0)
                    {
                        if (BedroomEntered != null)
                        {
                            BedroomEntered("CameraContainerParents" , "CameraContainerCorridor");
                        }
                        if (StopPlayer != null)
                        {
                            StopPlayer();
                        }
                        TurnTriggerOn();
                    }
                    break;
                case "AtticTrigger":
                    nextTrigger = exitTriggers[3];
                    if (PlayerScript._vertical > 0)
                    {
                        if(!PlayerScript.instance.hasFlashLight)
                        {
                            PlayerScript.instance.AtticDialogueTrigger();
                        }
                        else
                        {
                            if (BedroomEntered != null)
                            {
                                BedroomEntered("CameraContainerCorridor", "CameraContainerAttic");
                            }
                            if (StopPlayer != null)
                            {
                                StopPlayer();
                            }
                            TurnTriggerOn();
                            StartCoroutine(ChangePlayerPosition("Attic"));
                        }
                    }
                    break;
                case "AtticTriggerExit":
                    nextTrigger = enterTriggers[3];
                    if (PlayerScript._vertical > 0)
                    {
                        if (BedroomEntered != null)
                        {
                            BedroomEntered("CameraContainerAttic", "CameraContainerCorridor");
                        }
                        if (StopPlayer != null)
                        {
                            StopPlayer();
                        }
                        TurnTriggerOn();
                    }
                    break;  
                default:
                    break;
            }
        }
    }
    private IEnumerator StopTrigger()
    {
        
        yield return new WaitForSeconds(3.0f);
        nextTrigger.gameObject.SetActive(true);
    }
    private void TurnTriggerOn()
    {
        nextTrigger.gameObject.SetActive(false);
        StartCoroutine(StopTrigger());
        
    }
    private IEnumerator ChangePlayerPosition(string room)
    {
        yield return new WaitForSeconds(2f);
        PlayerScript.instance.UpdatePosition(room);
    }
}
