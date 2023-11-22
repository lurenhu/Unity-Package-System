using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanal
{
    private Transform UIMenu;
    private Transform UIMenuWeapon;
    private Transform UIMenuFood;
    private Transform UITabName;
    private Transform UICloseBtn;
    private Transform UICenter;
    private Transform UIScrollView;
    private Transform UIDetailPanel;
    private Transform UIRightBtn;
    private Transform UILeftBtn;
    private Transform UIDeletePanel;
    private Transform UIDeleteBackBtn;
    private Transform UIDeleteInfoText;
    private Transform UIDeleteConfirmBtn;
    private Transform UIBottomMenu;
    private Transform UIDeleteBtn;
    private Transform UIDetailBtn;

    override protected void Awake()
    {
        base.Awake();
        InitUI();
    }

    private void InitUI()
    {
        InitUIName();
        InitClick();
    }

    private void InitUIName()
    {
        UIMenu = transform.Find("Top/Menu");
        UIMenuWeapon = transform.Find("Top/Menu/Weapon");
        UIMenuFood = transform.Find("Top/Menu/Food");

        UITabName = transform.Find("LeftTop/PackageText");
        UICloseBtn = transform.Find("RightTop/Exit");

        UICenter = transform.Find("Center");
        UIScrollView = transform.Find("Center/Scroll View");
        UIDetailPanel = transform.Find("Center/Detail Panal");
        UIRightBtn = transform.Find("Center/Right");
        UILeftBtn = transform.Find("Center/Left");

        UIDeletePanel = transform.Find("Bottom/DeletePanal");
        UIDeleteBackBtn = transform.Find("Bottom/DeletePanal/Back");
        UIDeleteConfirmBtn = transform.Find("Bottom/DeletePanal/ConfirmBtn");
        UIDeleteInfoText = transform.Find("Bottom/DeletePanal/InfoText");
        UIBottomMenu = transform.Find("Bottom/BottomMenus"); 
        UIDeleteBtn = transform.Find("Bottom/BottomMenus/DeleteBtn");
        UIDetailBtn = transform.Find("Bottom/BottomMenus/DetailBtn");

        UIDeletePanel.gameObject.SetActive(false);
        UIBottomMenu.gameObject.SetActive(true);
        
    }

    private void InitClick()
    {
        UIMenuFood.GetComponent<Button>().onClick.AddListener(OnClickFood);
        UIMenuWeapon.GetComponent<Button>().onClick.AddListener(OnClickWeapon);
        UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        UILeftBtn.GetComponent<Button>().onClick.AddListener(OnClickLeft);
        UIRightBtn.GetComponent<Button>().onClick.AddListener(OnClickRight);

        UIDeleteBackBtn.GetComponent<Button>().onClick.AddListener(OnClickDeleteBack);
        UIDeleteConfirmBtn.GetComponent<Button>().onClick.AddListener(OnClickDeleteConfirm);
        UIDeleteBtn.GetComponent<Button>().onClick.AddListener(OnClickDelete);
        UIDetailBtn.GetComponent<Button>().onClick.AddListener(OnClickDetail);
    }

    private void OnClickDetail()
    {
        throw new NotImplementedException();
    }

    private void OnClickDelete()
    {
        throw new NotImplementedException();
    }

    private void OnClickDeleteConfirm()
    {
        throw new NotImplementedException();
    }

    private void OnClickRight()
    {
        throw new NotImplementedException();
    }

    private void OnClickLeft()
    {
        throw new NotImplementedException();
    }

    private void OnClickClose()
    {
        throw new NotImplementedException();
    }

    private void OnClickFood()
    {
        throw new NotImplementedException();
    }

    private void OnClickWeapon()
    {
        throw new NotImplementedException();
    }

    private void OnClickDeleteBack()
    {
        throw new NotImplementedException();
    }
}
