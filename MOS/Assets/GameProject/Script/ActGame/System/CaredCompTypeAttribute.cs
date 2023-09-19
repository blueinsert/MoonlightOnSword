using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CaredCompTypeAttribute : Attribute
{
    public List<Type> Types = new List<Type>();

    public CaredCompTypeAttribute(List<Type> type)
    {
        this.Types.Clear();
        this.Types.AddRange(type);
    }

    public CaredCompTypeAttribute(params Type[] types)
    {
        this.Types.Clear();
        this.Types.AddRange(types);
    }
}

