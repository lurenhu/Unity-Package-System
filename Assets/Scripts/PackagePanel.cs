using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public enum PackageMode
{
    normal,
    delete,
    sort
}

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

    public GameObject PackageUIItemPrefab;

    public List<string> deleteChooseUid;

    public PackageMode curMode = PackageMode.normal; 

    private string _chooseUid;
    public string ChooseUid
    {
        get
        {
            return _chooseUid;
        }
        set
        {
            _chooseUid = value;
            RefreshDetail();
        }
    }

    public void AddChooseDeleteUid(string uid)
    {
        this.deleteChooseUid??= new List<string>();
        if (!this.deleteChooseUid.Contains(uid))
        {
            this.deleteChooseUid.Add(uid);
        }else
        {
            this.deleteChooseUid.Remove(uid);
        }
        RefreshDeletePanel();
    }

    public void RefreshDeletePanel()
    {
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        foreach(Transform cell in scrollContent)
        {
            PackageCell packageCell = cell.GetComponent<PackageCell>();
            packageCell.RefreshDeleteState();
        }
    }
    override protected void Awake()
    {
        base.Awake();
        InitUI();
    }

    private void Start() {
        RefreshUI();
    }

    private void InitUI()
    {
        InitUIName();
        InitClick();
    }

    private void RefreshDetail()
    {
        //找到uid对应的动态数据
        PackageLocalItem localItem = GameManager.Instance.GetPackagerLocalDataByUid(ChooseUid);
        //刷新详情界面
        UIDetailPanel.GetComponent<PackageDetail>().Refresh(localItem,this);
    }

    private void RefreshUI()
    {
        RefreshScrollView();
    }

    private void RefreshScrollView()
    {
        //清理原本容器中的物体
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        //重新实例化本地数据中的物品数据
        foreach(PackageLocalItem localData in GameManager.Instance.GetSortPackageLocalData())
        {
            Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent) as Transform;
            PackageCell packageCell = PackageUIItem.GetComponent<PackageCell>();
            packageCell.Refresh(localData,this);
        }

    }

    
    private void InitUIName()
    {   //绑定所有UI的对象——通过在Inspector中的相对路径来绑定
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
    {   //为所有Button组件添加点击事件
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
        curMode = PackageMode.delete;
        UIDeletePanel.gameObject.SetActive(true);

    }

    private void OnClickDeleteConfirm()
    {
        if (this.deleteChooseUid == null)
        {
            return;
        }
        if (this.deleteChooseUid.Count == 0)
        {
            return;
        }
        GameManager.Instance.DeletePackageItems(deleteChooseUid);
        RefreshUI();
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
        ClosePanal();
        UIManager.Instance.OpenPanal(UIConst.MainPanel);
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
        curMode = PackageMode.normal;
        UIDeletePanel.gameObject.SetActive(false);
        deleteChooseUid = new List<string>();
        RefreshDeletePanel();
    }
}
