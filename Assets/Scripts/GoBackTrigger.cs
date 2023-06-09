using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackTrigger : CameraTriggers
{
    protected override void Awake()
    {
        base.Awake();
        // CameraTriggers.FirstCenterTriggerEntered += TurnObjectOn;
        
        // CameraTriggers.FirstCamTriggerGo += TurnTriggersOff;
        // CameraTriggers.SecondCamTriggerGo += TurnTriggersOff;
        // CameraTriggers.ThirdCamTriggerGo += TurnTriggersOff;
        TurnTriggersOff();
    }
    void TurnTriggersOff()
    {
        gameObject.SetActive(false);
    }
    void TurnTriggerOn()
    {
        gameObject.SetActive(true);
    }
}
