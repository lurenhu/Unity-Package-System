using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour
{
    private Transform UIIcon;
    private Transform UIHead;
    private Transform UINew;
    private Transform UISelect;
    private Transform UILevel;
    private Transform UIStars;
    private Transform UIDeleteSelect;
    private PackageLocalItem packageLocalItem;
    private PackageTableItem packageTableItem;
    private PackagePanel uiParent;

    private void Awake()
    {
        InitName();
    }

    private void InitName()
    {
        UIIcon = transform.Find("Top/Icon");
        UIHead = transform.Find("Top/Head");
        UINew = transform.Find("Top/New");
        UISelect = transform.Find("Select");

        UILevel = transform.Find("Bottom/Level");
        UIStars = transform.Find("Bottom/Start");
        UIDeleteSelect = transform.Find("DeleteSelect");

        UIDeleteSelect.gameObject.SetActive(false);
    }

    public void Refresh(PackageLocalItem packageLocalItem, PackagePanel uiParent)
    {
        //初始化数据
        this.packageLocalItem = packageLocalItem;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalItem.id);
        this.uiParent = uiParent;
        //等级信息
        UILevel.GetComponent<Text>().text = "Lv." + this.packageLocalItem.level.ToString();
        //是否为新获得的
        UINew.gameObject.SetActive(this.packageLocalItem.isNew);
        //物品的图片
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t,new Rect(0,0,t.width,t.height),new Vector2(0,0));
        UIIcon.GetComponent<Image>().sprite = temp;
        //刷新星数
        RefreshStars();

    }

    private void RefreshStars()
    {
        for (int i = 0; i < UIStars.childCount; i++)
        {
            Transform Star = UIStars.GetChild(i);
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
