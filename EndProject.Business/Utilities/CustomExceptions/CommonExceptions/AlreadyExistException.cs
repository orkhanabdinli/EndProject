using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Business.Utilities.CustomExceptions.CommonExceptions
{
    public class AlreadyExistException : Exception
    {
        public int StatusCode { get; set; }
        public AlreadyExistException()
        {
        }
        public AlreadyExistException(string message) : base(message)
        {
        }
        public AlreadyExistException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
