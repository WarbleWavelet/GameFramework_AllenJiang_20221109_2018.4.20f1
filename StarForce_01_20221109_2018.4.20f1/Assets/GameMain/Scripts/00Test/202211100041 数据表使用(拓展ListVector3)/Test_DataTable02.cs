/****************************************************
  文件：Demo_202211100041.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/10 0:46:13
  功能：同样是放在Assets/GameMain/Scripts/Procedure/ProcedureMenu.cs（OnEnter）
        【过程】
        修改DataTable数据。txt可以拖到excel，改好后另存为“以制表符分隔（*txt）”
        中文乱码。将文本另存为 UTF-16LE

        先定义
        from=Assets/GameMain/DataTables
        to=Assets/GameMain/Scripts/DataTable
        from随便复制一份txt出来(测试完放回去)。然后去修改from里面的比如Aircraft（参考【过程】），然后栏目生成表数据类。对应自动生成的类也变化了

        bug
        Positions = binaryReader.ReadList`1();  【代码自动生成中读取二进制的方法名GetType不出来，我先直接写】
*****************************************************/

using GameFramework.DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
namespace Demo_202211131708
{
    /// <summary>写在这程序集会冲突，因为需要用到GameMain中的类</summary>
    public class Test_DataTable02
    {
        public Test_DataTable02()
        {

        }
    }
}

