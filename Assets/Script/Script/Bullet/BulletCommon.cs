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
    public float bulletDamage = 2;//�ӵ��˺�

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="shootDir"></param>
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        GetComponent<Rigidbody2D>().AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);//������� �� Bullet_Common �ƶ�������
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));//�ӵ����ɵĳ���
        //û����ײ�������������ɵ��ӵ�������
        StartCoroutine(Push(() => { Push(); }, 2));
    }

    public override void OnUpdate()
    {
        //�ӵ��ƶ�
        transform.position += shootDir * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.Log("��������ײ");
        if (collision.tag.Equals(Config.enemyTag))//����
        {
            eventModule.EventTrigger<float>(EventType.bulletToEnemy, bulletDamage);
            Push();
            //StartCoroutine(Push(() => { Push(); }, 0));
        }
    }

    /// <summary>
    /// �����ӵ��������
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
