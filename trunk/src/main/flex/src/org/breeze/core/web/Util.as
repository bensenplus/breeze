package org.breeze.core.web
{
	import mx.controls.dataGridClasses.DataGridColumn;
	
	import spark.formatters.DateTimeFormatter;
	
	public class Util
	{
		private static var dtf:DateTimeFormatter = new DateTimeFormatter();
		
		{
			dtf.setStyle("locale", "zh-CN");
		}
		
		public static function formatDate(item:Object,column:DataGridColumn):String{
			
			return dtf.format(item[column.dataField.toString()]);			
		}
	}
}