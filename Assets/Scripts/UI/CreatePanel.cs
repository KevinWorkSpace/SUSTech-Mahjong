using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : UIBase
{
    private void Awake()
    {
        Bind(UIEvent.CREATE_PANEL_ACTIVE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.CREATE_PANEL_ACTIVE:
                setPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    private InputField inputName;
    private Button btnCreate;
    void Start()
    {
        inputName = transform.Find("inputName").GetComponent<InputField>();
        btnCreate = transform.Find("btnCreate").GetComponent<Button>();
        
        btnCreate.onClick.AddListener(createClick);
    }

    private void createClick()
    {
        if (string.IsNullOrEmpty(inputName.text))
        {
            
        }
        //向服务器发送一个创建的请求
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
