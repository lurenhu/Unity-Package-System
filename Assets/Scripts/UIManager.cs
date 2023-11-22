using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;
    private Transform _uiRoot;
    //配置路径字典
    private Dictionary<string,string> pathDict;
    //预制体缓存字典
    private Dictionary<string ,GameObject> prefabDict;
    //已打开界面缓存字典
    public Dictionary<string,BasePanal> panalDict;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UIManager();
            return _instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if(_uiRoot == null)
            {
                if(GameObject.Find("Canvas"))
                {
                    _uiRoot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    _uiRoot = new GameObject("Canvas").transform;
                }
            };
            return _uiRoot;
        }
    }
    
    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panalDict = new Dictionary<string, BasePanal>();

        pathDict = new Dictionary<string, string>()
        {
            {UIConst.PackagePanel,"Package/PackagePanal"},
        };
    }

    public BasePanal GetPanal(string name)
    {
        BasePanal panal = null;
        if (panalDict.TryGetValue(name, out panal))
        {
            return panal;
        }
        return null;
    }

    public BasePanal OpenPanal(string name)
    {
        //检查界面是否打开
        BasePanal panal = null;
        if (panalDict.TryGetValue(name, out panal))
        {
            Debug.Log("界面已打开：" + name);
            return null;
        }

        //检查路径是否配置
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("界面名称错误，或者未配置路径：" + name);
            return null;
        }

        //使用缓存的预制件
        GameObject panalPrefab = null;
        if (!prefabDict.TryGetValue(name, out panalPrefab))
        {
            string realpath = "Prefab/Panal/" + path;
            panalPrefab = Resources.Load<GameObject>(realpath) as GameObject;
            prefabDict.Add(name, panalPrefab);
        }

        //打开界面
        GameObject panalObject = GameObject.Instantiate(panalPrefab,UIRoot,false);
        panal = panalObject.GetComponent<BasePanal>();
        panalDict.Add(name, panal);
        panal.OpenPanal(name);
        return panal;

    }

    public bool ClosePanal(string name)
    {
        BasePanal panal = null;
        if (!panalDict.TryGetValue(name, out panal))
        {
            Debug.Log("界面未打开："+ name);
            return false;
        }

        panal.ClosePanal();
        return true;
    }
}

public class UIConst
{
    public const string PackagePanel = "PackagePanel";
}
