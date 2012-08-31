package org.breeze.core.web
{
	import flash.events.Event;
	import org.breeze.flex.entity.Clinic;

	public class FormEvent extends Event
	{
		public static const CREATED:String = "Created";
		public static const UPDATED:String = "Updated";
		public static const DELETED:String = "Deleted";
	
		public var object:Object;
		
		public function FormEvent(type:String, contact:Clinic, bubbles:Boolean = true, cancelable:Boolean = false)
   		{
   			this.object = object;
			super(type, bubbles, cancelable);
		}
	}
}