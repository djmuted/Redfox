using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Users.UserVariables
{
    public abstract class IUserVariable
    {
        public string Name { get; protected set; }
        public bool IsPrivate { get; protected set; }
        public abstract override string ToString();
    }
}
