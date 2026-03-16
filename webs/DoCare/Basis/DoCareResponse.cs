using FastFrame.Data;
using System.ComponentModel;

namespace DoCare
{
    public enum DoCareStatus
    {
        [Description("成功")]
        Success = 0000,
        [Description("失败")]
        Failure = -1,
    }

    /// <summary>
    /// 消息响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoCareResponse<T> : IResponse
    {
        private readonly string StatusOk = "0000";
        private readonly string StatusFailed = "-1";
        /// <summary>
        /// 状态码；成功:0000;失败:-1;
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        public string Message { get; set; }


        public T Data { get; set; }

        public bool Successed
        {
            get { return this.Status == StatusOk; }
        }

        public DoCareResponse() : this(DoCareStatus.Failure)
        { }

        public DoCareResponse(T data)
        {
            if (data != null)
            {
                this.Data = data;
                this.Status = StatusOk;
                this.Message = DoCareStatus.Success.ToDescription();
            }
        }

        public DoCareResponse(DoCareStatus status)
            : this(status, default(T)) { }

        public DoCareResponse(DoCareStatus status, T data)
            : this(status, status.ToDescription(), data) { }

        public DoCareResponse(DoCareStatus status, string msg, T data = default(T))
        {
            this.Status = status == DoCareStatus.Success ? StatusOk : StatusFailed;
            this.Message = msg;
            this.Data = data;
        }
    }
}
