using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.Service.Exceptions;

public class CustomException : Exception
{
    public CustomException() { }

    public CustomException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public CustomException(string message, int statusCode, Exception exception)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}