using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    //UI面板
    public const string Canvas = "Canvas";
    public const string Bottom = "Bottom";
    public const string Mid = "Mid";
    public const string Top = "Top";
    public const string System = "System";

    //光标
    public const string Arrow = "CursorIcons/Arrow";
    public const string Attack = "CursorIcons/Attack";
    public const string Grab = "CursorIcons/Grab";
    public const string Move = "CursorIcons/Move";
    public const string Unit = "CursorIcons/Unit";

    //场景
    public const string Init = "Init";
    public const string Game = "Game";
    public const string Loading = "Loading";

    //案件
    public const int Key_Mouse_Right = 1;//鼠标右键
    public const int Key_Mouse_Left = 0;//鼠标左键
    public static KeyCode Key_UP = KeyCode.W;
    public static KeyCode Key_Down = KeyCode.S;
    public static KeyCode Key_Left = KeyCode.A;
    public static KeyCode Key_Right = KeyCode.D;
    public static KeyCode key_T = KeyCode.T;
    public static KeyCode key_Y = KeyCode.Y;
    public static KeyCode key_U = KeyCode.U;
}
