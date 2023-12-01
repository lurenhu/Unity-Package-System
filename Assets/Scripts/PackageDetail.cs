using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    private Transform UITitle;
    private Transform UIStar;
    private Transform UIDescription;
    private Transform UIIcon;
    private Transform UISkillDescription;
    private Transform UILevelText;

    private PackageLocalItem packageLocalItem;
    private PackageTableItem packageTableItem;
    private PackagePanel uiParent;

    private void Awake() {
        InitUIName();
        Test();
    }

    private void Test() {
        Refresh(GameManager.Instance.GetPackageLocalData()[1],null);
    }

    private void InitUIName() {
        UITitle = transform.Find("Top/Title");
        UIStar = transform.Find("Center/Start");
        UIDescription = transform.Find("Center/Descrption");
        UIIcon = transform.Find("Center/Icon");
        UISkillDescription = transform.Find("Bottom/Description");
        UILevelText = transform.Find("Bottom/Level Pnl/Level Text");
    }

    public void Refresh(PackageLocalItem packageLocalItem, PackagePanel uiParent)
    {
        //初始化本地数据，动态数据和静态数据
        this.packageLocalItem = packageLocalItem;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalItem.id);
        this.uiParent = uiParent;
        //等级
        UILevelText.GetComponent<Text>().text = string.Format("Lv.{0}/40", this.packageLocalItem.level.ToString());
        //简短描述
        UIDescription.GetComponent<Text>().text = this.packageTableItem.description;
        //详细描述
        UISkillDescription.GetComponent<Text>().text = this.packageTableItem.skillDescription;
        //物品名称
        UITitle.GetComponent<Text>().text = this.packageTableItem.name;
        //图片加载
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero);
        UIIcon.GetComponent<Image>().sprite = temp;
        //刷新星级
        RefreshStars();
    }

     private void RefreshStars()
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
