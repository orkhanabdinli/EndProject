using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions
{
    public  class GenderNotFoundException : Exception
    {
        public int StatusCode { get; set; }
        public GenderNotFoundException()
        {
        }
        public GenderNotFoundException(string message) : base(message)
        {
        }
        public GenderNotFoundException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
