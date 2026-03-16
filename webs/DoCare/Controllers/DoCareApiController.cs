using FastFrame.AspNetCore.MVC;

namespace DoCare.Controllers
{
    public abstract class DoCareApiController : ApiController
    {
        protected DoCareResponse<T> Success<T>(T result = default(T))
        {
            return new DoCareResponse<T>(DoCareStatus.Success, result);
        }

        protected DoCareResponse<T> Error<T>()
        {
            return new DoCareResponse<T>(DoCareStatus.Failure);
        }

        protected DoCareResponse<T> Error<T>(string message)
        {
            return new DoCareResponse<T>(DoCareStatus.Failure, message);
        }
    }
}