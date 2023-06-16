using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUiController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartCredits()
    {
        animator.SetTrigger("Credits");
    }
    public void StartThanks()
    {
        animator.SetTrigger("Thanks");
    }

    public void EndApplication()
    {
        Application.Quit();
    }

}
