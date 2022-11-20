/****************************************************
  文件：Demo_202211131526.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/13 15:26:33
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>系统的事件不满足需求，想要达到，注册多个同类时间，注销时全部注销</summary>
public class Test_EventNotPerfect : MonoBehaviour
{

    public event Action eventPool;


    void Start()
    {
        eventPool += ActionTest_EventPool1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            eventPool?.Invoke();
        }
    }


    void ActionTest_EventPool1()
    {
        Debug.Log("****01****");
        eventPool += ActionTest_EventPool2;
        eventPool += ActionTest_EventPool2;
        eventPool -= ActionTest_EventPool1;
    }

    void ActionTest_EventPool2()
    {
        Debug.Log("****02****");
        eventPool += ActionTest_EventPool1;
        eventPool -= ActionTest_EventPool2;
        eventPool -= ActionTest_EventPool2;
    }
}
