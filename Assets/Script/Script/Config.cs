using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventType
{
    public const string playerShoot = "playerShoot";
}

public class Config
{
    //UI���
    public const string Canvas = "Canvas";
    public const string Bottom = "Bottom";
    public const string Mid = "Mid";
    public const string Top = "Top";
    public const string System = "System";

    //���
    public const string Arrow = "CursorIcons/Arrow";
    public const string Attack = "CursorIcons/Attack";
    public const string Grab = "CursorIcons/Grab";
    public const string Move = "CursorIcons/Move";
    public const string Unit = "CursorIcons/Unit";

    //����
    public const string Init = "Init";
    public const string Game = "Game";
    public const string Loading = "Loading";

    //����
    public const string Bullet = "Bullet/Prefab/Bullet";
    public const string BulletPhysics = "Bullet/Prefab/BulletPhysics";

    //���
    public const string Player = "Player/Player";

    //Tag
    public const string playerTag = "Player";

    //����
    public const string EnemyFlight = "Enemy/EnemyFlight";//а��

    //����
    public const int Key_Mouse_Right = 1;//����Ҽ�
    public const int Key_Mouse_Left = 0;//������
    public static KeyCode Key_UP = KeyCode.W;
    public static KeyCode Key_Down = KeyCode.S;
    public static KeyCode Key_Left = KeyCode.A;
    public static KeyCode Key_Right = KeyCode.D;
    public static KeyCode key_T = KeyCode.T;
    public static KeyCode key_Y = KeyCode.Y;
    public static KeyCode key_U = KeyCode.U;
}
