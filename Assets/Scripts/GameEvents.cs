using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DialogueEvent : UnityEvent<string, string> { }
public class UpdatePhaseEvent : UnityEvent<GameLoop, GamePhase> { } 

public class RestartDayEvent : UnityEvent { }

public class InventoryClear : UnityEvent { }
public class PuzzleItemCollected : UnityEvent<GameObject> { }

public class CluePuzzle : UnityEvent { }

public class CutsceneDialogue : UnityEvent<int> { }
public class CamPosUpdate : UnityEvent<int> { }


public class GameEvents 
{
    public static DialogueEvent dialogue = new DialogueEvent();

    public static UpdatePhaseEvent onUpdatePhase = new UpdatePhaseEvent();

    public static CluePuzzle onFreezer = new CluePuzzle();

    public static CluePuzzle onCabinet = new CluePuzzle();

    public static CluePuzzle onCloseTerminal = new CluePuzzle();

    //Libera o próximo puzzle quando interagindo com o coelho
    public static UpdatePhaseEvent onPuzzleEnabled = new UpdatePhaseEvent();

    public static InventoryClear onInventoryClear = new InventoryClear();

    public static PuzzleItemCollected onItemPicked = new PuzzleItemCollected();

    public static RestartDayEvent onRestartDayEvent = new RestartDayEvent();

    public static CamPosUpdate onUpdateCamPos = new CamPosUpdate();

    public static CutsceneDialogue onStartDialogue = new CutsceneDialogue();

    public static UnityEvent EnableFirstItem = new UnityEvent();
    public static UnityEvent GetSecondItem = new UnityEvent();
    public static UnityEvent GetFlashLight = new UnityEvent();

    public static UnityEvent TransitionDialogue = new UnityEvent();

    public static UnityEvent onDiaryInteracted = new UnityEvent();
    
}
