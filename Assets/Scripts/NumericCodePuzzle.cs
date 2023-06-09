using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumericCodePuzzle : MonoBehaviour
{
    public delegate void NumericChange(int x);
    public static event NumericChange OnFirstNumberChange;
    public static event NumericChange OnSecondNumberChange;
    public static event NumericChange OnThirdNumberChange;
    public static event NumericChange OnForthNumberChange;
    public delegate void CloseTerminal();
    public static event CloseTerminal OnTerminalClosed;
    [SerializeField] private List<Sprite> numberImages = new List<Sprite>();
    private Image ActualImage;
    public int ActualNumber;
    public int FirstNumber;
    public int LastNumber;
    public int SecondNumber;
    public int ThirdNumber;
    public int[] Combination;
    

    private void Awake()
    {
        ActualNumber = 0;
        ActualImage = GetComponent<Image>();
        ActualImage.sprite = numberImages[ActualNumber];
    }
    
    public void ChangeNumber()
    {
        switch(this.gameObject.name)
        {
            case "FirstNumber":
                if (ActualNumber <= 9)
                {
                    this.ActualNumber++;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    FirstNumber = this.ActualNumber;
                    if(OnFirstNumberChange != null)
                    {
                        OnFirstNumberChange(FirstNumber);
                    }
                    Debug.Log(FirstNumber);
                }
                else
                {
                    ActualNumber = 0;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    FirstNumber = this.ActualNumber;
                    if (OnFirstNumberChange != null)
                    {
                        OnFirstNumberChange(FirstNumber);
                    }
                    Debug.Log(FirstNumber);
                }
                break;
            case "SecondNumber":
                if (ActualNumber <= 9)
                {
                    this.ActualNumber++;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    SecondNumber = this.ActualNumber;
                    if (OnSecondNumberChange != null)
                    {
                        OnSecondNumberChange(SecondNumber);
                    }
                    Debug.Log(SecondNumber);
                }
                else
                {
                    ActualNumber = 0;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    SecondNumber = this.ActualNumber;
                    if (OnSecondNumberChange != null)
                    {
                        OnSecondNumberChange(SecondNumber);
                    }
                    Debug.Log(SecondNumber);
                }
                break;
            case "ThirdNumber":
                if (ActualNumber <= 9)
                {
                    this.ActualNumber++;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    ThirdNumber = this.ActualNumber;
                    if (OnThirdNumberChange != null)
                    {
                        OnThirdNumberChange(ThirdNumber);
                    }
                    Debug.Log(ThirdNumber);
                }
                else
                {
                    ActualNumber = 0;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    ThirdNumber = this.ActualNumber;
                    if (OnThirdNumberChange != null)
                    {
                        OnThirdNumberChange(ThirdNumber);
                    }
                    Debug.Log(ThirdNumber);
                }
                break;
            case "ForthNumber":
                if (ActualNumber <= 9)
                {
                    this.ActualNumber++;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    LastNumber = this.ActualNumber;
                    if (OnForthNumberChange != null)
                    {
                        OnForthNumberChange(LastNumber);
                    }
                    Debug.Log(LastNumber);
                }
                else
                {
                    ActualNumber = 0;
                    this.ActualImage.sprite = numberImages[ActualNumber];
                    LastNumber = this.ActualNumber;
                    if (OnForthNumberChange != null)
                    {
                        OnForthNumberChange(LastNumber);
                    }
                    Debug.Log(LastNumber);
                }
                break;
                default:
                break;
        }
    }
    
    public void CloseTerminalEvent()
    {
        GameEvents.onCloseTerminal.Invoke();
        ActualNumber = 0;
        
    }
}
