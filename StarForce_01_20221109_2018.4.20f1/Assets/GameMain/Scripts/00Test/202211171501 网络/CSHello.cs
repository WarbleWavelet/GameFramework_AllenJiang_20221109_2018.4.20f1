/****************************************************
  文件：TestNetwork.cs
  作者：lenovo
  邮箱: 
  日期：2022/11/18 17:34:26
  功能：
*****************************************************/

internal class CSHello :GameFramework.Network.Packet
{
    public CSHello()
    {
    }

    public string Name { get; set; }

    public override int Id { get;  }

    public override void Clear()
    {
        Common.Log_ClassFunction();
    }
}