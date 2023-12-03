using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanal
{
    Transform UILottery;
    Transform UIPackage;
    Transform UIExit;

    protected override void Awake()
    {
        base.Awake();
        InitUI();
    }

    private void InitUI()
    {
        UILottery = transform.Find("Lottery");
        UIPackage = transform.Find("Package");
        UIExit = transform.Find("Exit");

        UILottery.GetComponent<Button>().onClick.AddListener(OnLotteryClick);
        UIPackage.GetComponent<Button>().onClick.AddListener(OnPackageClick);
        UIExit.GetComponent<Button>().onClick.AddListener(OnExitClick);
    }

    private void OnLotteryClick()
    {
        UIManager.Instance.OpenPanal(UIConst.LotteryPanel);
        ClosePanal();
    }

    private void OnPackageClick()
    {
        UIManager.Instance.OpenPanal(UIConst.PackagePanel);
        ClosePanal();
    }

    private void OnExitClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
