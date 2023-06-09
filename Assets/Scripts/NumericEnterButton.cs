using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericEnterButton : MonoBehaviour
{
    public delegate void OnNumericCodeSent(int[] code);
    public static event OnNumericCodeSent CodeSent;
    [SerializeField]private int[] combination = new int[4];
    private void Awake()
    {
        NumericCodePuzzle.OnFirstNumberChange += FirstNumberChange;
        NumericCodePuzzle.OnSecondNumberChange += SecondNumberChange;
        NumericCodePuzzle.OnThirdNumberChange += ThirdNumberChange;
        NumericCodePuzzle.OnForthNumberChange += ForthNumberChange;
    }

    void FirstNumberChange(int x)
    {
        combination[0] = x;
    }
    void SecondNumberChange(int x)
    {
        combination[1] = x;
    }
    void ThirdNumberChange(int x)
    {
        combination[2] = x;
    }
    void ForthNumberChange(int x)
    {
        combination[3] = x;
    }
    public void SendCombination()
    {
        if(CodeSent != null)
        {
            CodeSent(combination);
        }
    }
}
