using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.Service.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException() { }

    public AlreadyExistException(string message) : base(message) { }

    public AlreadyExistException(string message, Exception exception) { }

    public int StatusCode => 409;
}