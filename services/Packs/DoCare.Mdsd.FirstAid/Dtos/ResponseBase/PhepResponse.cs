namespace DoCare.Mdsd.FirstAid.Dtos
{
    public sealed class PhepResponse<TValue>
    {
        private string m_message;
        private TValue m_value;
        private string m_resultCode;
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="value">数据对象</param>
        /// <returns></returns>
        public static PhepResponse<TValue> Success(TValue value)
        {
            return new PhepResponse<TValue>(value);
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static PhepResponse<TValue> Fail(string message)
        {
            return new PhepResponse<TValue>(message);
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="value">数据对象</param>
        /// <returns></returns>
        public static PhepResponse<TValue> Fail(string message, TValue value)
        {
            return new PhepResponse<TValue>(false, message, value);
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="message">消息</param>
        /// <param name="value">数据对象</param>
        private PhepResponse(bool isSuccess, string message, TValue value)
        {
            if (isSuccess)
                m_resultCode = "1000";
            else
                m_resultCode = "2000";
            m_message = message ?? string.Empty;
            m_value = value;
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="value">数据对象</param>
        private PhepResponse(TValue value)
            : this(true, string.Empty, value)
        {
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="message">消息</param>
        private PhepResponse(string message)
            : this(false, message, default(TValue))
        {
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        public PhepResponse()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// 返回代码
        /// </summary>
        public string resultCode
        {
            get
            {
                return m_resultCode;
            }
            set
            {
                m_resultCode = value;
            }
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string message
        {
            get { return m_message ?? string.Empty; }
            set { m_message = value; }
        }

        /// <summary>
        /// 数据对象
        /// </summary>
        public TValue data
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
