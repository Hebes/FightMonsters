using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCamera : MonoBehaviour
{
    private Transform target;
    public Vector3 offset = new Vector3(0, 0, -1);

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag(Config.playerTag).transform;
        ModuleManager.Instance.monoModule.AddUpdate(OnUpdate);
    }
    private void OnUpdate()
    {
        transform.position = target.position + offset;
    }
}
