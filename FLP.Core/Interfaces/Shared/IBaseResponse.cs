using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLP.Core.Interfaces.Shared;

public interface IBaseResponse<T> : IBaseResponse
{
    T? Data { get; set; }
}

public interface IBaseResponse
{
    IEnumerable<string>? Message { get; set; }
    bool IsSuccess { get; set; }
}
