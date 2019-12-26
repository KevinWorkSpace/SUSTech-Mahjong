using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Constant
{
    /// <summary>
    /// 卡牌花色
    /// </summary>
    public class CardColor
    {
        public const int NONE = 0;
        public const int BIXIU = 1;//必修
        public const int XUANXIU = 2;//选修
        public const int JIANZHU = 3;//建筑

        public static string GetString(int color)
        {
            switch (color)
            {
                case BIXIU:
                    return "Bixiu";
                case XUANXIU:
                    return "Xuanxiu";
                case JIANZHU:
                    return "Jianzhu";
                default:
                    throw new Exception("不存在这样的花色");
            }
        }
    }
}
