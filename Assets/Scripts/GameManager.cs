using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        UIManager.Instance.OpenPanal(UIConst.MainPanel);
    }

    public void DeletePackageItems(List<string> uids)
    {
        foreach (string uid in uids)
        {
            DeletePackageItem(uid, false);
        }
        PackageLocalData.Instance.SavePackage();
    }

    public void DeletePackageItem(string uid, bool needSave = true)
    {
        PackageLocalItem packageLocalItem = GetPackagerLocalDataByUid(uid);
        if (packageLocalItem == null)
        {
            return;
        }
        PackageLocalData.Instance.items.Remove(packageLocalItem);
        if (needSave)
        {
            PackageLocalData.Instance.SavePackage();
        }
    }

    /// <summary>
    /// 获取静态数据
    /// </summary>
    /// <returns></returns>
    public PackageTable GetPackageTable()
    {
        if (packageTable == null)
        {
            packageTable = Resources.Load<PackageTable>("TableData/PackageTable_");
        }
        return packageTable;
    }

    /// <summary>
    /// 获取动态数据
    /// </summary>
    /// <returns></returns>
    public List<PackageLocalItem> GetPackageLocalData()
    {
        return PackageLocalData.Instance.LoadPackage();
    }

    /// <summary>
    /// 通过id获取对应静态数据
    /// </summary>
    /// <param name="id">物品id</param>
    /// <returns>内存中对应的静态项</returns>
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

    /// <summary>
    /// 通过uid获取对应动态数据
    /// </summary>
    /// <param name="uid">物品的uid</param>
    /// <returns>本地动态数据中对应的项</returns>
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

    /// <summary>
    /// 通过type来获取静态数据
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<PackageTableItem> GetPackageDataByType(int type)
    {
        List<PackageTableItem> packageTableItems = new List<PackageTableItem>();
        foreach (PackageTableItem item in GetPackageTable().dataList)
        {
            if (item.type == type)
            {
                packageTableItems.Add(item);
            }
        }
        return packageTableItems;
    }

    /// <summary>
    /// 随机抽卡一次
    /// </summary>
    /// <returns></returns>
    public PackageLocalItem GetLotteryRandom1()
    {
        List<PackageTableItem> packageTableItems = GetPackageDataByType(GameConst.PackageTypeWeapon);
        int index = Random.Range(0, packageTableItems.Count);
        PackageTableItem packageTableItem = packageTableItems[index];
        PackageLocalItem packageLocalItem = new PackageLocalItem()
        {
            uid = System.Guid.NewGuid().ToString(),
            id = packageTableItem.id,
            num = 1,
            level = 1,
            isNew = CheckWeaponIsNew(packageTableItem.id)
        };
        PackageLocalData.Instance.items.Add(packageLocalItem);
        PackageLocalData.Instance.SavePackage();
        return packageLocalItem;
        
    }

    /// <summary>
    /// 连续抽十次
    /// </summary>
    /// <param name="Sort"></param>
    /// <returns></returns>
    public List<PackageLocalItem> GetLotteryRandom10(bool Sort = false)
    {
        List<PackageLocalItem> packageLocalItems = new();
        for (int i = 0; i < 10; i++)
        {
            PackageLocalItem packageLocalItem = GetLotteryRandom1();
            packageLocalItems.Add(packageLocalItem);
        }
        if (Sort)
        {
            packageLocalItems.Sort(new PackageItemCompare());
        }
        return packageLocalItems;
    }

    public bool CheckWeaponIsNew(int id)
    {
        foreach(PackageLocalItem item in GetPackageLocalData())
        {
            if (item.id == id)
            {
                return  false;
            }
        }
        return true;
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

public class GameConst
{
    public const int PackageTypeWeapon = 1;
    public const int PackageTypeFood = 2;
}