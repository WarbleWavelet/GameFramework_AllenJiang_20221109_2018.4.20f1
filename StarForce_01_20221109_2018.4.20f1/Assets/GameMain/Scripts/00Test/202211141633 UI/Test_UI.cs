/****************************************************
  文件：ReadMe.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/14 10:5:40
  功能：
*****************************************************/

using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ReadMe
{
public class ReadMe
{

}

    class UI
    {
        object DataTable = "Assets/GameMain/DataTables/UIForm.txt";
        object Enum = "Assets/GameMain/Scripts/UI/UIFormId.cs"; //连接AssetName
        object UGUIForm = "Assets/GameMain/Scripts/UI/UGuiForm.cs"; 

        void Open()
        {
          //  GameEntry.UI.OpenUIForm();
        }

        interface IUIForm { }
        class UIForm:IUIForm { }
        abstract class UIFormLogic {
            public abstract void OnInited() ;
            public abstract void OnOpen() ;
        }
        class UIExtension { }
        class UGuiForm : UIFormLogic
        {
            public override void OnInited()
            {
                throw new System.NotImplementedException();
            }

            public override void OnOpen()
            {
                throw new System.NotImplementedException();
            }
        }
        //  class UGuiForm : UIFormLogic { }
    }
}

