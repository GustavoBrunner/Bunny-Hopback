
public class GamePhaseChecker 
{
    public static string NonePhase { get => "NonePhase"; private set { } }
    public static string FirstPhase { get => "FirstPhase"; private set { } }
    public static string FirstQuestPhase { get => "FirstQuestPhase"; private set { } }
    public static string FirstQuestPhaseLoop1 { get => "FirstQuestPhaseLoop1"; private set { } }
    public static string FirstQuestPhaseLoop2 { get => "FirstQuestPhaseLoop2"; private set { } }
    public static string FirstQuestEndPhase { get => "FirstQuestEndPhase"; private set { } }
    public static string FirstQuestEndLoop1Phase { get => "FirstQuestEndLoop1Phase"; private set { } }
    public static string FirstQuestEndLoop2Phase { get => "FirstQuestEndLoop2Phase"; private set { } }
    public static string SecondQuestPhase { get => "SecondQuestPhase"; private set { } }
    public static string SecondQuestPhaseLoop1 { get => "SecondQuestPhaseLoop1"; private set { } }
    public static string SecondQuestPhaseLoop2 { get => "SecondQuestPhaseLoop2"; private set { } }
    public static string SecondQuestEndPhase { get => "SecondQuestEndPhase"; private set { } }
    public static string SecondQuestEndPhaseLoop1 { get => "SecondQuestEndPhaseLoop1"; private set { } }
    public static string SecondQuestEndPhaseLoop2 { get => "SecondQuestEndPhaseLoop2"; private set { } }
    public static string ThirdQuestPhase { get => "ThirdQuestPhase"; private set { } }
    public static string ThirdQuestEndPhase { get => "ThirdQuestEndPhase"; private set { } }
    public static string FinalPhase { get => "FinalPhase"; private set { } }
    public static string BetweenLoop1Phase { get => "BetweenLoop1Phase"; private set { } }
    public static string BetweenLoop2Phase { get => "BetweenLoop2Phase"; private set { } }

    public static string PhaseChecker(GameLoop gl, GamePhase gp)
    {
        var phase = "";
        if(gl == GameLoop.None )
        {
            if(gp == GamePhase.Start)
            {
                phase = FirstPhase;
            }
        }
        if(gl == GameLoop.None )
        {
            if(gp == GamePhase.StartFirstPuzzle)
            {
                phase = FirstQuestPhase;
            }
        }
        if(gl == GameLoop.None )
        {
            if(gp == GamePhase.EndFirstPuzzle)
            {
                phase = FirstQuestEndPhase;
            }
        }
        if(gl == GameLoop.First )
        {
            if(gp == GamePhase.Start)
            {
                phase = BetweenLoop1Phase;
            }
        }
        if(gl == GameLoop.First )
        {
            if(gp == GamePhase.StartFirstPuzzle)
            {
                phase = FirstQuestPhaseLoop1;
            }
        }
        if(gl == GameLoop.First )
        {
            if(gp == GamePhase.EndFirstPuzzle)
            {
                phase = FirstQuestEndLoop1Phase;
            }
        }
        if(gl == GameLoop.First )
        {
            if(gp == GamePhase.StartSecondPuzzle)
            {
                phase = SecondQuestPhaseLoop1;
            }
        }
        if(gl == GameLoop.First )
        {
            if(gp == GamePhase.EndSecondPuzzle)
            {
                phase = SecondQuestEndPhaseLoop1;
            }
        }
        if(gl == GameLoop.Second )
        {
            if(gp == GamePhase.Start)
            {
                phase = BetweenLoop2Phase;
            }
        }
        if(gl == GameLoop.Second)
        {
            if(gp == GamePhase.StartFirstPuzzle)
            {
                phase = FirstQuestPhaseLoop2;
            }
        }
        if(gl == GameLoop.Second)
        {
            if(gp == GamePhase.EndFirstPuzzle)
            {
                phase = FirstQuestEndLoop2Phase;
            }
        }
        if(gl == GameLoop.Second)
        {
            if(gp == GamePhase.StartSecondPuzzle)
            {
                phase = SecondQuestPhaseLoop2;
            }
        }
        if(gl == GameLoop.Second)
        {
            if(gp == GamePhase.EndSecondPuzzle)
            {
                phase = SecondQuestEndPhaseLoop2;
            }
        }
        if(gl == GameLoop.Second)
        {
            if(gp == GamePhase.StartThirdPuzzle)
            {
                phase = ThirdQuestPhase;
            }
        }
        if(gl == GameLoop.Second)
        {
            if(gp == GamePhase.EndThirdPuzzle)
            {
                phase = ThirdQuestEndPhase;
            }
        }
        if(gl == GameLoop.Second)
        {
            if(gp == GamePhase.Final)
            {
                phase = FinalPhase;
            }
        }
        return phase;
    }

}
