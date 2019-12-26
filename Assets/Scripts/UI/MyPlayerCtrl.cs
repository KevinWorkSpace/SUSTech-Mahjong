using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameServer.Cache.Fight;
using Protocol.Dto.Fight;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class MyPlayerCtrl : CharacterBase
{

    /// <summary>
    /// 自身管理的卡牌列表
    /// </summary>
    private List<CardDto> cardDtoList;

    private List<CardCtrl> cardCtrList;

    private Button btnEnter;
    private Button btnEat;
    private Button btnNoEat;
    private Button btnPeng;
    private Button btnNoPeng;
    private Button btnBack;

    private List<CardDto> list;

    /// <summary>
    /// 卡牌的父物体
    /// </summary>
    private Transform cardParent;

    private Transform SelectCard1;
    private Transform SelectCard2;
    private Transform SelectCard3;
    private Transform SelectCard4;

    private CardDto card1;
    private CardDto card2;
    private CardDto card3;
    private CardDto card4;

    private GameObject cardGo1;
    private GameObject cardGo2;
    private GameObject cardGo3;
    private GameObject cardGo4;

    private Text gongxiText;

    private RectTransform overPanel;

    private int currentPlayer;
    
    private int count = 0;

    private bool chiORpeng = false;

    // Use this for initialization
    void Start()
    {
        Canvas c = transform.Find("Canvas").GetComponent<Canvas>();
        overPanel = c.transform.Find("OverPanel").GetComponent<RectTransform>();
        overPanel.gameObject.SetActive(false);
        btnBack = c.transform.Find("btnBack").GetComponent<Button>();
        btnBack.onClick.AddListener(backToMain);
        btnBack.gameObject.SetActive(false);
        gongxiText = c.transform.Find("gongxiText").GetComponent<Text>();
        gongxiText.gameObject.SetActive(false);
        btnEnter = c.transform.Find("btnEnter").GetComponent<Button>();
        btnEnter.onClick.AddListener(dealSelectCard);
        btnEat = c.transform.Find("btnEat").GetComponent<Button>();
        btnNoEat = c.transform.Find("btnNoEat").GetComponent<Button>();
        btnPeng = c.transform.Find("btnPeng").GetComponent<Button>();
        btnNoPeng = c.transform.Find("btnNoPeng").GetComponent<Button>();
        btnEat.onClick.AddListener(eat);
        btnNoEat.onClick.AddListener(noEat);
        btnPeng.onClick.AddListener(peng);
        btnNoPeng.onClick.AddListener(noPeng);
        btnEat.gameObject.SetActive(false);
        btnNoEat.gameObject.SetActive(false);
        btnPeng.gameObject.SetActive(false);
        btnNoPeng.gameObject.SetActive(false);
        LibraryModel lm = new LibraryModel();
        cardParent = transform.Find("CardPoint");
        SelectCard1 = transform.Find("SelectCard1");
        SelectCard2 = transform.Find("SelectCard2");
        SelectCard3 = transform.Find("SelectCard3");
        SelectCard4 = transform.Find("SelectCard4");

        cardDtoList = new List<CardDto>();
        cardCtrList = new List<CardCtrl>();
        list = lm.CardQueue.ToList();
        /*List<CardDto> l = new List<CardDto>();*/
        for (int i = 0; i < 10; i++)
        {
            cardDtoList.Add(list[0]);
            list.RemoveAt(0);
        }
        initCardList();
    }

    private void backToMain()
    {
        SceneManager.LoadScene("1.main");
    }

    /// <summary>
    /// 初始化显示卡牌
    /// </summary>
    private IEnumerator initCardList()
    {
        //Debug.Log(cardList);
        cardDtoList.Sort();
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard");
        for (int i = 0; i < cardDtoList.Count; i++)
        {
            StartCoroutine(Wait(cardDtoList, i, cardPrefab));
            //createGo(cardDtoList[i], i, cardPrefab);
            /*yield return new WaitForSeconds(0.1f);*/
        }
        //GameObject cardGo = Object.Instantiate(cardPrefab, cardParent) as GameObject;
        //cardGo.transform.localPosition = new Vector2((0.45f * 9), 0);
        //CardCtrl cardCtrl = cardGo.GetComponent<CardCtrl>();
        //CardDto card = list[0];
        //list.RemoveAt(0);
        //cardCtrl.Init(card, 9, true);
        //cardCtrList.Add(cardCtrl);
        return null;
    }

    IEnumerator Wait(List<CardDto> list, int i, GameObject cardPrefab)
    {
        yield return new WaitForSeconds(1);
        createGo(list[i], i, cardPrefab);

    }

    

    /// <summary>
    /// 创建卡牌游戏物体
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    private void createGo(CardDto card, int index, GameObject cardPrefab)
    {
        GameObject cardGo = Object.Instantiate(cardPrefab, cardParent) as GameObject;
        cardGo.name = card.Name;
        cardGo.transform.localPosition = new Vector2((0.45f * index), 0);
        CardCtrl cardCtrl = cardGo.GetComponent<CardCtrl>();
        cardCtrl.Init(card, index, true);
        //存储本地
        cardCtrList.Add(cardCtrl);
 
    }

    /// <summary>
    /// 出牌 出选中的牌
    /// </summary>
    private void dealSelectCard()
    {
        destroy();
        List<CardDto> selectCardList = getSelectCard();
        //if (!chiORpeng)
        //{
          //  cardDtoList.Add(list[0]);
            //list.RemoveAt(0);
        //}
        card1 = selectCardList[0];
        cardDtoList.Remove(selectCardList[0]);
        removeCard();
        //chiORpeng = false;
        cardDtoList.Sort();
        cardVisual1();
        currentPlayer = 1;
        //btnEnter.gameObject.SetActive(false);
        Invoke("cardPlay4", 2);
    }

    private void cardPlay4()
    {
        card4 = list[0];
        list.RemoveAt(0);
        cardVisual4();
        currentPlayer = 4;
        print(isChi(card4));
        if (isChi(card4))
        {
            btnEat.gameObject.SetActive(true);
            btnNoEat.gameObject.SetActive(true);
        }
        else if (isPeng(card4))
        {
            btnPeng.gameObject.SetActive(true);
            btnNoPeng.gameObject.SetActive(true);
        }
        else
        {
            Invoke("cardPlay3", 1);    
        }
    }

    private void cardPlay3()
    {
        card3 = list[0];
        list.RemoveAt(0);
        cardVisual3();
        currentPlayer = 3;
        print(isChi(card3));
        if (isChi(card3))
        {
            btnEat.gameObject.SetActive(true);
            btnNoEat.gameObject.SetActive(true);
        }
        else if (isPeng(card3))
        {
            btnPeng.gameObject.SetActive(true);
            btnNoPeng.gameObject.SetActive(true);
        }
        else
        {
            Invoke("cardPlay2", 2);    
        }
    }

    private void cardPlay2()
    {
        card2 = list[0];
        list.RemoveAt(0);
        cardVisual2();
        currentPlayer = 2;
        if (isChi(card2))
        {
            btnEat.gameObject.SetActive(true);
            btnNoEat.gameObject.SetActive(true);
        }
        else if (isChi(card2))
        {
            btnPeng.gameObject.SetActive(true);
            btnNoPeng.gameObject.SetActive(true);
        }
        else
        {
            Invoke("addCard", 2);
        }
    }

    public void addCard()
    {
        cardDtoList.Add(list[0]);
        list.RemoveAt(0);
        cardDtoList.Sort();
        removeCard();
        if (isZimo())
        {
            print("胡了");
            overPanel.gameObject.SetActive(true);
            btnBack.gameObject.SetActive(true);
            gongxiText.gameObject.SetActive(true);
        }
        //btnEnter.gameObject.SetActive(true);
    }

    private void destroy()
    {
        Destroy(cardGo1);
        Destroy(cardGo2);
        Destroy(cardGo3);
        Destroy(cardGo4);
    }
    private void cardVisual1()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard");
        cardGo1 = Object.Instantiate(cardPrefab, SelectCard1) as GameObject;
        cardGo1.transform.localPosition = new Vector2(0, 0);
        CardCtrl cardCtrl = cardGo1.GetComponent<CardCtrl>();
        cardCtrl.Init(card1, 1, true);
    }
    
    private void cardVisual2()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard");
        cardGo2 = Object.Instantiate(cardPrefab, SelectCard2) as GameObject;
        cardGo2.transform.localPosition = new Vector2(0, 0);
        CardCtrl cardCtrl = cardGo2.GetComponent<CardCtrl>();
        cardCtrl.Init(card2, 1, true);
    }
    
    private void cardVisual3()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard");
        cardGo3 = Object.Instantiate(cardPrefab, SelectCard3) as GameObject;
        cardGo3.transform.localPosition = new Vector2(0, 0);
        CardCtrl cardCtrl = cardGo3.GetComponent<CardCtrl>();
        cardCtrl.Init(card3, 1, true);
    }
    
    private void cardVisual4()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard");
        cardGo4 = Object.Instantiate(cardPrefab, SelectCard4) as GameObject;
        cardGo4.transform.localPosition = new Vector2(0, 0);
        CardCtrl cardCtrl = cardGo4.GetComponent<CardCtrl>();
        cardCtrl.Init(card4, 1, true);
    }

    private void eat()
    {
        chiORpeng = true;
        switch (currentPlayer)
        {
            case 1:
                break;
            case 2:
                cardDtoList.Add(card2);
                cardDtoList.Sort();
                removeCard();
                if (isHu(card2))
                {
                    print("胡了");
                    overPanel.gameObject.SetActive(true);
                    btnBack.gameObject.SetActive(true);
                    gongxiText.gameObject.SetActive(true);
                }
                //btnEnter.gameObject.SetActive(true);
                break;
            case 3:
                cardDtoList.Add(card3);
                cardDtoList.Sort();
                removeCard();
                if (isHu(card3))
                {
                    print("胡了");
                    overPanel.gameObject.SetActive(true);
                    btnBack.gameObject.SetActive(true);
                    gongxiText.gameObject.SetActive(true);
                }
                //btnEnter.gameObject.SetActive(true);
                break;
            case 4:
                cardDtoList.Add(card4);
                cardDtoList.Sort();
                removeCard();
                if (isHu(card4))
                {
                    print("胡了");
                    overPanel.gameObject.SetActive(true);
                    btnBack.gameObject.SetActive(true);
                    gongxiText.gameObject.SetActive(true);
                }
                //btnEnter.gameObject.SetActive(true);
                break;
        }
        btnEat.gameObject.SetActive(false);
        btnNoEat.gameObject.SetActive(false);
    }

    private void noEat()
    {
        switch (currentPlayer)
        {
            case 1:
                break;
            case 2:
                cardDtoList.Add(list[0]);
                list.RemoveAt(0);
                cardDtoList.Sort();
                removeCard();
                //btnEnter.gameObject.SetActive(true);
                break;
            case 3:
                cardPlay2();
                break;
            case 4:
                cardPlay3();
                break;
        }
        btnEat.gameObject.SetActive(false);
        btnNoEat.gameObject.SetActive(false);
    }
    
    private void peng()
    {
        chiORpeng = true;
        switch (currentPlayer)
        {
            case 1:
                break;
            case 2:
                if (isHu(card2))
                {
                    print("胡了");
                    overPanel.gameObject.SetActive(true);
                    btnBack.gameObject.SetActive(true);
                    gongxiText.gameObject.SetActive(true);
                }
                cardDtoList.Add(card2);
                cardDtoList.Sort();
                removeCard();
                //btnEnter.gameObject.SetActive(true);
                break;
            case 3:
                if (isHu(card3))
                {
                    print("胡了");
                    overPanel.gameObject.SetActive(true);
                    btnBack.gameObject.SetActive(true);
                    gongxiText.gameObject.SetActive(true);
                }
                cardDtoList.Add(card3);
                cardDtoList.Sort();
                removeCard();
                //btnEnter.gameObject.SetActive(true);
                break;
            case 4:
                if (isHu(card4))
                {
                    print("胡了");
                    overPanel.gameObject.SetActive(true);
                    btnBack.gameObject.SetActive(true);
                    gongxiText.gameObject.SetActive(true);
                }
                cardDtoList.Add(card4);
                cardDtoList.Sort();
                removeCard();
                //btnEnter.gameObject.SetActive(true);
                break;
        }
        btnPeng.gameObject.SetActive(false);
        btnNoPeng.gameObject.SetActive(false);
    }
    
    private void noPeng()
    {
        switch (currentPlayer)
        {
            case 1:
                break;
            case 2:
                cardDtoList.Add(list[0]);
                list.RemoveAt(0);
                removeCard();
                //btnEnter.gameObject.SetActive(true);
                break;
            case 3:
                cardPlay2();
                break;
            case 4:
                cardPlay3();
                break;
        }
        btnPeng.gameObject.SetActive(false);
        btnNoPeng.gameObject.SetActive(false);
    }
    
    
/*DealDto dto = new DealDto(selectCardList, Models.GameModel.Id);*/


    //进行合法判断
    /*if (dto.IsRegular == false)
    {
        //如果出牌不合法  2 4
        promptMsg.Change("请选择合理的手牌！", Color.red);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        return;
    }*/

    /// <summary>
    /// 获取选中的牌
    /// </summary>
    private List<CardDto> getSelectCard()
    {
        
        List<CardDto> selectCardList = new List<CardDto>();
        foreach (var cardCtrl in cardCtrList)
        {
            if (cardCtrl.Selected)
            {
                selectCardList.Add(cardCtrl.CardDto);
                print(cardCtrl.CardDto.Name);
            }
        }
        return selectCardList;
    }

    /// <summary>
    /// 移除卡牌的游戏物体
    /// </summary>
    private void removeCard()
    {
        int index = 0;
        foreach (var cc in cardCtrList)
        {
            if (cardDtoList.Count == 0)
                break;
            else
            {
                cc.gameObject.SetActive(true);
                cc.Init(cardDtoList[index], index, true);
                index++;
                //没有牌了
                if (index == cardDtoList.Count)
                {
                    break;
                }
            }
        }

        //把index之后的牌 都隐藏掉
        for (int i = index; i < cardCtrList.Count; i++)
        {
            cardCtrList[i].Selected = false;
            //Destroy(cardCtrlList[i].gameObject);
            cardCtrList[i].gameObject.SetActive(false);
        }
    } 
    
    
    
    private bool contains(CardDto card)
    {
        for(int i = 0; i < cardDtoList.Count; i++){
            CardDto c = cardDtoList[i];
            if(c.Name.Equals(card.Name))
                return true;
        }
        return false;
    }
    
    private bool isPeng(CardDto card)
    {
        CardDto cardBixiuOne = new CardDto("BixiuOne", 1, 0);
        CardDto cardBixiuTwo = new CardDto("BixiuTwo", 1, 0);
        CardDto cardBixiuThree = new CardDto("BixiuThree", 1, 0);
        CardDto cardBixiuFour = new CardDto("BixiuFour", 1, 0);
        CardDto cardBixiuFive = new CardDto("BixiuFive", 1, 0);
        CardDto cardBixiuSix = new CardDto("BixiuSix", 1, 0);
        switch(card.Name)
        {
            case "BixiuOne":
                if(contains(cardBixiuTwo)&&contains(cardBixiuThree)&&contains(cardBixiuFour)
                    &&contains(cardBixiuFive)&&contains(cardBixiuSix)&&(!contains(cardBixiuOne)))
                    return true;
                else
                    return false;
            case "BixiuTwo":
                if(contains(cardBixiuOne)&&contains(cardBixiuThree)&&contains(cardBixiuFour)
                    &&contains(cardBixiuFive)&&contains(cardBixiuSix)&&(!contains(cardBixiuTwo)))
                    return true;
                else
                    return false;
            case "BixiuThree":
                if(contains(cardBixiuOne)&&contains(cardBixiuTwo)&&contains(cardBixiuFour)
                    &&contains(cardBixiuFive)&&contains(cardBixiuSix)&&(!contains(cardBixiuThree)))
                    return true;
                else
                    return false;
            case "BixiuFour":
                if(contains(cardBixiuOne)&&contains(cardBixiuTwo)&&contains(cardBixiuThree)
                    &&contains(cardBixiuFive)&&contains(cardBixiuSix)&&(!contains(cardBixiuFour)))
                    return true;
                else
                    return false;
            case "BixiuFive":
                if(contains(cardBixiuOne)&&contains(cardBixiuTwo)&&contains(cardBixiuThree)
                    &&contains(cardBixiuFour)&&contains(cardBixiuSix)&&(!contains(cardBixiuFive)))
                    return true;
                else
                    return false;
            case "BixiuSix":
                if(contains(cardBixiuOne)&&contains(cardBixiuTwo)&&contains(cardBixiuThree)
                    &&contains(cardBixiuFour)&&contains(cardBixiuFive)&&(!contains(cardBixiuSix)))
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    private bool isChi(CardDto card)
    {
        CardDto cardXuanxiuOne = new CardDto("XuanxiuOne", 2, 1);
        CardDto cardXuanxiuTwo = new CardDto("XuanxiuTwo", 2, 1);
        CardDto cardXuanxiuThree = new CardDto("XuanxiuThree", 2, 2);
        CardDto cardXuanxiuFour = new CardDto("XuanxiuFour", 2, 2);
        CardDto cardXuanxiuFive = new CardDto("XuanxiuFive", 2, 3);
        CardDto cardXuanxiuSix = new CardDto("XuanxiuSix", 2, 3);
        CardDto cardXuanxiuSeven = new CardDto("XuanxiuSeven", 2, 4);
        CardDto cardXuanxiuEight = new CardDto("XuanxiuEight", 2, 4);

        CardDto cardJianzhuOne = new CardDto("JianzhuOne", 3, 1);
        CardDto cardJianzhuTwo = new CardDto("JianzhuTwo", 3, 2);
        CardDto cardJianzhuThree = new CardDto("JianzhuThree", 3, 3);
        CardDto cardJianzhuFour = new CardDto("JianzhuFour", 3, 4);

        switch(card.Name){
            case "XuanxiuOne":
                bool flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuOne"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "XuanxiuTwo":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuOne"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "XuanxiuThree":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuTwo"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "XuanxiuFour":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuTwo"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "XuanxiuFive":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuThree"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "XuanxiuSix":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuThree"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "XuanxiuSeven":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuFour"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "XuanxiuEight":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("JianzhuFour"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "JianzhuOne":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("XuanxiuOne") || c.Name.Equals("XuanxiuTwo"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "JianzhuTwo":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("XuanxiuThree") || c.Name.Equals("XuanxiuFour"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "JianzhuThree":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("XuanxiuFive") || c.Name.Equals("XuanxiuSix"))
                    {
                        flag = true;
                    }
                }
                return flag;
            case "JianzhuFour":
                flag = false;
                for (int i = 0; i < cardDtoList.Count; i++)
                {
                    CardDto c = cardDtoList[i];
                    if (c.Name.Equals("XuanxiuOne") || c.Name.Equals("XuanxiuTwo"))
                    {
                        flag = true;
                    }
                }
                return flag;
            default:
                return false;

        }

    }
    
    private bool isHu(CardDto card)
    {
        cardDtoList.Add(card);
        if(isZimo()){
            cardDtoList.Remove(card);
            return true;
        }
        else {
            cardDtoList.Remove(card);
            return false;
        }
    }

    private bool isZimo()
    {
        List<CardDto> cardDtoListcp = new List<CardDto>();
        CardDto[] arr = cardDtoList.ToArray(); 
        for(int i = 0; i < cardDtoList.Count; i++){
            cardDtoListcp.Add(arr[i]);
        }
        bool flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++) {
            if(cardDtoListcp[i].Name == "BixiuOne") {
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Name == "BixiuTwo"){
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Name == "BixiuThree"){
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Name == "BixiuFour"){
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Name == "BixiuFive"){
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Name == "BixiuSix"){
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Color == 1)
                return false;
        }

        flag = false;
        CardDto xuanxiu1 = new CardDto();
        CardDto xuanxiu2 = new CardDto();
        CardDto jianzhu1 = new CardDto();
        CardDto jianzhu2 = new CardDto();

        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Color == 2){
                xuanxiu1 = cardDtoListcp[i];
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Color == 2){
                xuanxiu2 = cardDtoListcp[i];
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Color == 3){
                jianzhu1 = cardDtoListcp[i];
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        flag = false;
        for(int i = 0; i < cardDtoListcp.Count; i++){
            if(cardDtoListcp[i].Color == 3){
                jianzhu2 = cardDtoListcp[i];
                cardDtoListcp.RemoveAt(i);
                flag = true;
                break;
            }
        }
        if(!flag)
            return false;

        if(xuanxiu1.Weight == jianzhu1.Weight && xuanxiu2.Weight == jianzhu2.Weight)
            return true;
        if(xuanxiu1.Weight == jianzhu2.Weight && xuanxiu2.Weight == jianzhu1.Weight)
            return true;
        return false;
    }

    private bool contains2(CardDto card, List<CardDto> list)
    {
        for(int i = 0; i < list.Count; i++){
            CardDto c = list[i];
            if(c.Name.Equals(card.Name))
                return true;
        }
        return false;
    }


}
