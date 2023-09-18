using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CaredCompTypeAttribute : Attribute
{
    public Type Type;

    public CaredCompTypeAttribute(Type type)
    {
        this.Type = type;
    }
}

