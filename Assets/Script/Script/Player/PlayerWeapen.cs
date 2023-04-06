using CodeMonkey.Utils;
using Core;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 玩家射击参数
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
    /// 能否射击
    /// </summary>
    private bool canShoot = true;

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleAiming();
        if (!InteractwithUI())
        {
            HandleShooting();//修正穿透UI点进
        }
    }

    /// <summary>
    /// 射击的方法
    /// </summary>
    private void HandleShooting()
    {
        //TUDO 在安卓手机中可能不能运行
        //以下方法适用PC
        if (canShoot && Input.GetMouseButton(Config.Key_Mouse_Left))
        {
            canShoot = false;
            //播放枪械攻击动击动画
            //playerComponent.Player_Gun_Animator.SetTrigger(Config_Animator.gun_Trigger_Animator_Shoot);

            //发布攻击事件 用来添加枪射击完毕后需要处理的事情，比如屏幕抖动
            ModuleManager.Instance.eventModule.EventTrigger(EventType.playerShoot, new OnShootEvnentArgs
            {
                tfGunEndPoint = playerComponent.T_GunSpriteTransform,
                tfShootPoint = playerComponent.T_Gun_ShootPointTransform,
            });
            StartCoroutine(enumerator());//开火间隔
        }
    }

    //武器的开火间隔
    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
    }

    /// <summary>
    /// 处理瞄准目标的方法
    /// </summary>
    private void HandleAiming()
    {
        //设置枪械变换朝向
        if (MouseRightOrLeft(out float angle))
        {
            playerComponent.T_GunTransform.localScale = new Vector2(-1, -1);//枪旋转
            transform.localScale = new Vector2(-1, 1);//玩家旋转
        }
        else
        {
            playerComponent.T_GunTransform.localScale = Vector2.one;//枪旋转
            transform.localScale = Vector2.one; //玩家旋转
        }
        playerComponent.T_GunTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    /// <summary>
    /// 判断鼠标是左边还是右边
    /// </summary>
    /// <returns></returns>
    private bool MouseRightOrLeft(out float vector)
    {
        //鼠标在屏幕的位置
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        //偏移量
        Vector3 aimDir = (mousePosition - transform.position).normalized;
        //返回Tan为y/x的角度(以弧度为单位)。弧度到角度转换常数(只读)。
        float angle = vector = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        return (angle > 90 || angle < -90) ? true : false;
    }

    private bool InteractwithUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
