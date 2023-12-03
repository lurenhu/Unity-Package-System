using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryCell : MonoBehaviour
{
    Transform UIIcon;
    Transform UIStar;
    Transform UIIsNew;
    private PackageLocalItem packageLocalItem;
    private PackageTableItem packageTableItem;
    private LotteryPanel uiParent;

    private void Awake() {
        InitUI();
    }

    private void InitUI()
    {
        UIIcon = transform.Find("Icon");
        UIStar = transform.Find("Start");
        UIIsNew = transform.Find("IsNew");

        UIIsNew.gameObject.SetActive(false);
    }

    public void Refresh(PackageLocalItem packageLocalItem,LotteryPanel uiParent)
    {
        //数据初始化
        this.packageLocalItem = packageLocalItem;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(this.packageLocalItem.id);
        this.uiParent = uiParent;
        //刷新界面
        RefreshImage();
        RefreshStar();
    }

    private void RefreshImage()
    {
        Texture2D t = (Texture2D)(Resources.Load(this.packageTableItem.imagePath));
        Sprite temp = Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.zero);
        UIIcon.GetComponent<Image>().sprite = temp;
    }

    private void RefreshStar()
    {
        for (int i = 0; i < UIStar.childCount; i++)
        {
            Transform Star = UIStar.GetChild(i);
            if (this.packageTableItem.star > i)
            {
                Star.gameObject.SetActive(true);
            }
            else
            {
                Star.gameObject.SetActive(false);
            }
        }
    }
}
