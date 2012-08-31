package org.breeze.flex.entity
{
	[Bindable]
	[RemoteClass(alias="org.breeze.entity.Clinic")]
	public class Clinic
	{
		public var clinicId:int;    
		public var hospitalId:int;    
		public var parentId:int;    
		public var code:String;    
		public var name:String;    
		public var introduction:String;    
		public var delFlag:int;    
		public var pyCode:String;    
		public var dCode:String;    
		public var outCode:String;    
		public var inCode:String;    
		public var version:Number;    
	}
}
