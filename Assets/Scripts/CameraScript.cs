using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScript : MonoBehaviour
{
    public delegate void CameraMoveHandler(int x);
    public static event CameraMoveHandler CameraMove;
    Transform tf;

    public Animator animator { get; protected set; }
    private float moveSpeed = 1.7f;
    [SerializeField]
    private Transform[] PlaceHolders = new Transform[6];
    public int placeHolderIndex;
    [SerializeField]
    private GameObject[] Cams = new GameObject[5];
    private string ActualCamera;
    private string NextCamera;
    public static bool SecondaryCam;
    
    protected virtual void Awake()
    {
        
        animator = GetComponent<Animator>();
        ActualCamera = "CameraContainerCorridor";
        tf = GetComponent<Transform>();
        PlaceHolders = GameObject.FindGameObjectWithTag("PlaceHolders")
            .GetComponentsInChildren<Transform>();
        placeHolderIndex = 5;
        CameraTriggers.FirstCamTriggerGo += ChangePlaceHolderIndex;

        //Cams = GameObject.FindGameObjectsWithTag("Camera");
        TurnOffAllCams();
        SceneTriggers.BedroomEntered += FadeCam;
        SceneTriggers.BathroomEntered += FadeCam;
        SceneTriggers.ParentsRoomEntered += FadeCam;

        //GameEvents.onRestartDayEvent.AddListener(RestartDay);
    }
    private void LateUpdate()
    {
        checkTriggerIndex(placeHolderIndex);
        
    }

    private void moveCam(int camIndex)
    {
        if(this.gameObject.name == "CameraContainerCorridor")
        {
            Vector3 target = new Vector3(PlaceHolders[camIndex].position.x, tf.position.y, tf.position.z);
            transform.position = Vector3.Lerp(transform.position,
                            target, moveSpeed * Time.deltaTime  );
        }
    }
    private void checkTriggerIndex(int tIndex)
    {
        switch(tIndex)
        {
            case 0:
                if(PlayerScript._horizontal > 0 )
                {
                    moveCam(2);
                    StartCoroutine(StopCamera());
                }
                else if(PlayerScript._horizontal < 0 )
                {
                    moveCam(1);
                    StartCoroutine(StopCamera());
                }
                break;
            case 1:
                if(PlayerScript._horizontal > 0)
                {
                    moveCam(3);
                    StartCoroutine(StopCamera());
                }
                else if(PlayerScript._horizontal < 0)
                {
                    moveCam(2);
                    StartCoroutine(StopCamera());
                }
                break;
            case 2:
                if(PlayerScript._horizontal > 0)
                {
                    moveCam(4);
                    StartCoroutine(StopCamera());
                } else if (PlayerScript._horizontal < 0)
                {
                    moveCam(3);
                    StartCoroutine(StopCamera());
                }
                break;
            case 3:
                if(PlayerScript._horizontal > 0)
                {
                    moveCam(5);
                    StartCoroutine(StopCamera());
                } else if(PlayerScript._horizontal < 0)
                {
                    moveCam(4);
                    StartCoroutine(StopCamera());
                }
                break;

                default:
                break;
        }
    }
    private void CheckNextCamera(string nc)
    {
        foreach (var _cam in Cams)
        {
            if (_cam.gameObject.name == nc)
            {
                _cam.gameObject.SetActive(true);
                return;
            }
        }
    }
    void ChangePlaceHolderIndex(int index)
    {
        placeHolderIndex = index;
    }
    virtual protected void TurnCurrentCamOff()
    {
        if(this.gameObject.name == ActualCamera)
        {
            gameObject.SetActive(false);
        }
        CheckNextCamera(NextCamera);
    }
    //função que irá ativar a transição da primeira tela
    //e a animação disparará o evento de desligar uma câmera e ligar a próxima
    private void FadeCam(string _lastCam, string _nextCam)
    {
        ActualCamera = _lastCam;
        NextCamera = _nextCam;
        this.animator.SetTrigger("EnterBedroom");
    }
    private IEnumerator StopCamera()
    {
        if(!PlayerScript.playerInTrigger)
        {
            yield return new WaitForSeconds(0.1f);
            placeHolderIndex = 5;
        }
    }
    private IEnumerator TurnOffCameraTimer(string _ac)
    {
        animator.SetTrigger("LeaveBedroom");
        yield return new WaitForSeconds(1f);
        //TurnCurrentCamOff(_ac);
    }
    private IEnumerator TurnSecondCameraTimer(GameObject goCam)
    {
        yield return new WaitForSeconds(1f);
        goCam.gameObject.SetActive(true);
        
    }
    public void RestartDay()//chamado por animação
    {
        Vector3 initialPosition = new Vector3
            (
            PlaceHolders[0].position.x
            , transform.position.y
            , transform.position.z
            );
        this.transform.position = initialPosition;
        foreach (var cam in Cams)
        {
            if(cam.name == "CameraContainerBedroom")
            {
                cam.gameObject.SetActive(true);
            }
            else
            {
                cam.gameObject.SetActive(false);
            }
        }
    }

    public void TurnOffAllCams()
    {
        foreach (var cam in Cams)
        {
            if (this.gameObject.name != "CameraContainerBedroom")
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void StartDialogue()
    {
        GameController._instance.CheckWichDialogue(GameController._instance._phase);
    }
    public void SecondCutsceneDialogue()
    {
        PlayerScript.instance.CutsceneDialogue(2);
    }
    public void SecondTransitionDialogue()
    {

    }
}
