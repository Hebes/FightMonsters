using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeBase : MonoBehaviour
{
   protected PlayerComponent playerComponent;

    private void Awake()
    {
        playerComponent = GetComponent<PlayerComponent>();
    }

    private void OnEnable()
    {
        ModuleManager.Instance.monoModule.AddStart(OnStart);
        ModuleManager.Instance.monoModule.AddUpdate(OnUpdate);
        ModuleManager.Instance.monoModule.AddFixedUpdate(OnFixedUpdate);
    }

    private void OnDisable()
    {
        ModuleManager.Instance.monoModule.RemoveStart(OnStart);
        ModuleManager.Instance.monoModule.RemoveUpdate(OnUpdate);
        ModuleManager.Instance.monoModule.RemoveFixedUpdate(OnFixedUpdate);
    }

   
    public virtual void OnStart()
    {

    }

    public virtual void  OnUpdate()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }
}
