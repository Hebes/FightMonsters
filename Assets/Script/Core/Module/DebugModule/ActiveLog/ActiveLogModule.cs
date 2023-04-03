using LogUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主动日志模块
/// </summary>
public class ActiveLogModule : BaseManager<ActiveLogModule>, IModule
{
    public void InitModule()
    {
        bool isLogPrint = PlayerPrefs.GetInt("设置日志开启") == 0;

        //主动日志模块
        DLog.InitSettings(new LogConfig()
        {
            enableSave = isLogPrint,
            eLoggerType = LoggerType.Unity,
#if !UNITY_EDITOR
            //savePath = $"{Application.persistentDataPath}/LogOut/ActiveLog/",
#endif
            savePath = $"{Application.dataPath}/LogOut/ActiveLog/",
            saveName = "Debug主动输出日志.txt",
        });
    }
}
