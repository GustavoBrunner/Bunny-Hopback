using UnityEngine;

[CreateAssetMenu(fileName ="ScenesInfo", menuName ="DTO")]
public class ScenesInfo : ScriptableObject
{
    public static bool EnableThirdPuzzle = false;

    public static int[] ThirdPuzzleCode = new int[4];
    void ChangeThirdPuzzleInfo()
    {

    }


}
