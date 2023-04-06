using CodeMonkey.Utils;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCommon : MonoBehaviour, IBulletSetup
{
    private Vector3 shootDir;
    private float moveSpeed = 100f;

    private void OnEnable()
    {
        ModuleManager.Instance.monoModule.AddUpdate(OnUpdate);
    }

    private void OnDisable()
    {
        ModuleManager.Instance.monoModule.RemoveUpdate(OnUpdate);
    }

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="shootDir"></param>
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        GetComponent<Rigidbody2D>().AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);//添加推力 和 Bullet_Common 移动的区别
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));//子弹生成的朝向
        //没有碰撞到物体的在外面飞的子弹的销毁
        StartCoroutine(Push(() => { Push(); }, 2));
    }

    private void OnUpdate()
    {
        //子弹移动
        transform.position += shootDir * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.Log("进入了碰撞");
        //ICommonCollide target = collision.GetComponent<ICommonCollide>();
        //if (target != null)
        //{
        //    // Hit enemy 敌人伤害
        //    int damageAmount = UnityEngine.Random.Range(100, 200);//随机伤害
        //    bool isCritical = UnityEngine.Random.Range(0, 100) < 30;//是否重击
        //    if (isCritical) damageAmount *= 2;//重击伤害*2

        //    target.Damage(damageAmount);
        //    StartCoroutine(Push(() => { Push(); }, 0));

        //    //显示伤害文字效果
        //    //Component_Helper.Show_pf_Damage(collision.transform.position, damageAmount, isCritical);
        //    //加载特效
        //    //Component_Helper.LoadEffect(Config_ResLoadPaths.Gun_pf_Effect, collision.transform.position);
        //}
    }

    /// <summary>
    /// 销毁子弹到对象池
    /// </summary>
    IEnumerator Push(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action?.Invoke();
    }

    private void Push()
    {
        ModuleManager.Instance.poolModule.PushObj(gameObject.name, gameObject);
    }
}
