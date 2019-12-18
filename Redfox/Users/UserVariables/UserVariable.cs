using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Users.UserVariables
{
    public class UserVariable<T> : IUserVariable
    {
        public T Value { get; private set; }
        public UserVariable(string name, T val, bool _isprivate)
        {
            this.Name = name;
            this.IsPrivate = _isprivate;
            SetValue(val);
        }
        public void SetValue(T val)
        {
            Value = val;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
