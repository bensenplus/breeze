package org.breeze.core.web
{
	import flash.events.Event;

	public class FormEvent extends Event
	{
		public static const CREATED:String = "Created";
		public static const UPDATED:String = "Updated";
		public static const REMOVED:String = "Removed";
	
		public var object:Object;
		
		public function FormEvent(type:String, contact:Object, bubbles:Boolean = true, cancelable:Boolean = false)
   		{
   			this.object = object;
			super(type, bubbles, cancelable);
		}
	}
}