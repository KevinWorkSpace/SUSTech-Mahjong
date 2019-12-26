using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : UIBase
{
    private Text txtName;
    private Text txtLv;
    private Slider sldExp;
    private Text txtExp;
    void Start()
    {
        txtName = transform.Find("txtName").GetComponent<Text>();
        txtLv = transform.Find("txtLv").GetComponent<Text>();
        sldExp = transform.Find("sldExp").GetComponent<Slider>();
        txtExp = transform.Find("txtExp").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
