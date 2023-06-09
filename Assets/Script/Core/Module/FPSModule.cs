using LogUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 显示FPS模块
/// https://blog.csdn.net/f_957995490/article/details/104470689
/// </summary>
public class FPSModule : SingletonAutoMono<FPSModule>, IModule
{

    float _updateInterval = 1f;//设定更新帧率的时间间隔为1秒  
    float _accum = .0f;//累积时间  
    int _frames = 0;//在_updateInterval时间内运行了多少帧  
    float _timeLeft;
    string fpsFormat;


    public void InitModule()
    {
        _timeLeft = _updateInterval;
    }

    void OnGUI()
    {
        //定义一个GUIStyle的对象  
        GUIStyle labelFont = new GUIStyle();
        //设置文本颜色 
        labelFont.normal.textColor = new Color(0, 0, 0);
        //设置字体大小
        labelFont.fontSize = 40;
        GUI.Label(new Rect(100, 100, 200, 200), fpsFormat, labelFont);
    }

    void Update()
    {
        _timeLeft -= Time.deltaTime;
        //Time.timeScale可以控制Update 和LateUpdate 的执行速度,  
        //Time.deltaTime是以秒计算，完成最后一帧的时间  
        //相除即可得到相应的一帧所用的时间  
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;//帧数  

        if (_timeLeft <= 0)
        {
            float fps = _accum / _frames;
            //Debug.Log(_accum + "__" + _frames);  
            fpsFormat = System.String.Format("{0:F2}FPS", fps);//保留两位小数  
            //Debug.LogError(fpsFormat);

            _timeLeft = _updateInterval;
            _accum = .0f;
            _frames = 0;
        }
    }
}
