using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    public MonoModule monoModule { get { return ModuleManager.Instance.monoModule; } }
    public PoolModule poolModule { get { return ModuleManager.Instance.poolModule; } }
    public EventModule eventModule { get { return ModuleManager.Instance.eventModule; } }


    private void OnEnable()
    {
        monoModule.AddUpdate(OnUpdate);
        monoModule.AddFixedUpdate(OnFixedUpdate);
    }

    private void OnDisable()
    {
        monoModule.RemoveUpdate(OnUpdate);
        monoModule.RemoveFixedUpdate(OnFixedUpdate);
    }

    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }

}
