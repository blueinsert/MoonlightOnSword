using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ExecutableEventAttribute : Attribute
{
    public Type ExecuteClassType;
    public ExecutableEventAttribute(Type executeClassType)
    {
        ExecuteClassType = executeClassType;
    }
}

