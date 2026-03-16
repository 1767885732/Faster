using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoCare.Mdsd.FirstAid.Entities
{
    public class VMPoctDetail
    {
        /// <summary>
        /// 检验项目
        /// </summary>
        public string LibName { get; set; }

        /// <summary>
        /// 检验项目（中文名）
        /// </summary>
        public string LibNameCn { get; set; }

        /// <summary>
        /// 检验值
        /// </summary>
        public string LibValue { get; set; }

        /// <summary>
        /// 参考范围
        /// </summary>
        public string ReferenceRange { get; set; }

        /// <summary>检验时间</summary>
        public string LibTime { get; set; }
    }
}
