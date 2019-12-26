using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchPanel : UIBase

{
    /*private void Awake()
    {
        Bind(UIEvent.SHOW_ENTER_ROOM_BUTTON);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SHOW_ENTER_ROOM_BUTTON:
                btnEnter.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }*/


    private Button btnMatch;

    private Text txtDes;

    private Button btnCancel;

    private Button btnEnter;
    void Start()
    {
        btnMatch = transform.Find("btnMatch").GetComponent<Button>();
        txtDes = transform.Find("txtDes").GetComponent<Text>();
        btnCancel = transform.Find("btnCancel").GetComponent<Button>();
        btnEnter = transform.Find("btnEnter").GetComponent<Button>();
        
        btnMatch.onClick.AddListener(matchClick);
        btnCancel.onClick.AddListener(cancelClick);
        
        setObjectActive(false);
        btnEnter.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMatching == false)
        {
            return;
        }
        Timer += Time.deltaTime;
        if (Timer >= intervalTime )
        {
            doAnimation();
            Timer = 0f;
        }
       
    }

    private void cancelClick()
    {
        setObjectActive(false);
    }

    private void matchClick()
    {
        setObjectActive(true);
        SceneManager.LoadScene("2.fight");
        isMatching = true;
    }
    /// <summary>
    /// 控制点击匹配按钮之后显示的物体
    /// </summary>
    /// <param name="active"></param>
    private void setObjectActive(bool active)
    {
        txtDes.gameObject.SetActive(active);
        btnCancel.gameObject.SetActive(active);
    }

    private String defaultText = "正在寻找房间";
    private int dotCount = 0;
    private bool isMatching = false;
    private float intervalTime = 1f;
    private float Timer = 0f;

    private void doAnimation()
    {
        txtDes.text = defaultText;
        dotCount++;
        if (dotCount > 5)
        {
            dotCount = 0;
        }
        for (int i = 0; i < dotCount; i++)
        {
            txtDes.text = txtDes.text + ".";
        }
    }
}
