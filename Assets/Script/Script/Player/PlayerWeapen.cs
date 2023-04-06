using CodeMonkey.Utils;
using Core;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ����������
/// </summary>
public class OnShootEvnentArgs
{
    public Transform tfShootPoint;
    public Transform tfGunEndPoint;
    public Vector3 shellPosition;
}

public class PlayerWeapen : PlayeBase
{
    /// <summary>
    /// �ܷ����
    /// </summary>
    private bool canShoot = true;

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleAiming();
        if (!InteractwithUI())
        {
            HandleShooting();//������͸UI���
        }
    }

    /// <summary>
    /// ����ķ���
    /// </summary>
    private void HandleShooting()
    {
        //TUDO �ڰ�׿�ֻ��п��ܲ�������
        //���·�������PC
        if (canShoot && Input.GetMouseButton(Config.Key_Mouse_Left))
        {
            canShoot = false;
            //����ǹе������������
            //playerComponent.Player_Gun_Animator.SetTrigger(Config_Animator.gun_Trigger_Animator_Shoot);

            //���������¼� �������ǹ�����Ϻ���Ҫ��������飬������Ļ����
            ModuleManager.Instance.eventModule.EventTrigger(EventType.playerShoot, new OnShootEvnentArgs
            {
                tfGunEndPoint = playerComponent.T_GunSpriteTransform,
                tfShootPoint = playerComponent.T_Gun_ShootPointTransform,
            });
            StartCoroutine(enumerator());//������
        }
    }

    //�����Ŀ�����
    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
    }

    /// <summary>
    /// ������׼Ŀ��ķ���
    /// </summary>
    private void HandleAiming()
    {
        //����ǹе�任����
        if (MouseRightOrLeft(out float angle))
        {
            playerComponent.T_GunTransform.localScale = new Vector2(-1, -1);//ǹ��ת
            transform.localScale = new Vector2(-1, 1);//�����ת
        }
        else
        {
            playerComponent.T_GunTransform.localScale = Vector2.one;//ǹ��ת
            transform.localScale = Vector2.one; //�����ת
        }
        playerComponent.T_GunTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    /// <summary>
    /// �ж��������߻����ұ�
    /// </summary>
    /// <returns></returns>
    private bool MouseRightOrLeft(out float vector)
    {
        //�������Ļ��λ��
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        //ƫ����
        Vector3 aimDir = (mousePosition - transform.position).normalized;
        //����TanΪy/x�ĽǶ�(�Ի���Ϊ��λ)�����ȵ��Ƕ�ת������(ֻ��)��
        float angle = vector = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        return (angle > 90 || angle < -90) ? true : false;
    }

    private bool InteractwithUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
