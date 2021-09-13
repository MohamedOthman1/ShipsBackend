using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsBackEnd.Utils
{
    public interface ILogHelper
    {
        void ErrorLogger(string message, Exception ex);
    }
}
