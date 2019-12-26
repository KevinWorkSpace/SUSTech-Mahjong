using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameServer.Cache.Fight;
using Protocol.Dto.Fight;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerCtrl : CharacterBase
{

    /// <summary>
    /// 自身管理的卡牌列表
    /// </summary>
    private List<CardDto> cardDtoList;

    private List<CardCtrl> cardCtrList;

    private Button btnEnter;

    private List<CardDto> list;

    /// <summary>
    /// 卡牌的父物体
    /// </summary>
    private Transform cardParent;

    private int count = 0;

    // Use this for initialization
    void Start()
    {
        LibraryModel lm = new LibraryModel();
        cardParent = transform.Find("CardPoint");
        cardDtoList = new List<CardDto>();
        cardCtrList = new List<CardCtrl>();
        list = lm.CardQueue.ToList();
        for (int i = 0; i < 9; i++)
        {
            cardDtoList.Add(list[0]);
            list.RemoveAt(0);
        }

        initCardList(cardDtoList);
    }

    /// <summary>
    /// 初始化显示卡牌
    /// </summary>
    private IEnumerator initCardList(List<CardDto> cardList)
    {
        //Debug.Log(cardList);
        cardDtoList.Sort();
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard");
        for (int i = 0; i < cardList.Count; i++)
        {
            createGo(cardList[i], i, cardPrefab);
            /*yield return new WaitForSeconds(0.1f);*/
        }

        return null;
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
        cardCtrl.Init(card, index, false);
        //存储本地
        cardCtrList.Add(cardCtrl);
    }
}