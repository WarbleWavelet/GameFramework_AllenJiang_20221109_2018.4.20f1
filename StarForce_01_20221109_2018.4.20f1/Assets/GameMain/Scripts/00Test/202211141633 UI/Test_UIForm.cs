/****************************************************
  文件：NewBehaviourScript.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/14 16:34:23
  功能：01 通过名字OpenUIForm
        在Assets/GameMain/Scripts/UI/UIExtension.cs添加public static int? OpenUIForm(this UIComponent uiComponent, string uiFormName, object userData = null)
        到Assets/GameMain/Scripts/UI/MenuForm.cs，改
            public void OnAboutButtonClick()
            {
                //GameEntry.UI.OpenUIForm(UIFormId.AboutForm);
                GameEntry.UI.OpenUIForm("AboutForm");
            }

`       02 是否在最上层
        Assets/GameMain/DataTables/UIForm.txt 改AboutForm的覆盖改为FALSE

        03 当UI的Canvas设置RenderMode.ScreenSpaceCamera;时要做的修改；[场景这样写是因为VS有提示，快]
        Assets/GameMain/Scripts/UI/UGuiForm.cs，OnInit()
            // transform.anchoredPosition = Vector2.zero;
            transform.anchoredPosition3D = Vector2.zero;//新增
        Assets/Plugins/UnityGameFramework/Scripts/Runtime/UI/UIComponent.cs ，AddUIGroup
            transform.localPosition = Vector3.one;//新增
            transform.rotation = Quaternion.identity; //新增
        [场景]Game Framework/Builtin/UI/UI Form Instances
            Canvas a;
            a.renderMode = RenderMode.ScreenSpaceCamera;
        [场景]Game Framework/Camera   
            Camera a;
            a.clearFlags = CameraClearFlags.Depth;
            a.cullingMask = UI;
            a.depth = 100;
            
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UIForm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
