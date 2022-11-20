/****************************************************
  文件：Test_Resources.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/16 18:43:1
  功能：
        01 资源加载模式（StreamingAsset（没示例），下载（有下载进度条那种，E大的阿里云服务器），还是Editor Resource（主要））
        02 加载资源（示例了图片）
        03 加载卸载场景
        04 音效（没进行代码示例）
        99 bug
        不管是Builtin的BaseComponent还是原型Assets/Plugins/UnityGameFramework/GameFramework.prefab，都缺了StartForcr.LitJsonHelper的选项
        {
            简单说就是BaseComponent识别不到Assets/GameMain/Scripts/Utility/LitJsonHelper.cs
            Entrance procedure is invalid.
            UnityEngine.Debug:LogError(Object)
            UnityGameFramework.Runtime.DefaultLogHelper:Log(GameFrameworkLogLevel, Object) (at Assets/Plugins/UnityGameFramework/Scripts/Runtime/Utility/DefaultLogHelper.cs:40)
            GameFramework.GameFrameworkLog:Error(String) (at Assets/Plugins/GameFramework/GameFramework/Base/Log/GameFrameworkLog.cs:1623)
            UnityGameFramework.Runtime.Log:Error(String) (at Assets/Plugins/UnityGameFramework/Scripts/Runtime/Utility/Log.cs:1619)
            UnityGameFramework.Runtime.<Start>d__9:MoveNext() (at Assets/Plugins/UnityGameFramework/Scripts/Runtime/Procedure/ProcedureComponent.cs:103)
            UnityEngine.SetupCoroutine:InvokeMoveNext(IEnumerator, IntPtr) (at E:/unity/Runtime/Export/Coroutines.cs:17)

            需要设置为StartForce的组件
            Builtin，JSONHelper
            Localization
            UI，UIGroupHelper

            最终，对比源工程，是程序集的问题（不要程序集）
        }

*****************************************************/

using GameFramework;
using GameFramework.Resource;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class Test_Resources : MonoBehaviour
{

    public Image img;//在场景中类似Game Framework/Builtin/UI/UI Form Instances/Image


    void Start()
    {  
        //Test_Start01();
    }


    void Update()
    {
        //Test_Update01();
        Test_Update02();
    }


    #region 辅助


    /// <summary>单机读取StreamingAssets</summary> 
    void Test_Start01()
    {
        //BaseComponent b;
        //b.EditorResourceMode = false;

        //ResourceComponent r;
        //r.ResourceMode = ResourceMode.Package; // StreamingAssets没有内容会报错
        //r.ReadWritePathType = ReadWritePathType.Unspecified;
    }

    /// <summary>
    /// 出现下载进度条页面的操作
    /// PersistentDataPath下删除所有.dat
    /// </summary>
    void Test_Start02()
    {          
        //在Untiy编辑器操作的，动态代码会报错
        //BaseComponent b;
        //b.EditorResourceMode = false;

        //ResourceComponent r;
        // r.ResourceMode = ResourceMode.Updatable;
        //r.ReadWritePathType = ReadWritePathType.Unspecified;
        //r.UpdatePrefixUri = "https://starforce.gameframework.cn/Resources/0_1_0_1/Windows";//默认
    }



    /// <summary>
    /// 测试加载卸载场景
    /// 01 在数据表的文件中新增一行场景的信息
    /// 02 在Assets/GameMain/Scenes下新建场景，并添加到playerSettings
    /// 03 将这函数放到update中，脚本放到场景中
    /// </summary>
    void Test_Update01()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StarForce.GameEntry.Scene.AddLoadScene("Test_Scene");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StarForce.GameEntry.Scene.UnLoadScene("Test_Scene");
        }

    }

    /// <summary>测试加载图片</summary>
    void Test_Update02()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSprite("捕获", img);
        }
    }


     void SetSprite(string name,UnityEngine.UI.Image img)
    {
        StarForce.GameEntry.Resource.LoadAsset
        (
            GetSpriteAsset(name), 
            new LoadAssetCallbacks
            (
                (assetName, asset, duration, userData) => 
                {
                    //Texture2D texture = asset as Texture2D;
                    Texture2D texture = (Texture2D)asset;
                    Sprite sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), Vector2.zero);
                    img.sprite = sprite;
                },
                (assetName, status, errorMessage, userData) =>
                {
                    Debug.LogErrorFormat("加载失败，资源：{0}，错误信息：{1}",assetName,errorMessage);        
                }
            )
        );
    }

    /// <summary>
    /// 必须是.PNG，.png我这边报错了
    /// </summary> 
     static string GetSpriteAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Scripts/00Test/202211161842_资源/{0}.PNG",assetName);
        // return Utility.Text.Format("Assets/GameMain/UI/UISprites/Common/{0}.PNG",assetName); //V诺视频主放的位置
    }
    #endregion

}


#region 拓展类 Test_SceneExtension


public static class Test_SceneExtension
{
    public static void AddLoadScene(  this UnityGameFramework.Runtime.SceneComponent sceneComponent,  int sceneId, object userData = null)
    {
        GameFramework.DataTable.IDataTable<DRScene> scenes = StarForce.GameEntry.DataTable.GetDataTable<DRScene>();
        DRScene scene = scenes.GetDataRow(sceneId) ;

        if (scene == null)
        {
            Debug.LogErrorFormat("不存在ID={0}的Scene",sceneId );
            return ;
        }
        sceneComponent.LoadScene(AssetUtility.GetSceneAsset(scene.AssetName), Constant.AssetPriority.SceneAsset, userData);
    }

    public static void AddLoadScene(this UnityGameFramework.Runtime.SceneComponent sceneComponent, string sceneName, object userData = null)
    {
        GameFramework.DataTable.IDataTable<DRScene> scenes = StarForce.GameEntry.DataTable.GetDataTable<DRScene>();
        DRScene scene = scenes.GetDataRow( (sceneData) =>{ return sceneData.AssetName == sceneName ? true : false; });

        if (scene == null)
        {
            Debug.LogErrorFormat("不存在Name={0}的Scene", sceneName);
            return;
        }
        sceneComponent.LoadScene(AssetUtility.GetSceneAsset(scene.AssetName), Constant.AssetPriority.SceneAsset, userData);
    }

    public static void UnLoadScene(this UnityGameFramework.Runtime.SceneComponent sceneComponent, int sceneId, object userData = null)
    {
        GameFramework.DataTable.IDataTable<DRScene> scenes = StarForce.GameEntry.DataTable.GetDataTable<DRScene>();
        DRScene scene = scenes.GetDataRow(sceneId);

        if (scene == null)
        {
            Debug.LogErrorFormat("不存在ID={0}的Scene", sceneId);
            return;
        }
        sceneComponent.UnloadScene(AssetUtility.GetSceneAsset(scene.AssetName), userData);
    }

    public static void UnLoadScene(this UnityGameFramework.Runtime.SceneComponent sceneComponent, string sceneName, object userData = null)
    {
        GameFramework.DataTable.IDataTable<DRScene> scenes = StarForce.GameEntry.DataTable.GetDataTable<DRScene>();
        DRScene scene = scenes.GetDataRow((sceneData) => { return sceneData.AssetName == sceneName ? true : false; });

        if (scene == null)
        {
            Debug.LogErrorFormat("不存在Name={0}的Scene", sceneName);
            return;
        }
        sceneComponent.UnloadScene(AssetUtility.GetSceneAsset(scene.AssetName), userData);
    }
}

#endregion

