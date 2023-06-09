using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggers : ObjectsScript
{
    public delegate void GoAndBackTriggersHandler(int tIndex);
    protected Collider clldr;
    protected List<CameraTriggers> triggers = new List<CameraTriggers>();

    
    public static event GoAndBackTriggersHandler FirstCamTriggerGo;
    public static event GoAndBackTriggersHandler SecondCamTriggerGo;
    public static event GoAndBackTriggersHandler ThirdCamTriggerGo;
    public static event GoAndBackTriggersHandler FirstCamTriggerBack;
    public static event GoAndBackTriggersHandler SecondCamTriggerBack;
    public static event GoAndBackTriggersHandler ThirdCamTriggerBack;
    public static event GoAndBackTriggersHandler FirstCenterTriggerEntered;
    //public static event GoAndBackTriggersHandler SecondCenterTriggerEntered;
    //public static event GoAndBackTriggersHandler ThirdCenterTriggerEntered;




    protected override void Awake()
    {
        base.Awake();
        clldr = GetComponent<Collider>();
        clldr.isTrigger = true;
        triggers.AddRange(GetComponentsInChildren<CameraTriggers>());
        clldr.enabled = true;
        //TurnAllOff();
    }
    protected virtual void TurnObjectOn()
    {
        //Respons�vel por ligar todos os triggers da c�mera
        gameObject.SetActive(true);
    }
    protected virtual void TurnObjectOff()
    {
        //Respons�vel por desligar todos os triggers da c�mera
        gameObject.SetActive(false);
    }
    protected void TurnAllOff()
    {
        //Desliga todos os triggers ao iniciar a fase
        foreach (var trigger in triggers)
        {
            if(this.gameObject.name != "DoorTrigger1")
            {
                TurnObjectOff();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Verifica qual que � o side trigger que foi entrado pelo player, e voltar� a navega��o de acordo
        if(other.gameObject.tag == "Player")
        {
            PlayerScript.playerInTrigger = true;
            switch (this.gameObject.name)
            {
                case "CamTrigger1":
                    if (FirstCamTriggerGo != null)
                    {
                        FirstCamTriggerGo(0);
                    }
                    break;
                case "CamTrigger2":
                    if (FirstCamTriggerGo != null)
                    {
                        FirstCamTriggerGo(1);
                    }
                    break;
                case "CamTrigger3":
                    if (FirstCamTriggerGo != null)
                    {
                        FirstCamTriggerGo(2);
                    }
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerScript.playerInTrigger = false;
        }
    }
}
