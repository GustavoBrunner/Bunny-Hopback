using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamParentsRoom : CameraScript
{
    private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
