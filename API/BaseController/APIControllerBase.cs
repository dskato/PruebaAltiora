using Microsoft.AspNetCore.Mvc;

[ApiController]
public class APIControllerBase : ControllerBase
{
     public ResponseBase Response(object data = null, ResponseCode code = ResponseCode.OK)
    {
        return new ResponseBase()
        {
            Code = code,
            Data = data,
            CodeText = ResponseCodeText.OK.ToString()
        };
    }
}