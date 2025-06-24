using FLP.Core.Interfaces.Shared;

namespace FLP.Core.Context.Shared;

public record BaseResponse<T> : BaseResponse, IBaseResponse<T>
    where T : class
{
    /// <summary>
    /// success response constructor
    /// </summary>
    /// <param name="data"></param>
    public BaseResponse(T data) : base()
    {
        Data = data;
    }
    /// <summary>
    /// fail response constructor
    /// </summary>
    /// <param name="message"></param>
    public BaseResponse(bool isSuccess = true, params IEnumerable<string> message) : base(isSuccess, message)
    {
        Data = default;
    }
    public T? Data { get; set; }
}

public record BaseResponse : IBaseResponse
{
    /// <summary>
    /// success response constructor
    /// </summary>
    public BaseResponse(bool isSuccess = true)
    {
        IsSuccess = isSuccess;
        Message = [];
    }
    /// <summary>
    /// fail response constructor
    /// </summary>
    /// <param name="message">fail message</param>
    public BaseResponse(bool isSuccess, params IEnumerable<string> message)
    {
        IsSuccess = isSuccess;
        Message = message ?? [];
    }
    public IEnumerable<string>? Message { get; set; }
    public bool IsSuccess { get; set; }
}
