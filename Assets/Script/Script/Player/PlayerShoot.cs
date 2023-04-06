using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


/// <summary>
/// 玩家射击
/// </summary>
public class PlayerShoot : PlayeBase
{
    private Action<OnShootEvnentArgs> actionShoot;//射击方法

    private void Awake()
    {
        actionShoot = ShootPhysics;
        ModuleManager.Instance.eventModule.AddEventListener<OnShootEvnentArgs>(EventType.playerShoot, ShootingOverTrigger);
    }

    private void OnDestroy()
    {
        ModuleManager.Instance.eventModule.RemoveEventListener<OnShootEvnentArgs>(EventType.playerShoot, ShootingOverTrigger);
    }

    /// <summary>
    /// 监听的事件
    /// </summary>
    /// <param name="arg0"></param>
    private void ShootingOverTrigger(OnShootEvnentArgs arg0)
    {
        actionShoot(arg0);
    }

    /// <summary>
    /// 射击方法
    /// </summary>
    /// <param name="arg0"></param>
    private void ShootPhysics(OnShootEvnentArgs arg0)
    {
        ModuleManager.Instance.poolModule.GetObj(Config.Bullet, (go) =>
        {
            //以下为开火后调用的方法
            go.transform.position = arg0.tfShootPoint.position;//开火点 子弹生成的位置
            Vector3 shootDir = (arg0.tfShootPoint.position - arg0.tfGunEndPoint.position).normalized;//子弹的朝向
            go.GetComponent<IBulletSetup>().Setup(shootDir);
        });
    }
}
