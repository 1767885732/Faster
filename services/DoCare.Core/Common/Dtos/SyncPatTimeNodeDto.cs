using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Core.Common.Dtos
{
    /// <summary>
    /// 推送结果说明
    /// </summary>
    public class SyncPatTimeNodeDto
    {
        public string type { get; set; }
        public long time { get; set; }

        public SyncPatTimeNodeItem data { get; set; }
    }
    public class SyncPatTimeNodeItem
    {
        /// <summary>
        /// 电子围栏ID
        /// </summary>
        public string railId { get; set; }
        /// <summary>
        /// 用户 ID 即定位标签的 MAC 地址
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 围栏名称
        /// </summary>
        public string railName { get; set; }
        /// <summary>
        /// 事件类型，1 进入围栏，2 离开围栏
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 建筑 ID
        /// </summary>
        public string buildId { get; set; }
        /// <summary>
        /// 楼层号
        /// </summary>
        public string floorNo { get; set; }
    }
}
