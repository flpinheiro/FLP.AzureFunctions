using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLP.Core.Interfaces.Shared;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
}
