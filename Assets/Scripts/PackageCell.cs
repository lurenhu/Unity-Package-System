using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCell : MonoBehaviour
{
    private Transform UIIcon;
    private Transform UIHead;
    private Transform UINew;
    private Transform UISelect;
    private Transform UILevel;
    private Transform UIStars;
    private Transform UIDeleteSelect;

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
}
