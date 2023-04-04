using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
1��AddComponentMenu �������˵�
2��ContextMenu �Ҽ��˵�
3��HeaderAttribute
4��HideInInspector ������public������Inspector�����أ��޷���Editor�н��б༭
5��MultilineAttribute ֧����������ı�
6��RangeAttribute �޶�����ֵ�ķ�Χ
7��RequireComponent ���������ʹ�ø�������Զ�����������
8��RuntimeInitializeOnLoadMethodAttribute
9��SerializeField ǿ�ƶԱ����������л�����ʹ������private
10��SpaceAttribute ���ӿ�λ
11��TooltipAttribute ��ʾ��Ϣ��������Ƶ�Inspector��ʱ��ʾ��Ӧ����ʾ
12��InitializeOnLoadAttribute
13��InitializeOnLoadMethodAttribute
14��MenuItem �������Ĳ˵���
 */

public class Init : MonoBehaviour
{
    private List<IModule> modiles;

    /// <summary>
    /// ȷ����һ������ΪMenuScene
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        if (SceneManager.GetActiveScene().name == Config.Init) return;
        SceneManager.LoadScene(Config.Init);
    }

    private void Awake()
    {
        //����ģ��
        modiles = new List<IModule>()
        {
            ActiveLogModule.Instance,//��־
            UIModule.Instance,//UI
            ResModule.Instance,//����
            PoolModule.Instance,//�����
            EventModule.Instance,//�¼�
            AudioModule.Instance,//����
            FPSModule.Instance,//FPS
            MonoModule.Instance,//Э��
            ScenesModule.Instance,//����
            PrefabModule.Instance,//�������

        };
        modiles.ForEach(mod => { mod.InitModule(); });
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(Config.Game);
    }
}
