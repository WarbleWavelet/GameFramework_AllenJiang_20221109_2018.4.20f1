//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StarForce.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        /// <summary>自定义</summary>
        private sealed class Vector3ListProcessor : GenericDataProcessor<List<Vector3>>
        {
            public override bool IsSystem
            {
                get
                {
                    return false;
                }
            }

            public override string LanguageKeyword
            {
                get
                {
                    return "List<Vector3>";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "List<Vector3>",
                    "Unityengine.List<Vector3>"
                };
            }

            public override List<Vector3> Parse(string value)
            {
                List<Vector3> lst = new List<Vector3>();
                if (value == "" || value == "empty")
                {
                    return lst;
                }

                string[] splitedValues = value.Split(';');
                for (int i = 0; i < splitedValues.Length; i++)
                {
                    string[] splitedValue = splitedValues[i].Split(',');
                    lst.Add( new Vector3(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2])));
                }
                return lst;
            }

            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                List<Vector3> valueLst= Parse(value);  
                binaryWriter.Write((float)valueLst.Count);

                for (int i = 0; i < valueLst.Count; i++)
                {
                    Vector3 vector3 = valueLst[i];
                    binaryWriter.Write(vector3.x);
                    binaryWriter.Write(vector3.y);
                    binaryWriter.Write(vector3.z);
                }

            }
        }
    }
}
