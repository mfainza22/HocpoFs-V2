
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeighingSystemCoreHelpers.Models
{
    public class ResponseResult
    {

        public ResponseResult(HttpResponse _response)
        {
            response = _response;
            this.StatusCode = (int)StatusCodes.Status500InternalServerError;
            this.ResponseObject = null;
        }

        [JsonIgnore]
        private HttpResponse response { get; set; }

        private int _statusCode;
        public int StatusCode
        {
            get { return _statusCode; }
            set
            {
                _statusCode = value;
                response.StatusCode = _statusCode;
            }
        }
        public string ErrorMessage { get; set; }
        public object ResponseObject { get; set; }

        public JsonResult ToJsonResult()
        {
            string defErrMsg = null;

            switch (this.StatusCode)
            {
                case (int)StatusCodes.Status200OK:
                    defErrMsg = "OK";
                    break;
                case (int)StatusCodes.Status201Created:
                    defErrMsg = "CREATED";
                    break;
                case (int)StatusCodes.Status202Accepted:
                    defErrMsg = "ACCEPTED";
                    break;
                case (int)StatusCodes.Status400BadRequest:
                    defErrMsg = "BAD REQUEST";
                    break;
                case (int)StatusCodes.Status401Unauthorized:
                    defErrMsg = "UNAUTHORIZED";
                    break;
                case (int)StatusCodes.Status403Forbidden:
                    defErrMsg = "FORBIDDEN";
                    break;
                case (int)StatusCodes.Status404NotFound:
                    defErrMsg = "NOT FOUND";
                    break;
                case (int)StatusCodes.Status406NotAcceptable:
                    defErrMsg = "NOT ACCEPTABLE";
                    break;
                case (int)StatusCodes.Status408RequestTimeout:
                    defErrMsg = "REQUEST TIMEOUT";
                    break;
                case (int)StatusCodes.Status405MethodNotAllowed:
                    defErrMsg = "METHOD NOT ALLOWED";
                    break;
                case (int)StatusCodes.Status419AuthenticationTimeout:
                    defErrMsg = "AUTHENTICATION TIMEOUT";
                    break;
                case (int)StatusCodes.Status500InternalServerError:
                    defErrMsg = "INTERNAL SERVER ERROR.";
                    break;
                case (int)StatusCodes.Status502BadGateway:
                    defErrMsg = "BAD GATEWAY";
                    break;

            }
            this.ErrorMessage = string.IsNullOrEmpty(this.ErrorMessage) ? defErrMsg : this.ErrorMessage;

            var json = JsonConvert.SerializeObject(this);
            return new JsonResult(json);
        }

    }
}