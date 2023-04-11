using CodeMonkey.Utils;
using Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// 邪眼
/// </summary>
public class EnemyFlight : EnemyBase, IDamage
{
    public FSMSystem fSMSystem;
    public ModuleManager moduleManager;

    private void OnEnable()
    {
        ModuleManager.Instance.monoModule.AddUpdate(fSMSystem.DOUpdata);
        ModuleManager.Instance.monoModule.AddFixedUpdate(fSMSystem.DOFixedUpdate);
        ModuleManager.Instance.eventModule.AddEventListener<float>(EventType.bulletToEnemy, TakeDamage);
    }
    private void OnDisable()
    {
        ModuleManager.Instance.monoModule.RemoveUpdate(fSMSystem.DOUpdata);
        ModuleManager.Instance.monoModule.RemoveFixedUpdate(fSMSystem.DOFixedUpdate);
        ModuleManager.Instance.eventModule.RemoveEventListener<float>(EventType.bulletToEnemy, TakeDamage);
    }

    private void Awake()
    {
        moduleManager = ModuleManager.Instance;
        fSMSystem = new FSMSystem();
        fSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            { EnemyFlightFSM.Idle.ToString(),new EnemyFlightIdleState(fSMSystem,this)},
            { EnemyFlightFSM.FlyMove.ToString(),new EnemyFlightFlyMoveState(fSMSystem,this)},
            { EnemyFlightFSM.Death.ToString(),new EnemyFlightDeathState(fSMSystem,this)},
            { EnemyFlightFSM.Attack.ToString(),new EnemyFlightAttackState(fSMSystem,this)},
            { EnemyFlightFSM.TakeDamage.ToString(),new EnemyFlightTakeDamageState(fSMSystem,this)},
        };
        fSMSystem.ChangeGameState(EnemyFlightFSM.Idle.ToString(), this);//进入移动状态
    }

    public void Kill()
    {
        moduleManager.poolModule.PushObj(
            gameObject.name,
        gameObject);
    }

    public void TakeDamage(float damage)
    {
        //进入受伤状态
        fSMSystem.ChangeGameState(EnemyFlightFSM.TakeDamage.ToString());
        //2被伤害
        float damage2 = damage * 2;
        // Hit enemy 敌人伤害
        float damageAmount = UnityEngine.Random.Range(damage, damage2);//随机伤害
        bool isCritical = UnityEngine.Random.Range(0, 100) < 30;//是否重击
        if (isCritical) damageAmount *= 2;//重击伤害*2
        hp -= damageAmount;
        if (hp <= 0) Kill();
        //显示伤害文字效果
        //Component_Helper.Show_pf_Damage(collision.transform.position, damageAmount, isCritical);
        //加载特效
        //Component_Helper.LoadEffect(Config_ResLoadPaths.Gun_pf_Effect, collision.transform.position);
    }
}

/// <summary>
/// 怪物状态
/// </summary>
public enum EnemyFlightFSM
{
    Idle,
    FlyMove,
    Death,
    Attack,
    TakeDamage,//收到伤害
}

/// <summary>
/// 等待状态
/// </summary>
public class EnemyFlightIdleState : FSMState
{
    public EnemyFlightIdleState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
    }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        fSMSystem.ChangeGameState(EnemyFlightFSM.FlyMove.ToString());
    }
}

/// <summary>
/// 收到伤害
/// </summary>
public class EnemyFlightTakeDamageState : FSMState
{
    public EnemyFlightTakeDamageState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
        this.enemyFlight = obj as EnemyFlight;
    }

    public EnemyFlight enemyFlight { get; private set; }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        //怪物图标变白等
        enemyFlight.fSMSystem.ChangeGameState(EnemyFlightFSM.FlyMove.ToString(), enemyFlight);
    }
}

/// <summary>
/// 移动
/// </summary>
public class EnemyFlightFlyMoveState : FSMState
{

    public EnemyFlightFlyMoveState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
        this.enemyFlight = obj as EnemyFlight;
    }

    public EnemyFlight enemyFlight { get; private set; }
    public GameObject player { get; private set; }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void DOUpdata()
    {
        base.DOUpdata();
        //float distance = Vector3.Distance(enemyFlight.transform.position, player.transform.position);
        //Debug.Log("开始攻击距离"+ distance);
        //if (distance <= 0.2)
        //{
        //    Debug.Log("开始攻击");
        //    enemyFlight.fSMSystem.ChangeGameState(EnemyFlightFSM.Attack.ToString());
        //}
    }

    public override void DOFixedUpdate()
    {
        base.DOFixedUpdate();

        EnemyLookPlayer();
        EnemyMovePlayer();
    }

    /// <summary>
    /// 怪物向玩家移动
    /// </summary>
    public void EnemyMovePlayer()
    {
        //Move towards target
        enemyFlight.transform.position = Vector2.MoveTowards(
            enemyFlight.transform.position,
            player.transform.position,
            enemyFlight.moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// 怪物朝向玩家
    /// </summary>
    private void EnemyLookPlayer()
    {
        //偏移量
        UnityEngine.Vector3 aimDir = (player.transform.position - enemyFlight.transform.position).normalized;
        //返回Tan为y/x的角度(以弧度为单位)。弧度到角度转换常数(只读)。
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        //怪物旋转
        float y = angle > 90 || angle < -90 ? 180f : 0f;
        enemyFlight.transform.rotation = Quaternion.Euler(0, y, 0);
    }
}

/// <summary>
/// 死亡
/// </summary>
public class EnemyFlightDeathState : FSMState
{
    public EnemyFlightDeathState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
    }
}

/// <summary>
/// 攻击
/// </summary>
public class EnemyFlightAttackState : FSMState
{
    public EnemyFlightAttackState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
        this.enemyFlight = obj as EnemyFlight;
    }
    public EnemyFlight enemyFlight { get; private set; }

    private GameObject PlayerGo { get; set; }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        Debug.Log("进入攻击状态");
        enemyFlight.moduleManager.eventModule.EventTrigger<GameObject>(EventType.playerTakeDamage, PlayerGo);
    }
}
