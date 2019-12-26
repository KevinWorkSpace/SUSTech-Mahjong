using Protocol.Dto.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Constant
{
    /// <summary>
    /// 卡牌权值
    /// </summary>
    public class CardWeight
    {
        public const int ONE = 1;
        public const int TWO = 2;
        public const int THREE = 3;
        public const int FOUR = 4;
 

        public static string GetString(int weight)
        {
            switch (weight)
            {
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                default:
                    throw new Exception("不存在这样的权值");
            }
        }

        /*/// <summary>
        /// 获取卡牌的权值
        /// </summary>
        /// <param name="cardList">选中的卡牌</param>
        /// <param name="cardType">出牌类型</param>
        /// <returns></returns>
        public static int GetWeight(List<CardDto> cardList, int cardType)
        {
            int totalWeight = 0;
            if (cardType == CardType.THREE_ONE || cardType == CardType.THREE_TWO)
            {
                //如果是 三代一 或者说 三代二
                // 3335  4443   5333  3335  3353
                for (int i = 0; i < cardList.Count - 2; i++)
                {
                    if (cardList[i].Weight == cardList[i + 1].Weight && cardList[i].Weight == cardList[i + 2].Weight)
                    {
                        totalWeight += (cardList[i].Weight * 3);
                    }
                }
            }
            else
            {
                for (int i = 0; i < cardList.Count; i++)
                {
                    totalWeight += cardList[i].Weight;
                }
            }
            return totalWeight;
        }*/

    }
}
