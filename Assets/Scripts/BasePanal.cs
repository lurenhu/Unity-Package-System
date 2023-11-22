using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanal : MonoBehaviour
{
    protected bool isRemove = false;
    protected new string name;

    protected virtual void Awake() {
        
    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void OpenPanal(string name)
    {
        this.name = name;
        SetActive(true);
    }

    public virtual void ClosePanal()
    {
        isRemove = true;
        SetActive(false);
        Destroy(gameObject);

        if (UIManager.Instance.panalDict.ContainsKey(name))
        {
            UIManager.Instance.panalDict.Remove(name);
        }
    }
}
