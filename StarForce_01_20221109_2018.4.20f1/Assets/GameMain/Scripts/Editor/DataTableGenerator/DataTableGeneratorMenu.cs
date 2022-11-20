//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEditor;
using UnityEngine;

namespace StarForce.Editor.DataTableTools
{
    public sealed class DataTableGeneratorMenu
    {
        [MenuItem("Star Force/Generate DataTables")]
        private static void GenerateDataTables()
        {
            foreach (string dataTableName in ProcedurePreload.DataTableNames)
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }



        [MenuItem("Star Force/定位到编辑器脚本（主）")]
        private static void MenuItem_Process()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Scripts/Editor/DataTableGenerator/DataTableGeneratorMenu.cs");
        }

        [MenuItem("Star Force/定位到初始位置（DataTables 表）")]
        private static void MenuItem_From()    
        {
                Common.Selection_ActiveObject("Assets/GameMain/DataTables");
        }

        [MenuItem("Star Force/定位到生成位置（DataTable 脚本）")]
        private static void MenuItem_To()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Scripts/DataTable");
        }
        

        [MenuItem("Star Force/定位到实例Demo（Test）")]
        private static void MenuItem_Test1()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Scripts/00Test");
        }





        [MenuItem("Star Force/定位到实例Demo要测试的代码位置ProcedureMenu.cs（OnEnter）")]
        private static void MenuItem_TestTo()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Scripts/Procedure/ProcedureMenu.cs");
        }


        [MenuItem("Star Force/定位到代码生成模板（DataTableCodeTemplate.txt）")]
        private static void MenuItem_CodeTemp()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Configs/DataTableCodeTemplate.txt");
        }


        [MenuItem("Star Force/定位到Processor新增")]
        private static void DateTable_Add()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Scripts/Editor/DataTableGenerator/DataTableProcessor.cs");
        }


        [MenuItem("Star Force/定位到Processor调用（BinaryReaderExtension，DataTableExtension）")]
        private static void DateTable_Process()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Scripts/DataTable");
        }


        [MenuItem("Star Force/定位到Processor判断(DataTableGenerator.GenerateDataTableParser())")]
        private static void MenuItem_DateTable_Judge()
        {
            Common.Selection_ActiveObject("Assets/GameMain/Scripts/Editor/DataTableGenerator/DataTableGenerator.cs");
        }



        [MenuItem("Star Force/定位到事件Mgr")]
        private static void MenuItem_EventMgr()
        {
            Common.Selection_ActiveObject("Assets/Plugins/GameFramework/GameFramework/Event/EventManager.cs");
        }

        [MenuItem("Star Force/定位到LoadConfigSuccessEventArgs.cs")]
        private static void MenuItem_LoadConfigSuccessEventArgs()
        {
            Common.Selection_ActiveObject("Assets/Plugins/UnityGameFramework/Scripts/Runtime/Config/LoadConfigSuccessEventArgs.cs");
        }
    }
}
