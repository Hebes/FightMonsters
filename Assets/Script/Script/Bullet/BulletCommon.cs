using CodeMonkey.Utils;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCommon : BaseMonoBehaviour, IBulletSetup
{
    private Vector3 shootDir;
    private float moveSpeed = 100f;
    public float bulletDamage = 2;//子弹伤害

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

    public override void OnUpdate()
    {
        //子弹移动
        transform.position += shootDir * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.Log("进入了碰撞");
        if (collision.tag.Equals(Config.enemyTag))//触发
        {
            eventModule.EventTrigger<float>(EventType.bulletToEnemy, bulletDamage);
            Push();
            //StartCoroutine(Push(() => { Push(); }, 0));
        }
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
        poolModule.PushObj(gameObject.name, gameObject);
    }
}
