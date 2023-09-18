using System;
using System.Collections;
using System.Collections.Generic;

//todo
public class ForeignKeyAttribute : Attribute {

    public string m_tableName;
    public string m_columeName;

	public ForeignKeyAttribute(string tableName, string columeName)
    {
        m_tableName = tableName;
        m_columeName = columeName;
    }

}
