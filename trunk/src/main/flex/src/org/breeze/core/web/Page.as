package org.breeze.core.web
{
	[Bindable]
	[RemoteClass(alias="org.breeze.core.web.Page")]
	public class Page
	{
		public var count:int=0;
		public var page:int=0;
		public var size:int=20;
		public var start:int=0;
	}
}