using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
1、AddComponentMenu 导航栏菜单
2、ContextMenu 右键菜单
3、HeaderAttribute
4、HideInInspector 可以让public变量在Inspector上隐藏，无法在Editor中进行编辑
5、MultilineAttribute 支持输入多行文本
6、RangeAttribute 限定输入值的范围
7、RequireComponent 组件依赖，使用该组件后自动添加依赖组件
8、RuntimeInitializeOnLoadMethodAttribute
9、SerializeField 强制对变量进行序列化，即使变量是private
10、SpaceAttribute 增加空位
11、TooltipAttribute 提示信息，当鼠标移到Inspector上时显示相应的提示
12、InitializeOnLoadAttribute
13、InitializeOnLoadMethodAttribute
14、MenuItem 导航栏的菜单项
 */

public class Init : MonoBehaviour
{
    private List<IModule> modiles;

    /// <summary>
    /// 确保第一个场景为MenuScene
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        if (SceneManager.GetActiveScene().name == Config.Init) return;
        SceneManager.LoadScene(Config.Init);
    }

    private void Awake()
    {
        //加载模块
        modiles = new List<IModule>()
        {
            ActiveLogModule.Instance,//日志
            UIModule.Instance,//UI
            ResModule.Instance,//加载
            PoolModule.Instance,//对象池
            EventModule.Instance,//事件
            AudioModule.Instance,//声音
            FPSModule.Instance,//FPS
            MonoModule.Instance,//协程
            ScenesModule.Instance,//场景
            PrefabModule.Instance,//物体加载

        };
        modiles.ForEach(mod => { mod.InitModule(); });
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(Config.Game);
    }
}
