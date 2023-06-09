using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class FSMSystem
{
    
    public Dictionary<string, FSMState> stateDic;
    /// <summary>
    /// 当前的状态
    /// </summary>
    public string gameState;
    /// <summary>
    /// 当前的状态类
    /// </summary>
    private FSMState currentState;


    /// <summary>
    /// 获取当前状态
    /// </summary>
    public string GetCurState => gameState;

    /// <summary>
    /// 执行过渡条件满足时对应状态该做的事
    /// </summary>
    public void ChangeGameState(string gameState, object obj = null)
    {
        if (!stateDic.ContainsKey(gameState))
        {
            Debug.LogError("在状态机里面不存在状态" + gameState + ",无法进行状态转换");
            return;
        }
        currentState?.DoLeave(obj);
        currentState = stateDic[gameState];
        this.gameState = gameState;
        currentState?.DoEnter(obj);
    }

    /// <summary>
    /// 更新npc的动作
    /// </summary>
    public void DOUpdata()
    {
        currentState?.DOUpdata();
    }

    public void DOFixedUpdate()
    {
        currentState?.DOFixedUpdate();
    }

    /// <summary>
    /// 获取指定状态类
    /// </summary>
    public FSMState GetFSMState(string FSMState)
    {
        return stateDic.ContainsKey(FSMState) ? stateDic[FSMState] : null;
    }

    /// <summary>
    /// 确认当前状态
    /// </summary>
    /// <param name="state"></param>
    public bool CheckCurState(string state)
    {
        return gameState == state;
    }
}
