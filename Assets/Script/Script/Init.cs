using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Init : MonoBehaviour
{
    private List<IModule> modiles;

    private void Awake()
    {
        //����ģ��
        modiles = new List<IModule>()
        {
            ActiveLogModule.I,//��־
            UIModule.I,//UI
            EventModule.I,//�¼�
            AudioModule.I,//����
            FPSModule.I,//FPS
            MonoModule.I,//Э��
            PoolModule.I,//�����
            PrefabModule.I,//�������
            ResModule.I,//����
            ScenesModule.I,//����
        };
        modiles.ForEach(mod => { mod.InitModule(); });
        DontDestroyOnLoad(gameObject);
    }
}
