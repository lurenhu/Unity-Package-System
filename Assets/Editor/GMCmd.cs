using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mono.Cecil;
using System;

public class GMCmd
{
    [MenuItem("GMCmd/读取表格")]
    public static void ReadTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable_");
        foreach (PackageTableItem packageTableItem in packageTable.dataList)
        {
            Debug.Log(string.Format("[id] :{0},[name] :{1}", packageTableItem.id, packageTableItem.name));
        }
    }

    [MenuItem("GMCmd/创建背包测试数据")]
    public static void CreateLocalPackageData()
    {
        //保存数据
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i = 0; i < 9; i++)
        {
            PackageLocalItem packageLocalItem = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                num = i,
                level = i,
                isNew = i/2 == 1
            };
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavePackage();
    }

    [MenuItem("GMCmd/读取背包测试数据")]
    public static void ReadLocalPackageData()
    {
        //读取数据
        List<PackageLocalItem> readItem = PackageLocalData.Instance.LoadPackage();
        foreach (PackageLocalItem item in readItem)
        {
            Debug.Log(item);
        }
    }

    [MenuItem("GMCmd/打开背包主界面")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanal(UIConst.PackagePanel);
    }
}
