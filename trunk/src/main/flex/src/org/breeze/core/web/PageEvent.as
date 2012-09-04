package org.breeze.core.web
{
	import flash.events.Event;

	public class PageEvent extends Event
	{
		public static const PAGED:String = "PAGED";
	
		public var page:Page;
		
		public function PageEvent(type:String, page:Page, bubbles:Boolean = true, cancelable:Boolean = false)
   		{
   			this.page = page;
			super(type, bubbles, cancelable);
		}
	}
}