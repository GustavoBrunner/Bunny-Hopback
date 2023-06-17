using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuUi : MonoBehaviour
{

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator?.SetTrigger("InitialMenu");
        AudioController.instance.RestartMusic();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        AudioController.instance.RestartMusic();
    }
    public void CreditsMenu()
    {
        //ficará o menu de opções, ou créditos
        SceneManager.LoadScene("Credits");
        AudioController.instance.RestartMusic();
    }
    public void StartMenu()
    {
        SceneManager.LoadScene("InitialMenu");
        AudioController.instance.RestartMusic();
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
        AudioController.instance.RestartMusic();
    }
}
