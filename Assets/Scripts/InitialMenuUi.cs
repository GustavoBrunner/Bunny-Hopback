using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuUi : MonoBehaviour
{
    private bool firstTimePlaying;
    private Animator animator;
    void Start()
    {
        firstTimePlaying = true;
        animator = GetComponent<Animator>();
        animator?.SetTrigger("InitialMenu");
        if(firstTimePlaying)
        {
            firstTimePlaying = false;
            AudioController.instance.RestartMusic();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void CreditsMenu()
    {
        //ficará o menu de opções, ou créditos
        SceneManager.LoadScene("Credits");
    }
    public void StartMenu()
    {
        SceneManager.LoadScene("InitialMenu");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }
    public void TutorialMenu()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
