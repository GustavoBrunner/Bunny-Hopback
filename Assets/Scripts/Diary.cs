using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    [SerializeField]
    private List<Pages> pages = new List<Pages>();

    private Pages lastPage { get { return pages[index - 1]; }  }
    public Pages nextPage { get { return pages[index]; } private set { } }
    private int index;
    private void Awake()
    {
        index = 0;
        pages.AddRange(gameObject.GetComponentsInChildren<Pages>());
        
        CloseDiary();
        GameEvents.onDiaryInteracted.AddListener(OpenDiary);
    }

    public void NextPage()
    {
        Debug.Log(nextPage);
        if(nextPage != null )
        {
            if(index == pages.Count -1)
            {
                CloseDiary();
                //Liberará o evento de final do jogo.
            }
            index++;
            nextPage = pages[index];
            nextPage.ShowPage();
            lastPage.ClosePage();
        }
        
        
    }
    public void CloseDiary()
    {
        gameObject.transform.localScale = Vector3.zero;
        index = 0;
    }
    public void OpenDiary()
    {
        index = 0;
        gameObject.transform.localScale = Vector3.one;
        foreach (var page in pages)
        {
            if(page.gameObject.name != "Page")
                page.ClosePage();
        }
    }
}
