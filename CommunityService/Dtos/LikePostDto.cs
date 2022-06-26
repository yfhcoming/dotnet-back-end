using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityService.Dtos
{
    public class LikePostDto : IComparable<LikePostDto>
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int LikeNum { get; set; }

        public int CompareTo(LikePostDto other)
        {
            if (null == other)
            {
                return 0;//空值比较小，返回0
            }
            return other.LikeNum.CompareTo(this.LikeNum);//降序
        }
    }
}
