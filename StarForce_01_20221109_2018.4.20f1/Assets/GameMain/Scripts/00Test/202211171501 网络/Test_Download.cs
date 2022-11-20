/****************************************************
  文件：Test_Network.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/17 15:16:10
  功能：01 下载
            自己配置一个服务器，我这边用的是 Apache + start WampServer64

            bug OnDownloadUpdate(object sender, GameEventArgs e)，e报了空指针【注册没写对应】 
*****************************************************/

using GameFramework.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class Test_Download : MonoBehaviour
{
    void Start()
    {
        GameEntry.Event.Subscribe( DownloadStartEventArgs.EventId, OnDownloadStart);
        GameEntry.Event.Subscribe( DownloadUpdateEventArgs.EventId, OnDownloadUpdate);
        GameEntry.Event.Subscribe(DownloadSuccessEventArgs.EventId, OnDownloadSuccess);
        GameEntry.Event.Subscribe(DownloadFailureEventArgs.EventId, OnDownloadFailure);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {        
            string downloadPath = Application.streamingAssetsPath + "/Android.zip";
           
            //string downloadUri = "http://127.0.0.1:8880/ServerAssetBundle/Android.zip";  //作者配置的
           string downloadUri = "http://127.0.0.1:8081/Test_202211171559_GameFramework_Download/Android.zip";  //我配置的
           //string downloadUri = "file:///D:/ProgramFilesTrim/Apache/Apache24/htdocs/Test_202211171559_GameFramework_Download/Android.zip";  //我配置的
           GameEntry.Download.AddDownload(downloadPath, downloadUri);
     
        }
    }
    private void OnDownloadStart(object sender, GameEventArgs e)
    {          
        Debug.LogFormat("下载开始");
    }
    private void OnDownloadUpdate(object sender, GameEventArgs e)
    {
        UnityGameFramework.Runtime.DownloadUpdateEventArgs eventArgs = e as UnityGameFramework.Runtime.DownloadUpdateEventArgs;
        Debug.LogFormat("下载中：{0}", eventArgs.CurrentLength);
    }

    private void OnDownloadFailure(object sender, GameEventArgs e)
    {
        DownloadFailureEventArgs eventArgs = e as DownloadFailureEventArgs;
        Debug.LogFormat("下载失败：{0}", eventArgs.ErrorMessage);
    }

    private void OnDownloadSuccess(object sender, GameEventArgs e)
    {
        DownloadSuccessEventArgs eventArgs = e as DownloadSuccessEventArgs;
        Debug.LogFormat("下载完成：{0}", eventArgs.CurrentLength);
        Common.Refresh();
   
    }
}
