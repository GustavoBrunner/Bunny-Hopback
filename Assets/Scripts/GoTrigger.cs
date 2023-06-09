using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTrigger : CameraTriggers
{
    protected override void Awake()
    {
        base.Awake();
        // CameraTriggers.FirstCenterTriggerEntered += TurnTriggerOn;
        // CameraTriggers.FirstCamTriggerBack += TurnTriggersOff;
        // CameraTriggers.SecondCamTriggerBack += TurnTriggersOff;
        // CameraTriggers.ThirdCamTriggerBack += TurnTriggersOff;
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
