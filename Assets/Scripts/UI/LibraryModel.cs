/*using Protocol.Constant;*/
using Protocol.Dto.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Constant;

namespace GameServer.Cache.Fight
{
    /// <summary>
    /// 牌库
    ///   什么？就是54张牌的地方
    /// </summary>
    public class LibraryModel
    {
        /// <summary>
        /// 所有牌的队列
        /// </summary>
        public Queue<CardDto> CardQueue { get; set; }

        public LibraryModel()
        {
            //创建牌
            create();
            //洗牌
            shuffle();
        }

        public void Init()
        {
            //创建牌
            create();
            //洗牌
            shuffle();
        }

        private void create()
        {
            CardQueue = new Queue<CardDto>();
            string[] cardName = new string[] {"BixiuOne", "BixiuTwo", "BixiuThree", "BixiuFour", "BixiuFive", "BixiuSix", "XuanxiuOne", "XuanxiuTwo", "XuanxiuThree", "XuanxiuFour", "XuanxiuFive", "XuanxiuSix", "XuanxiuSeven", "XuanxiuEight", "JianzhuOne", "JianzhuTwo", "JianzhuThree", "JianzhuFour"};
            int[] cardColor = new int[] {1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3};
            int[] cardWeight = new int[] {0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 1, 2, 3, 4};

            //创建普通的牌
            for(int i = 0; i < 6; i++){
                for (int j = 0; j < 8; j++)
                {
                    CardDto card = new CardDto(cardName[i],cardColor[i], cardWeight[i]);
                    //添加到 CardQueue  里面
                    CardQueue.Enqueue(card);
                }
            }

            for(int i = 6; i < 18; i++){
                for(int j = 0; j < 6; j++)
                {
                    CardDto card = new CardDto(cardName[i], cardColor[i], cardWeight[i]);
                    //添加到 CardQueue  里面
                    CardQueue.Enqueue(card);
                }
            }

            /*//大王小王
            CardDto sJoker = new CardDto("SJoker", CardColor.NONE, CardWeight.SJOKER);
            CardDto lJoker = new CardDto("LJoker", CardColor.NONE, CardWeight.LJOKER);
            CardQueue.Enqueue(sJoker);
            CardQueue.Enqueue(lJoker);*/
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        private void shuffle()
        {
            List<CardDto> newList = new List<CardDto>();
            Random r = new Random();
            // 1 2 3 4 5 6 7
            foreach (CardDto card in CardQueue)
            {
                int index = r.Next(0, newList.Count + 1);
                // 6 2 5 4 3 7 1...
                newList.Insert(index, card);
            }
            CardQueue.Clear();
            foreach (CardDto card in newList)
            {
                CardQueue.Enqueue(card);
            }
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <returns></returns>
        public CardDto Deal()
        {
            return CardQueue.Dequeue();
        }

    }
}
