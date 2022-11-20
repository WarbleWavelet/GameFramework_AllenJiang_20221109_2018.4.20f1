/****************************************************
  文件：Demo_202211091551.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/16 20:7:11
  功能：让GameMain的MenuProcedure.OnEnter()中实例FsmTemp ,然后按1,2切换状态
        https://www.bilibili.com/video/BV16B4y1K7Sm/?vd_source=54db9dcba32c4988ccd3eddc7070f140
*****************************************************/

using GameFramework.Fsm;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class Test_Procedure8Fsm : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
    }
}



#region 两种自定义状态


public class IdleState : FsmState<FsmTemp>
{
    string m_stateName = "IdleState";

    protected override void OnEnter(IFsm<FsmTemp> fsm)
    {
        base.OnEnter(fsm);
        Log.Info(m_stateName+" OnEnter");
    }

    protected override void OnInit(IFsm<FsmTemp> fsm)
    {
        base.OnInit(fsm);
        Log.Info(m_stateName + " OnInit");
    }

    protected override void OnLeave(IFsm<FsmTemp> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        Log.Info(m_stateName + " OnLeave");
    }

    protected override void OnUpdate(IFsm<FsmTemp> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Log.Info(m_stateName + " OnUpdate");

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState<MoveState>(fsm);
        }
    }    
    
    protected override void OnDestroy(IFsm<FsmTemp> fsm)
    {
        base.OnDestroy(fsm);
        Log.Info(m_stateName + " OnDestroy");
    }
}


public class MoveState : FsmState<FsmTemp>
{
    string m_stateName = "MoveState";

    protected override void OnEnter(IFsm<FsmTemp> fsm)
    {
        base.OnEnter(fsm);
        Log.Info(m_stateName + " OnEnter");
    }

    protected override void OnInit(IFsm<FsmTemp> fsm)
    {
        base.OnInit(fsm);
        Log.Info(m_stateName + " OnInit");
    }

    protected override void OnLeave(IFsm<FsmTemp> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        Log.Info(m_stateName + " OnLeave");
    }

    protected override void OnUpdate(IFsm<FsmTemp> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Log.Info(m_stateName + " OnUpdate");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState<IdleState>(fsm);
        }
    }

    protected override void OnDestroy(IFsm<FsmTemp> fsm)
    {
        base.OnDestroy(fsm);
        Log.Info(m_stateName + " OnDestroy");
    }
}
#endregion


public class FsmTemp
{
    public IFsm<FsmTemp> m_Fsm = null;

    public FsmTemp()
    {
        FsmComponent fsmComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();
        m_Fsm = fsmComponent.CreateFsm("ActiorFsm",this, new IdleState(), new MoveState());

        m_Fsm.Start<IdleState>();
    
    }
}






