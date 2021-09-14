using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolymorphismExmaple
{
    class ChessException : Exception
    {
            public ChessException() { }
            public ChessException(string message) : base(message) { }   
            public ChessException(string message, Exception inner) : base(message, inner) { }

    }
}
