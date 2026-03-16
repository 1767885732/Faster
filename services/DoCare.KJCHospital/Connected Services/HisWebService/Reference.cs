//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace HisWebService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="HisWebService.PUB0020Soap")]
    public interface PUB0020Soap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DHC.Published.PUB0020.BS.PUB0020.HIPMessageServer", ReplyAction="*")]
        System.Threading.Tasks.Task<HisWebService.HIPMessageServerResponse> HIPMessageServerAsync(HisWebService.HIPMessageServerRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HIPMessageServerRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HIPMessageServer", Namespace="http://tempuri.org", Order=0)]
        public HisWebService.HIPMessageServerRequestBody Body;
        
        public HIPMessageServerRequest()
        {
        }
        
        public HIPMessageServerRequest(HisWebService.HIPMessageServerRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class HIPMessageServerRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string input1;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string input2;
        
        public HIPMessageServerRequestBody()
        {
        }
        
        public HIPMessageServerRequestBody(string input1, string input2)
        {
            this.input1 = input1;
            this.input2 = input2;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HIPMessageServerResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HIPMessageServerResponse", Namespace="http://tempuri.org", Order=0)]
        public HisWebService.HIPMessageServerResponseBody Body;
        
        public HIPMessageServerResponse()
        {
        }
        
        public HIPMessageServerResponse(HisWebService.HIPMessageServerResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class HIPMessageServerResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HIPMessageServerResult;
        
        public HIPMessageServerResponseBody()
        {
        }
        
        public HIPMessageServerResponseBody(string HIPMessageServerResult)
        {
            this.HIPMessageServerResult = HIPMessageServerResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface PUB0020SoapChannel : HisWebService.PUB0020Soap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class PUB0020SoapClient : System.ServiceModel.ClientBase<HisWebService.PUB0020Soap>, HisWebService.PUB0020Soap
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public PUB0020SoapClient() : 
                base(PUB0020SoapClient.GetDefaultBinding(), PUB0020SoapClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.PUB0020Soap.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PUB0020SoapClient(EndpointConfiguration endpointConfiguration) : 
                base(PUB0020SoapClient.GetBindingForEndpoint(endpointConfiguration), PUB0020SoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PUB0020SoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(PUB0020SoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PUB0020SoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(PUB0020SoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PUB0020SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<HisWebService.HIPMessageServerResponse> HisWebService.PUB0020Soap.HIPMessageServerAsync(HisWebService.HIPMessageServerRequest request)
        {
            return base.Channel.HIPMessageServerAsync(request);
        }
        
        public System.Threading.Tasks.Task<HisWebService.HIPMessageServerResponse> HIPMessageServerAsync(string input1, string input2)
        {
            HisWebService.HIPMessageServerRequest inValue = new HisWebService.HIPMessageServerRequest();
            inValue.Body = new HisWebService.HIPMessageServerRequestBody();
            inValue.Body.input1 = input1;
            inValue.Body.input2 = input2;
            return ((HisWebService.PUB0020Soap)(this)).HIPMessageServerAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PUB0020Soap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PUB0020Soap))
            {
                return new System.ServiceModel.EndpointAddress("http://192.168.0.61/csp/hsb/DHC.Published.PUB0020.BS.PUB0020.cls");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return PUB0020SoapClient.GetBindingForEndpoint(EndpointConfiguration.PUB0020Soap);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return PUB0020SoapClient.GetEndpointAddress(EndpointConfiguration.PUB0020Soap);
        }
        
        public enum EndpointConfiguration
        {
            
            PUB0020Soap,
        }
    }
}
