package org.breeze.flex.entity
{
	[Bindable]
	[RemoteClass(alias="org.breeze.entity.Consultation")]
	public class Consultation
	{
		public var consultationId:Number;    
		public var consultationCode:String;    
		public var processTypeId:Number;    
		public var consultationType:Number;    
		public var isurgency:Number;    
		public var isafterwards:Number;    
		public var status:Number;    
		public var applyClinicId:Number;    
		public var applyDoctorId:Number;    
		public var applyReason:String;    
		public var applyPurpose:String;    
		public var consultationPlace:String;    
		public var bookStartDatetime:Date;    
		public var bookEndDatetime:Date;    
		public var startDatetime:Date;    
		public var endDatetime:Date;    
		public var patientId:Number;    
		public var inpatientId:String;    
		public var inpatientTimes:Number;    
		public var bedNo:String;    
		public var ward:String;    
		public var medicalAdviceId:String;    
		public var medicalAdviceStatus:String;    
		public var consultationFees:Number;    
		public var paymentPrintFlag:Number;    
		public var paymentStatus:Number;    
		public var remarks:String;    
		public var deleteFlag:Number;    
		public var deleteReason:String;    
		public var creator:Number;    
		public var createTime:Date;    
		public var updateTime:Date;    
		public var updater:Number;    
		public var updateCount:Number;    
		public var approvalDoctorId:Number;    
		public var room:String;    
		public var patientSource:Number;    
		public var outpatientNumber:String;    
		public var submitTime:Date;    
		public var outpatientTimes:Number;    
		public var inpatientNumber:String;    
		public var patientGo:Number;    
		public var outClinicId:Number;    
		public var outHospitalName:String;    
		public var outClinicName:String;    
		public var outApplyTime:Date;    
	}
}
