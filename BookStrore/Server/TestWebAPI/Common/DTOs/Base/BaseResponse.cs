using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.DTOs.Base
{
    public class BaseResponse
    {
        public bool IsSucced { get; set; }
        public int Status { get; set; }
    }
}
