using System;
using System.IO;
using UnityEngine;
using LogUtils;

/// <summary>
/// 被动日志模块
/// </summary>
public class PassiveLog : SingletonAutoMono<PassiveLog>,IModule
{
    private string path { get; set; }


    public void InitModule()
    {
        if (PlayerPrefs.GetInt("设置日志开启") == 0)
        {
            //path = $"{Application.dataPath}/LogOut/PassiveLog/";
            path = "Assets/LogOut/PassiveLog";
            DLog.Log($"被动日志输出路径：{path}");
            Application.logMessageReceived += Handler;
        }

        // 被动日志模块
        PassiveLog passiveLog = Instance; 
        passiveLog.name = "被动日志模块";
    }
    private void OnDestroy()
    {
        Application.logMessageReceived -= Handler;
    }

    /// <summary>
    /// 消息输出
    /// </summary>
    /// <param name="logString"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    private void Handler(string logString, string stackTrace, LogType type)
    {

        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {
            //UnityEngine.Debug.Log("显示堆栈调用：" + new System.Diagnostics.StackTrace().ToString());
            //UnityEngine.Debug.Log("接收到异常信息" + logString);
            string logPath = Path.Combine(path, $"Passive_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.log");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (Directory.Exists(path))
            {
                File.AppendAllText(logPath, "[时间]:" + DateTime.Now.ToString() + "\r\n");
                File.AppendAllText(logPath, "[类型]:" + type.ToString() + "\r\n");
                File.AppendAllText(logPath, "[报错信息]:" + logString + "\r\n");
                File.AppendAllText(logPath, "[堆栈跟踪]:" + stackTrace + "\r\n");
            }
        }
    }

    
}
