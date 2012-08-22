/*
RootOutputPath:	输出主目录
ModulName:	模块名
ActionName:动作
Filename:文件名.java

目录构造：输出主目录/模块名/动作/文件名.java


处理逻辑：
首先逐层判断 目录是否存在,不存在的话，创建目录
*/
public string GetFileName(string RootOutputPath,string packageName, string  ActionName,string Filename,string FileExtType )
{
    string ModulName = packageName.Replace(".",@"\");
	string RootOutputPathFormat = @"{0}";
	string ModulNamePathFormat = @"{0}\{1}";
	string ActionNamePathFormat = @"{0}\{1}\{2}";
	string FilenameFormat = @"{0}\{1}\{2}\{3}.{4}";
	
	string FileName = "";

	
	string tmpPath = "";
    /// 1. 判断主路径是否存在,不存在创建主路径
	tmpPath = string.Format(RootOutputPathFormat,RootOutputPath);
    
	/// 判断主路径是否存在
    if(Directory.Exists(tmpPath) == false)
    {
        Directory.CreateDirectory(tmpPath);
    }
	
	/// 2 判断主路径下面的子路径是否存在
	tmpPath = string.Format(ModulNamePathFormat,RootOutputPath,ModulName);
    
	/// 判断主路径是否存在
    if(Directory.Exists(tmpPath) == false)
    {
        Directory.CreateDirectory(tmpPath);
    }
	
	/// 3 判断主路径下面的子路径的模块是否存在
	tmpPath = string.Format(ActionNamePathFormat,RootOutputPath,ModulName,ActionName);
    
	/// 判断主路径是否存在
    if(Directory.Exists(tmpPath) == false)
    {
        Directory.CreateDirectory(tmpPath);
    }
	
	/// 返回相应的文件名
	FileName = 	string.Format(FilenameFormat,RootOutputPath,ModulName,ActionName,Filename,FileExtType);
    Response.WriteLine("Creating:" + FileName);
	return FileName;
}
	
#region Naming 

public string ClassName( string TableName)
{
	string classname = StringUtil.ToPascalCase(TableName.ToLower());
	
	return classname;
}

/// 输出 Model 的类名
public string ModelName( string TableName)
{
	return ClassName(TableName);
}

/// 输出 Dao 的类名, 通用类名+DAOImpl
public string DaoName( string TableName)
{
	return ClassName(TableName) + "Mapper";
}

/// 输出Controller 的类名 通用类名+Controller
public string ControllerName( string TableName)
{
	return ClassName(TableName) + "Controller";
}


/// 输出Service 的类名 通用类名+Service
public string ServiceName( string TableName)
{
	return ClassName(TableName) + "Service";
}


/// 输出 Manager 的类名 通用类名+ManagerImpl
public string IntanceName( string TableName)
{	
	string tname = ClassName(TableName);
	
	string intanceName = tname.Substring(0,1).ToLower() +  tname.Substring(1);
	
	return intanceName;
}


/// 通过 字段名字 得到 通用的字段名字 ，最后以小写存在；例：  COM_ID   com_id
public string JspName( string TableName)
{
	string tname = ClassName(TableName);
	
	return tname.ToLower();
}

/// 通过 字段名字 得到 通用的字段名字 ，最后以小写存在；例：  COM_ID   com_id
public string JspNameList( string TableName)
{
	string tname = ClassName(TableName)+"list";
	
	return tname.ToLower();
}


/// 通过 字段名字 得到 通用JSP页面  ，最后以小写存在；例：  COM_ID   com_id
/// 

public string JspNameInit( string TableName)
{
	string tname = ClassName(TableName)+"init";
	
	return tname.ToLower();
}

public string JspNameInitAdd( string TableName)
{
	string tname = ClassName(TableName)+"initadd";
	
	return tname.ToLower();
}

public string JspNameInitUpdate( string TableName)
{
	string tname = ClassName(TableName)+"initupdate";
	
	return tname.ToLower();
}

public string JspNameAdd( string TableName)
{
	string tname = ClassName(TableName)+"add";
	
	return tname.ToLower();
}

public string JspNameUpdate( string TableName)
{
	string tname = ClassName(TableName)+"update";
	
	return tname.ToLower();
}

public string JspNameRelaList( string TableName)
{
	string tname = ClassName(TableName)+"relalist";
	
	return tname.ToLower();
}

/// 通过 字段名字 得到 通用JSP 关页面  ，最后以小写存在；例：  COM_ID   com_id
public string JspNameRelaMgr( string TableName)
{
	string tname = ClassName(TableName)+"relamgr";
	
	return tname.ToLower();
}


#endregion


public string fieldName(string FieldName)
{
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

public string GetFileName(string ClassName)
{
	return ClassName;
}

public string FormatDesc( string description )
{
	description = description.Replace("\r",string.Empty);
	
	description = description.Replace("\n",string.Empty);
	
	return description;
}

///
///
///将*改成   各个字段 
public string GetEtParam(TableSchema dt )
{
	string param = string.Empty;
	
	int count = 0;
	/// {0} 变量 
	//string Format = "{0}";
	foreach (ColumnSchema column in dt.Columns) 
	{ 
		if ( count == 0 )
		{

			param = column.Name ;
		}
		else
		{
			param = param + "," + column.Name;
		}
		count = count + 1;
	}
	
	return param;
}

//根据采集数据的字段来
public string GetEtParamSrc(TableSchema dt )
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
			param = param + "," + column.Name;
		}
		count = count + 1;
	}
	
	return param;
}



///
///
///将*改成   各个字段 
//根据采集数据的字段来  ?,?,?,?,?,?,?,?,?,?
public string GetEtParamAsk(TableSchema dt )
{
	string param = string.Empty;
	int count = 0;
	/// {0} 变量 
	foreach (ColumnSchema column in dt.Columns) 
	{ 
		if ( count == 0 )
		{
			param = "#{" + fieldName(column.Name) + "}";
		}
		else
		{
			param = param + ",#{" + fieldName(column.Name) + "}";
		}
		count = count + 1;
	}
	return param;
}


//根据采集数据的字段来
public string GetUpdateString(TableSchema dt )
{
	string param = string.Empty;	
	int count = 0;
	foreach (ColumnSchema column in dt.Columns) 
	{ 
		
		if ( count == 0 )
		{

			param = column.Name + "=#{" + fieldName(column.Name) + "}";
		}
		else
		{
			param = param + "," + column.Name + "=#{" + fieldName(column.Name) + "}";
		}
		count = count + 1;
	}
	
	return param;
}



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
		
		if ( count == 0 )
		{
			if(hasType){
				param = JavaAlias[column.SystemType.FullName] + " " + StringUtil.ToCamelCase(column.Name.ToLower());
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
			param = "parameterType=\""+JavaAlias[column.SystemType.FullName]+"\"";
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

#endregion


