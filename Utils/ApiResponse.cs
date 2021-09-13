using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShipsBackEnd.Utils
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Flag = Flag.Fail;
            Message = string.Empty;
            AllErrors = new HashSet<ModelError>();
        }

        public Flag Flag { get; set; }
        public string Message { get; set; }
        public IEnumerable<ModelError> AllErrors { get; set; }
        public object Result { get; set; }
        public int Code { get; set; }

    }
    [Flags]
    public enum Flag
    {
        Pass = 0x00,
        Fail = 0x01
    }
}
