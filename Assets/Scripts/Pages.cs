using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : Diary
{
    
    public void ClosePage()
    {
        this.transform.localScale = Vector3.zero;
    }
    public void ShowPage()
    {
        this.transform.localScale = Vector3.one;
    }
}
