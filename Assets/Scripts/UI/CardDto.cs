using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Protocol.Dto.Fight
{
    /// <summary>
    /// 表示卡牌
    /// </summary>
    [Serializable]
    public class CardDto : IComparable<CardDto>
    {
        public string Name;
        public int Color;//必修，选修，建筑
        public int Weight;//相同的weight表示同一组

        public CardDto()
        {

        }

        public CardDto(string name, int color, int weight)
        {
            this.Name = name;
            this.Color = color;
            this.Weight = weight;
        }
        

        public int CompareTo(CardDto other)
        {
            if (this.Color.CompareTo(other.Color) == 0)
            {
                return this.Name.CompareTo(other.Name);
            }
            return this.Color.CompareTo(other.Color);
        }
        
        public bool Equals(CardDto c)
        {
            //按需求定制自己需要的比较方式
            return (this.Name.Equals(c.Name));
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode(); 
        }
    }
}
