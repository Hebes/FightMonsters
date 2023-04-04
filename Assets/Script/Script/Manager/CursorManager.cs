using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECursorType
{
    /// <summary>
    /// Ĭ�ϼ�ͷ
    /// </summary>
    Arrow,
    /// <summary>
    /// ץȡ
    /// </summary>
    Grab,
    /// <summary>
    /// ѡ��
    /// </summary>
    Select,
    /// <summary>
    /// ����
    /// </summary>
    Attack,
    /// <summary>
    /// �ƶ�
    /// </summary>
    Move,
}


[Serializable]
public class CursorAnimation
{
    public ECursorType cursorType;
    public Texture2D[] textureArray;
    public float frameRate;
    public Vector2 offset;
}

/// <summary>
/// ������
/// </summary>
public class CursorManager : MonoBehaviour
{
    private List<CursorAnimation> cursorList { get; set; }
    private CursorAnimation cursorAnimation { get; set; }

    private int currentFrame { get; set; }
    private float frameTimer { get; set; }
    private int frameCount { get; set; }

    private void Awake()
    {
        cursorList = new List<CursorAnimation>() {
        new CursorAnimation{
            cursorType = ECursorType.Arrow,//Ĭ�����ͼ��
            textureArray = ModuleManager.Instance.resModule.LoadAll<Texture2D>(Config.Arrow),
            frameRate =.1f,
            offset = new Vector2(4,4)},//ͼƬ�������Ͻǵ���ͷ�Ĳ������ ԭ����10
        new CursorAnimation{
            cursorType = ECursorType.Grab,//ץȡ
            textureArray = ModuleManager.Instance.resModule.LoadAll<Texture2D>(Config.Grab),
            frameRate =.05f,
            offset = new Vector2(4,4)},//ͼƬ�������Ͻǵ���ͷ�Ĳ������ ԭ����16
        new CursorAnimation{
            cursorType = ECursorType.Select,//ѡ��
            textureArray = ModuleManager.Instance.resModule.LoadAll<Texture2D>(Config.Unit),
            frameRate =1f,
            offset = new Vector2(4,4)},//ͼƬ�������Ͻǵ���ͷ�Ĳ������
        new CursorAnimation{
            cursorType = ECursorType.Attack,//����
            textureArray = ModuleManager.Instance.resModule.LoadAll<Texture2D>(Config.Attack),
            frameRate =.1f,
            offset = new Vector2(16,16)},//ͼƬ�������Ͻǵ���ͷ�Ĳ������
        new CursorAnimation{
            cursorType = ECursorType.Move,//�ƶ�
            textureArray = ModuleManager.Instance.resModule.LoadAll<Texture2D>(Config.Move),
            frameRate =.2f,
            offset = new Vector2(4,4)},//ͼƬ�������Ͻǵ���ͷ�Ĳ������
        };

        SetActiveCursorType(ECursorType.Arrow);//��ʼĬ�����ü�ͷͼ��
    }

    private void Update()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += cursorAnimation.frameRate;//֡Ƶ
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);//��������ǩ
        }
    }

    [SerializeField] private ECursorType cursorType;
    public ECursorType SetCursorType { set => cursorType = value; }
    private void OnMouseEnter() => SetActiveCursorType(cursorType);
    private void OnMouseExit() => SetActiveCursorType(ECursorType.Arrow);

    /// <summary>
    /// ���û�ı�ǩ����
    /// </summary>
    /// <param name="cursorType"></param>
    public void SetActiveCursorType(ECursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }

    /// <summary>
    /// ��ȡ��ǩ�Ķ���
    /// </summary>
    /// <param name="cursorType">��ǩ����</param>
    /// <returns></returns>
    private CursorAnimation GetCursorAnimation(ECursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorList)
            if (cursorAnimation.cursorType == cursorType) return cursorAnimation;
        return null;
    }

    /// <summary>
    /// ���û�ı�ǩ����
    /// </summary>
    /// <param name="cursorAnimation"></param>
    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;//�����ĳ���
    }
}


