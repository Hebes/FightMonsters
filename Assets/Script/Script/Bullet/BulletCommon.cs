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

    private void OnUpdate()
    {
        //�ӵ��ƶ�
        transform.position += shootDir * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.Log("��������ײ");
        //ICommonCollide target = collision.GetComponent<ICommonCollide>();
        //if (target != null)
        //{
        //    // Hit enemy �����˺�
        //    int damageAmount = UnityEngine.Random.Range(100, 200);//����˺�
        //    bool isCritical = UnityEngine.Random.Range(0, 100) < 30;//�Ƿ��ػ�
        //    if (isCritical) damageAmount *= 2;//�ػ��˺�*2

        //    target.Damage(damageAmount);
        //    StartCoroutine(Push(() => { Push(); }, 0));

        //    //��ʾ�˺�����Ч��
        //    //Component_Helper.Show_pf_Damage(collision.transform.position, damageAmount, isCritical);
        //    //������Ч
        //    //Component_Helper.LoadEffect(Config_ResLoadPaths.Gun_pf_Effect, collision.transform.position);
        //}
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
        ModuleManager.Instance.poolModule.PushObj(gameObject.name, gameObject);
    }
}
