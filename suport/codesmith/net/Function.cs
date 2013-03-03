
public void SafeCopyFileTo(string path, string destination)
{
	if(Directory.Exists(destination) == false)
    {
        Directory.CreateDirectory(destination);
    }
	System.IO.FileInfo file1 = new System.IO.FileInfo(path);
	file1.CopyTo(destination +"/"+ file1.Name, true);
}

public void SafeCopyFile(string path, string destination)
{
	System.IO.FileInfo file1 = new System.IO.FileInfo(path);
	file1.CopyTo(destination, true);
}

public string getValidate(ColumnSchema column){
    
    string required = "required:"+ (column.AllowDBNull?"false":"true");
    string maxlength = "maxlength:" + column.Size;
    string result = " validate=\"{"+required+","+maxlength+"}\"";
    if(column.Size ==0){
        result = " validate=\"{"+required+", digits:true}\"";
    }
   if (column.DataType.ToString().Equals("DateTime")) { 
       result = " validate=\"{"+required+", date:true}\"";
   }
    return result;
}

	
#region Naming 

public string ClassName( string TableName)
{
	string classname = StringUtil.ToPascalCase(TableName.ToLower());
	
	return classname;
}

public string ModelName( string TableName)
{
	return ClassName(TableName);
}

public string DaoName( string TableName)
{
	return ClassName(TableName) + "Mapper";
}

public string RepositoryName( string TableName)
{
	return ClassName(TableName) + "Repository";
}

public string ControllerName( string TableName)
{
	return ClassName(TableName) + "Controller";
}

public string ServiceName( string TableName)
{
	return ClassName(TableName) + "Service";
}

public string fieldName(string FieldName)
{
     if(FieldName == "NATIVE") return "_native";
	 return StringUtil.ToCamelCase(FieldName.ToLower());
}

public string getter(string FieldName)
{
	return StringUtil.ToCamelCase("get_"+FieldName.ToLower());
}

public string setter( string FieldName)
{
	return StringUtil.ToCamelCase("set_" +FieldName.ToLower());
}

#endregion

#region get param string 

public string GetEtParam(TableSchema dt )
{
	string param = string.Empty;
	
	int count = 0;
	foreach (ColumnSchema column in dt.Columns) 
	{ 
		if ( count == 0 )
		{

			param = column.Name ;
		}
		else
		{
			param = param + ",\n\t\t" + column.Name;
		}
		count = count + 1;
	}
	
	return param;
}


public string JDBCType(string type){
    
    if(type =="NUMBER") return "NUMERIC";
    
    if(type == "VARCHAR2") return "VARCHAR";
    
    return type;
   
}

public string GetTableFiled(TableSchema dt )
{
	string param = string.Empty;	
	int count = 0;
	foreach (ColumnSchema column in dt.Columns) 
	{ 		
		if ( count == 0 )
		{
			param = column.Name ;
		}
		else
		{
			param = param + ",\n\t\t" + column.Name;
		}
		count = count + 1;
	}
	
	return param;
}

public string parameterType(TableSchema dt)
{
	string param = string.Empty;
	int count = 0;
	
	if(!dt.HasPrimaryKey) {
		param = "parameterType=\""+ ModelName(dt.Name) +"\"";
		return param;
	}
	
	foreach (ColumnSchema column in dt.PrimaryKey.MemberColumns) 
	{ 
		if ( count == 0 )
		{
			param = "parameterType=\""+CSharpAlias[column.SystemType.FullName]+"\"";
		}
		else
		{
			param = "parameterType=\""+ ModelName(dt.Name) +"\"";
			break;
		}
		count = count + 1;
	}
	
	return param;
}

public string parameterSQL(TableSchema dt)
{
	string param = string.Empty;	
	int count = 0;
	
	if(!dt.HasPrimaryKey) {

		param = dt.Columns[0].Name +" = #{"+ StringUtil.ToCamelCase(dt.Columns[0].Name.ToLower())+"}";

		return param;
	}
	
	foreach (ColumnSchema column in dt.PrimaryKey.MemberColumns) 
	{ 
		
		if ( count == 0 )
		{
			param = column.Name +" = #{"+ StringUtil.ToCamelCase(column.Name.ToLower())+"}";
		}
		else
		{
			param = param + " AND " + column.Name +" = #{"+ StringUtil.ToCamelCase(column.Name.ToLower())+"}";
		}
		count = count + 1;
	}
	
	return param;
}

public string parameterURL(TableSchema dt)
{

	string param = string.Empty;	
	int count = 0;
	string  instance = StringUtil.ToCamelCase(dt.Name.ToLower());
	
	if(!dt.HasPrimaryKey) {
		string  strCol = StringUtil.ToCamelCase(dt.Columns[0].Name.ToLower());
		param = strCol +"=${"+ instance + "." + strCol+"}";

		return param;
	}
	

	
	foreach (ColumnSchema column in dt.PrimaryKey.MemberColumns) 
	{ 
		string  strCol = StringUtil.ToCamelCase(column.Name.ToLower());
		
		if ( count == 0 )
		{
			param = strCol +"=${"+ instance + "." + strCol+"}";
		}
		else
		{
			param = param + "&" + strCol +"=${"+ instance + "." + strCol+"}";
		}
		count = count + 1;
	}
	
	return param;
}

public string GetInsertParam(TableSchema dt )
{
	string param = string.Empty;
	int count = 0;
	foreach (ColumnSchema column in dt.Columns) 
	{ 
        string temp= string.Empty;
         if(JDBCType(column.NativeType) =="DATE" && fieldName(column.Name).StartsWith("create")){
             temp = "sysdate";
         }else{
		    temp = "#{" + fieldName(column.Name) +",jdbcType="+JDBCType(column.NativeType)+"}";
         }

        param = (count==0?temp:(param + ",\n\t\t" + temp));
		count = count + 1;
	}
	return param;
}

public string GetUpdateString(TableSchema dt )
{
	string param = string.Empty;	
	int count = 0;
	foreach (ColumnSchema column in dt.Columns) 
	{ 
       string temp= string.Empty;
       if(JDBCType(column.NativeType) =="DATE"){
            if(fieldName(column.Name).StartsWith("create")){
                continue;
            }else if(fieldName(column.Name).StartsWith("update")){
                temp = column.Name + "= sysdate ";
            }else{
                temp = column.Name + "=#{" + fieldName(column.Name) +",jdbcType="+JDBCType(column.NativeType)+"}";
            }
        }else{
            temp = column.Name + "=#{" + fieldName(column.Name) +",jdbcType="+JDBCType(column.NativeType)+"}";
        }
		
		param = (count==0?temp:(param + ",\n\t\t" + temp));
		count = count + 1;
	}
	
	return param;
}

#endregion

#region Keys 

public string GetKeys(TableSchema dt, bool hasType)
{
	string param = string.Empty;	
	string  instance = StringUtil.ToCamelCase(dt.Name.ToLower());
	int count = 0;
	
	if(!dt.HasPrimaryKey) {
		if(hasType){
			param = ModelName(dt.Name) + " p_" + instance;
		}else{
			param = "p_"+instance;
		}
		return param;
	}

	foreach (ColumnSchema column in dt.PrimaryKey.MemberColumns) 
	{ 
		
		if (count == 0)
		{
			if(hasType){
				param = CSharpAlias[column.SystemType.FullName] + " " + StringUtil.ToCamelCase(column.Name.ToLower());
			}else{
				param = StringUtil.ToCamelCase(column.Name.ToLower());
			}
		}
		else
		{
			if(hasType){
				param = ModelName(dt.Name) + " p_" + instance;
			}else{
				param = "p_"+instance;
			}
			break;
		}
		count = count + 1;
	}
	
	return param;
}


public string KeyType(TableSchema table)
{
    string KeyType = CSharpAlias[table.Columns[0].SystemType.FullName];
    return KeyType;
}

public string KeyColumn(TableSchema table)
{
    string KeyColumn = fieldName(table.Columns[0].Name);
    return KeyColumn;
}

public string KeyParam(TableSchema table)
{
    string KeyType = CSharpAlias[table.Columns[0].SystemType.FullName];
    string KeyColumn = fieldName(table.Columns[0].Name);
    return KeyType + " " + KeyColumn;
}



public string GetCallKeysParam(TableSchema dt, string instance)
{
	string param = string.Empty;	
	int count = 0;	
	if(!dt.HasPrimaryKey) {
		return instance;
	}

	foreach (ColumnSchema column in dt.PrimaryKey.MemberColumns) 
	{ 
		
		if ( count == 0 )
		{
			param = instance + "." + fieldName(column.Name);

		}
		else
		{
		    param = param +", " + instance + "." + getter(column.Name);
		}
		count = count + 1;
	}
	
	return param;
}

#endregion

public  DataTable LoadDataFromExcel(string filePath, string sheetName)  
{  
    try  
    {  
        string strConn;  
        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";  
        OleDbConnection OleConn = new OleDbConnection(strConn);  
        OleConn.Open();  
        String sql = "SELECT * FROM  ["+sheetName+"$]";//可是更改Sheet名称，比如sheet2，等等   
  
        OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);  
        DataSet dataset = new DataSet();  
        OleDaExcel.Fill(dataset, sheetName);  
        OleConn.Close();  
        return dataset.Tables[sheetName];  
    }  
    catch (Exception err)  
    { 
         Response.WriteLine(err.Message);
        return null;  
    }  
} 

private void PrintValues(DataTable table)
{
    
     for (int i = 0; i < table.Columns.Count; i++)
     {
         Response.Write(table.Columns[i].ColumnName +"\t");
     }
 
    foreach(DataRow row in table.Rows)
    {
        Response.WriteLine();
        foreach(DataColumn column in table.Columns)
        {
            Response.Write(row[column]+"\t");
        }
        
    }
}

private void print(object val){
    Response.Write(val);
}

private void println(object val){
    Response.WriteLine(val);
}
