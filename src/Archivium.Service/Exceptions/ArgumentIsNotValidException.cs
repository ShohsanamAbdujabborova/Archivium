using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.Service.Exceptions;

public class ArgumentIsNotValidException : Exception
{
    public ArgumentIsNotValidException() { }

    public ArgumentIsNotValidException(string message) : base(message) { }

    public ArgumentIsNotValidException(string message, Exception exception) { }

    public int StatusCode => 400;
}