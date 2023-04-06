using Core;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 邪眼
/// </summary>
public class EnemyFlight : EnemyBase
{
    public FSMSystem fSMSystem;
    private void Awake()
    {
        fSMSystem = new FSMSystem();
        fSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            { EnemyFlightFSM.Idle.ToString(),new EnemyFlightIdleState(fSMSystem,this)},
            { EnemyFlightFSM.FlyMove.ToString(),new EnemyFlightFlyMoveState(fSMSystem,this)},
            { EnemyFlightFSM.Death.ToString(),new EnemyFlightDeathState(fSMSystem,this)},
            { EnemyFlightFSM.Attack.ToString(),new EnemyFlightAttackState(fSMSystem,this)},
        };
        ModuleManager.Instance.monoModule.AddUpdate(fSMSystem.DOUpdata);
        ModuleManager.Instance.monoModule.AddFixedUpdate(fSMSystem.DOFixedUpdate);
        fSMSystem.ChangeGameState(EnemyFlightFSM.Idle.ToString(), this);//进入移动状态
    }
}

public enum EnemyFlightFSM
{
    Idle,
    FlyMove,
    Death,
    Attack,
}

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

    public override void DOFixedUpdate()
    {
        base.DOFixedUpdate();

        //lock at target Player
        //enemyFlight.transform.rotation = Quaternion.Slerp(
        //enemyFlight.transform.rotation,
        //     Quaternion.LookRotation(player.transform.position - enemyFlight.transform.position),
        //     1 * Time.fixedDeltaTime
        //);

        //Move towards target
        enemyFlight.transform.position += enemyFlight.transform.forward * enemyFlight.moveSpeed * Time.fixedDeltaTime;


        //enemyFlight.transform.Translate(Vector3.right * Time.fixedDeltaTime * enemyFlight.moveSpeed, player.transform);
        //float distance = Vector3.Distance(enemyFlight.transform.position, player.transform.position);
        //this.Log("距离是:" + distance);
    }
}

public class EnemyFlightDeathState : FSMState
{
    public EnemyFlightDeathState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
    }
}

public class EnemyFlightAttackState : FSMState
{
    public EnemyFlightAttackState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
    }
}
