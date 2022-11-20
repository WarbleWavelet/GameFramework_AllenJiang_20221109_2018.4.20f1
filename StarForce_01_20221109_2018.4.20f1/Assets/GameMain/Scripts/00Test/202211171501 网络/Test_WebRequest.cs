/****************************************************
  文件：Test_WebRequest.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/18 2:21:54
  功能：
*****************************************************/


using UnityEngine.UI;
using GameFramework.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;
using UnityEngine.Networking;

public class Test_WebRequest : MonoBehaviour
{
    public Image image = null;
    public string URLA = "";
    public string URLB = "";
    // Start is called before the first frame update
    void Start()
    {

        GameEntry.Event.Subscribe(WebRequestStartEventArgs.EventId, OnWebRequestStart);
        GameEntry.Event.Subscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
        GameEntry.Event.Subscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (URLA == "")
            {
                return;
            }

            Debug.Log("下载方式 WebRequest");
            StartCoroutine(DownloadTexture(URLA));

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (URLB == "")
            {
                return;
            }
            Debug.Log("下载方式 GF WebRequest");
            // StartCoroutine(DownloadTexture(URLB));
            GameEntry.WebRequest.AddWebRequest(URLB);
        }
    }


    private void OnWebRequestStart(object sender, GameEventArgs e)
    {
        WebRequestStartEventArgs eventArgs = e as WebRequestStartEventArgs;
        Common.Log_ClassFunction();
    }
    private void OnWebRequestSuccess(object sender, GameEventArgs e)
    {
        WebRequestSuccessEventArgs eventArgs = e as WebRequestSuccessEventArgs;
        Common.Log_ClassFunction();

        Texture2D texture2D = new Texture2D(100,100);
        texture2D.LoadImage( eventArgs.GetWebResponseBytes());
        Sprite sprite = Sprite.Create(texture2D,new Rect( Vector2.zero, new Vector2(texture2D.width,texture2D.height)), Vector2.zero);
        image.sprite = sprite;
    }

    private void OnWebRequestFailure(object sender, GameEventArgs e)
    {
        
        WebRequestFailureEventArgs eventArgs = e as WebRequestFailureEventArgs;

       Common.Log_ClassFunction(); 
        Debug.Log(eventArgs.ErrorMessage);
    }


    IEnumerator DownloadTexture(string url)
    {
        //UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url, nonReadable:false);
        UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        DownloadHandlerTexture downloadHandler = new DownloadHandlerTexture(readable: true);
        unityWebRequest.downloadHandler = downloadHandler;

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            yield return null;
        }
        else
        {
            Texture2D texture2D = downloadHandler.texture;
            Sprite sprite = Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2(texture2D.width, texture2D.height)), Vector2.zero);
            image.sprite = sprite;
        }

    }


}
