using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance 
    {
        get
        {
            return _instance;
        }
    }

    private PackageTable packageTable;

    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        UIManager.Instance.OpenPanal(UIConst.PackagePanel);
    }

    public PackageTable GetPackageTable()
    {
        if (packageTable == null)
        {
            packageTable = Resources.Load<PackageTable>("TableData/PackageTable_");
        }
        return packageTable;
    }

    public List<PackageLocalItem> GetPackageLocalData()
    {
        return PackageLocalData.Instance.LoadPackage();
    }

    public PackageTableItem GetPackageItemById(int id)
    {
        List<PackageTableItem> packageDataList = GetPackageTable().dataList;
        foreach(PackageTableItem item in packageDataList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }

    public PackageLocalItem GetPackagerLocalDataByUid(string uid)
    {
        List<PackageLocalItem> packageDataList = GetPackageLocalData();
        foreach(PackageLocalItem item in packageDataList)
        {
            if (item.uid == uid)
            {
                return item;
            }
        }
        return null;
    }

    public List<PackageLocalItem> GetSortPackageLocalData()
    {
        List<PackageLocalItem> localItem = PackageLocalData.Instance.LoadPackage();
        localItem.Sort(new PackageItemCompare());
        return localItem;
    }
}


public class PackageItemCompare : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem a, PackageLocalItem b)
    {
        PackageTableItem x = GameManager.Instance.GetPackageItemById(a.id);
        PackageTableItem y = GameManager.Instance.GetPackageItemById(b.id);
        //先按照星的数量排序
        int StarCompare = y.star.CompareTo(x.star);
        
        //如果星的数量相同，按照id排序
        if (StarCompare == 0)
        {
            int IdCompare = y.id.CompareTo(x.id);
            if (IdCompare == 0)
            {
                return b.level.CompareTo(a.level);
            }
            return IdCompare;
        }
        return StarCompare;
    }
}