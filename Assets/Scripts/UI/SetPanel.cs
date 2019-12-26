using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : UIBase
{
    /*private void Awake()
    {
        Bind();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            default:
                break;
        }
    }*/

    private Button btnSet;
    private Image imgBg;
    private Button btnClose;
    private Text txtAudio;
    private Toggle togAudio;
    private Text txtVolume;
    private Slider sldVolume;
    private Button btnQuit;

    // Start is called before the first frame update
    void Start()
    {
        btnSet = transform.Find("btnSet").GetComponent<Button>();
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        txtAudio = transform.Find("txtAudio").GetComponent<Text>();
        togAudio = transform.Find("togAudio").GetComponent<Toggle>();
        txtVolume = transform.Find("txtVolume").GetComponent<Text>();
        sldVolume = transform.Find("sldVolume").GetComponent<Slider>();
        btnQuit = transform.Find("btnQuit").GetComponent<Button>();

        btnSet.onClick.AddListener(setClick);
        btnClose.onClick.AddListener(closeClick);
        btnQuit.onClick.AddListener(quitClick);
        togAudio.onValueChanged.AddListener(audioValueChanged);
        sldVolume.onValueChanged.AddListener(volumeValueChanged);

        setObjectActive(false);
    }

    private void setObjectActive(bool active)
    {
        imgBg.gameObject.SetActive(active);
        btnClose.gameObject.SetActive(active);
        togAudio.gameObject.SetActive(active);
        sldVolume.gameObject.SetActive(active);
        btnQuit.gameObject.SetActive(active);
        txtAudio.gameObject.SetActive(active);
        txtVolume.gameObject.SetActive(active);
    }

    private void setClick()
    {
        setObjectActive(true);
    }

    private void closeClick()
    {
        setObjectActive(false);
    }

    private void quitClick()
    {
        Application.Quit();
    }

    private void audioValueChanged(bool result)
    {
        //操作声音
        // TODO
    }

    private void volumeValueChanged(float value)
    {
        //操作声音
        // TODO
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
