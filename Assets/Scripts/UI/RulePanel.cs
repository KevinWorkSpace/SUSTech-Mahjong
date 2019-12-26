using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class RulePanel : UIBase
{
    private Image imgBg;
    private Button btnRules;
    private Button btnClose;
    private Text txtRules;
    
    // Start is called before the first frame update
    void Start()
    {
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        btnRules = transform.Find("btnRules").GetComponent<Button>();
        txtRules = transform.Find("txtRules").GetComponent<Text>();
        btnClose.onClick.AddListener(closeClick);
        btnRules.onClick.AddListener(ruleClick);
        setObjectActive(false);
    }

    private void setObjectActive(bool active)
    {
        imgBg.gameObject.SetActive(active);
        btnClose.gameObject.SetActive(active);
        txtRules.gameObject.SetActive(active);
    }

    private void ruleClick()
    {
        setObjectActive(true);
    }

    private void closeClick()
    {
        setObjectActive(false);
    }
}
