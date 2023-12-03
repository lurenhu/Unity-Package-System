using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryPanel : BasePanal
{
    Transform UIBack;
    Transform UICenter;
    Transform UILottery10;
    Transform UILottery1;
    GameObject Prefab_LotteryItem;

    protected override void Awake()
    {
        base.Awake();
        InitUI();
        LoadPrefab();
    }

    private void InitUI()
    {
        UIBack = transform.Find("TopRight/Back");
        UICenter = transform.Find("Center");
        UILottery1 = transform.Find("Bottom/Lottery1");
        UILottery10 = transform.Find("Bottom/Lottery10");

        UIBack.GetComponent<Button>().onClick.AddListener(OnClickBack);
        UILottery1.GetComponent<Button>().onClick.AddListener(OnClickLottery1);
        UILottery10.GetComponent<Button>().onClick.AddListener(OnClickLottery10);
    }

    private void LoadPrefab()
    {
        Prefab_LotteryItem = Resources.Load("Prefab/Panal/Lottery/LotteryItem") as GameObject;
    }

    private void OnClickBack()
    {
        Debug.Log(">>>>>>OnClickBack");
        ClosePanal();
        UIManager.Instance.OpenPanal(UIConst.MainPanel);
    }

    private void OnClickLottery1()
    {
        Debug.Log(">>>>>>OnClickLottery1");
        //销毁所有子物体
        for (int i = 0; i < UICenter.childCount; i++)
        {
            Destroy(UICenter.GetChild(i).gameObject);
        }
        //初始化并刷新界面
        PackageLocalItem packageLocalItem = GameManager.Instance.GetLotteryRandom1();
        Transform LotteryCellTran = Instantiate(Prefab_LotteryItem.transform, UICenter) as Transform;
        LotteryCell lotteryCell = LotteryCellTran.GetComponent<LotteryCell>();
        lotteryCell.Refresh(packageLocalItem,this);
    }

    private void OnClickLottery10()
    {
        Debug.Log(">>>>>>OnClickLottery10");
        //销毁所有子物体
        for (int i = 0; i < UICenter.childCount; i++)
        {
            Destroy(UICenter.GetChild(i).gameObject);
        }

        //初始化并刷新界面
        List<PackageLocalItem> packageLocalItemList = GameManager.Instance.GetLotteryRandom10(Sort : true);
        foreach (PackageLocalItem packageLocalItem in packageLocalItemList)
        {
            Transform LotteryCellTran = Instantiate(Prefab_LotteryItem.transform, UICenter) as Transform;
            LotteryCell lotteryCell = LotteryCellTran.GetComponent<LotteryCell>();
            lotteryCell.Refresh(packageLocalItem,this);
        }

    }
}
