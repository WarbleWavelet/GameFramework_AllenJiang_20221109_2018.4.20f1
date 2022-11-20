/****************************************************
  文件：Demo_202211100041.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/10 0:46:13
  功能：
        bug GameFrameworkException: Event '1903766080' not allow duplicate handler.
*****************************************************/

using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// 写在这程序集会冲突，因为需要用到GameMain中的类
    /// 挂在StartForce上
    /// </summary>
public class Test_Event :MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameEntry.Event.Subscribe(TestEventArgs.EventId, OnTestFire);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameEntry.Event.Fire(this, TestEventArgs.Create(1111));
            GameEntry.Event.FireNow(this, TestEventArgs.Create(2222));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameEntry.Event.Unsubscribe(TestEventArgs.EventId, OnTestFire);
        }
    }

    void OnTestFire(object sender, GameEventArgs e)
    {
        TestEventArgs eventArgs= e as TestEventArgs;
        Debug.LogFormat("测试事件：{0}",(eventArgs.TestInt).ToString());        
    }
}

public sealed class TestEventArgs : GameEventArgs
{
    /// <summary>加载全局配置成功事件的编号</summary> 
    public static readonly int EventId = typeof(TestEventArgs).GetHashCode();

    public override int Id
    {
        get
        {
            return EventId;
        }
    }

    /// <summary>事件自带的参数</summary>
    public int TestInt
    {
        get;
        private set;
    }

    public override void Clear()
    {
        TestInt = default;
           
    }


    /// <summary>
    /// 创建加载全局配置成功事件。
    /// </summary>
    /// <param name="e">内部事件。</param>
    /// <returns>创建的加载全局配置成功事件。</returns>
    public static TestEventArgs Create(int id)
    {
        TestEventArgs tmp = ReferencePool.Acquire<TestEventArgs>();
        tmp.TestInt = id;
        return tmp;
    }
}


