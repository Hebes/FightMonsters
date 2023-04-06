using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


/// <summary>
/// ������
/// </summary>
public class PlayerShoot : PlayeBase
{
    private Action<OnShootEvnentArgs> actionShoot;//�������

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
    /// �������¼�
    /// </summary>
    /// <param name="arg0"></param>
    private void ShootingOverTrigger(OnShootEvnentArgs arg0)
    {
        actionShoot(arg0);
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="arg0"></param>
    private void ShootPhysics(OnShootEvnentArgs arg0)
    {
        ModuleManager.Instance.poolModule.GetObj(Config.Bullet, (go) =>
        {
            //����Ϊ�������õķ���
            go.transform.position = arg0.tfShootPoint.position;//����� �ӵ����ɵ�λ��
            Vector3 shootDir = (arg0.tfShootPoint.position - arg0.tfGunEndPoint.position).normalized;//�ӵ��ĳ���
            go.GetComponent<IBulletSetup>().Setup(shootDir);
        });
    }
}
