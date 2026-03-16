using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.Mdsd.FirstAid.Entities
{
    public class VMPoctMaster
    {
        public VMPoctMaster()
        {
            Details = new List<VMPoctDetail>();
        }
        /// <summary>
        /// 检验编号
        /// </summary>
        public string LibId { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        public string LibTime { get; set; }

        public List<VMPoctDetail> Details;

    }
}
