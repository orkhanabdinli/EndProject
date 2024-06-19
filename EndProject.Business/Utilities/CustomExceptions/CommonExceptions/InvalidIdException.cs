using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Business.Utilities.CustomExceptions.CommonExceptions
{
    public class InvalidIdException : Exception
    {
        public int StatusCode { get; set; }
        public InvalidIdException() 
        {
        }
        public InvalidIdException(string message) : base(message)
        {
        }
        public InvalidIdException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
