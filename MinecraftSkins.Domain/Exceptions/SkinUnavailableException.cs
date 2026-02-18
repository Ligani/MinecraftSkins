using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Domain.Exceptions
{
    public class UnavailableException : Exception
    {
        public UnavailableException(string message) : base(message) { }
    }
}
