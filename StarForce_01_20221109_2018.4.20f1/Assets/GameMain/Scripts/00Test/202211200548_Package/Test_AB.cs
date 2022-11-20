/****************************************************
  文件：Test_AB.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/20 5:49:4
  功能：【01 走StreamingAssets下载包】
        01  新建AssetBundle文件夹（Assets外）
            打包，用选项卡GameFramework下的ResourceBuilder（只勾选Window64）打包，已经默认添加包了一遍，所以下面可以直接打
            AssetBundle将生成4个文件夹 BuildReport，Full,Package,Packed,Working
        02  将类似AssetBundle\Package\0_1_0_14\Windows64下的所有.dat，复制到StreamingAssets（Assets内）下【我有看到下架了的《大多数》，资源就放这里】
            取消勾选EditorResource
            就可以运行
        bug   每次打包，都删掉了StreamingAssets
        注意  初始都删掉了StreamingAssets有文件.gitkeep（可以新建txt改名）   

        【01 走HFS下载包】
        01  下载https://sourceforge.net/projects/hfs/files/latest/download
            将Full(其它的下载有问题)下的Window64，拖拽到hfs中（virtual folder） ，就得到了http://【你的IP】:8080/Windows64/
        02  修改Assets/GameMain/Scripts/Definition/DataStruct/VersionInfo.cs
            hfs根目录新建Version.txt，具体参数看BuildReport下的两个文件BuildLog.txt、BuildReport.xml、原来的BuildInfo.txt，hfs【有望编写工具】
                {
	                "ForceUpdateGame":false,
	                "LatestGameVersion":"0.1.0",
	                "InternalGameVersion":1,
	                "InternalResourceVersion":14,
	                "UpdatePrefixUri":"http://【HFS显示的IP】/Windows64/",
	                "VersionListLength":7158,
	                "VersionListHashCode":-1045455818,
	                "VersionListCompressedLength":2660,
	                "VersionListCompressedHashCode":-1777176079,
	                "END_OF_JSON":""
                }
                bug hfs识别不了这个 Version.txt（此时是default图标，不是txt图标）；浏览器NotFound
                    原因，Version.txt中文路径，改成英文（数字无妨）
        03 设置场景，看Test_Start01()
        bug JsonException: Invalid character '�' in input string
            看到了HFS有请求信息，所以应该是HFS上的Version.txt有错
            原因 文件编码格式，看原来的BuildInfo是什么格式（GB2312（代码页936）），新的BuildInfo和Version.txt都跟着改（VS/文件/高级保存选项）



*****************************************************/

using GameFramework;
using GameFramework.Resource;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class Test_AB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Test_Start01()
    {
        //BaseComponent b;
        //b.EditorResourceMode = false;

        //ResourceComponent r;
        //r.ResourceMode = ResourceMode.Updatable; // StreamingAssets没有内容会报错
        //r.ReadWritePathType = ReadWritePathType.Unspecified;
    }
}
