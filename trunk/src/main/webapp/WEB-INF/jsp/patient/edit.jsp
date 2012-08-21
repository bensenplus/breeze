<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="../common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title></title>

<link rel="stylesheet" href="../resources/css/common.css" />
<%@ include file="../common/page.jsp"%>

</head>

<body>
	<form action="save">
		<input type="hidden" name="patientId" value="${patient.patientId}" />
		<table border=0 cellspacing=0 cellpadding=5 align=center>
			<tr>
			
				<td>患者医院HIS标识:</td>
				<td><input type="text"  name ="patientHisId" value="${patient.patientHisId}" size="20" /></td>
			</tr><tr>
				<td>医保号:</td>
				<td><input type="text"  name ="healthCardId" value="${patient.healthCardId}" size="20" /></td>
			
				<td>费用支付类别:</td>
				<td><input type="text"  name ="defrayType" value="${patient.defrayType}" size="20" /></td>
			</tr><tr>
				<td>姓名:</td>
				<td><input type="text"  name ="name" value="${patient.name}" size="20" /></td>
			
				<td>性别:</td>
				<td><input type="text"  name ="gender" value="${patient.gender}" size="20" /></td>
			</tr><tr>
				<td>出生日期:</td>
				<td><input type="text"  name ="birthDate" value="${patient.birthDate}" size="20" /></td>
			
				<td>证件类别:</td>
				<td><input type="text"  name ="certType" value="${patient.certType}" size="20" /></td>
			</tr><tr>
				<td>证件号码:</td>
				<td><input type="text"  name ="certId" value="${patient.certId}" size="20" /></td>
			
				<td>工作单位名称:</td>
				<td><input type="text"  name ="unitName" value="${patient.unitName}" size="20" /></td>
			</tr><tr>
				<td>工作单位地址:</td>
				<td><input type="text"  name ="unitAddress" value="${patient.unitAddress}" size="20" /></td>
			
				<td>工作单位邮编:</td>
				<td><input type="text"  name ="unitPostcode" value="${patient.unitPostcode}" size="20" /></td>
			</tr><tr>
				<td>工作单位电话:</td>
				<td><input type="text"  name ="unitTelephone" value="${patient.unitTelephone}" size="20" /></td>
			
				<td>所在科室:</td>
				<td><input type="text"  name ="clinicName" value="${patient.clinicName}" size="20" /></td>
			</tr><tr>
				<td>本人电话号码:</td>
				<td><input type="text"  name ="cellphone" value="${patient.cellphone}" size="20" /></td>
			
				<td>联系人姓名:</td>
				<td><input type="text"  name ="contactName" value="${patient.contactName}" size="20" /></td>
			</tr><tr>
				<td>联系人电话号码:</td>
				<td><input type="text"  name ="contactTelephone" value="${patient.contactTelephone}" size="20" /></td>
			
				<td>患者与联系人关系:</td>
				<td><input type="text"  name ="relationOfPatient" value="${patient.relationOfPatient}" size="20" /></td>
			</tr><tr>
				<td>国籍:</td>
				<td><input type="text"  name ="nationality" value="${patient.nationality}" size="20" /></td>
			
				<td>户籍地类别:</td>
				<td><input type="text"  name ="registerPlace" value="${patient.registerPlace}" size="20" /></td>
			</tr><tr>
				<td>籍贯:</td>
				<td><input type="text"  name ="birthPlace" value="${patient.birthPlace}" size="20" /></td>
			
				<td>民族:</td>
				<td><input type="text"  name ="ethnicGroup" value="${patient.ethnicGroup}" size="20" /></td>
			</tr><tr>
				<td>现住址:</td>
				<td><input type="text"  name ="residenceAddr" value="${patient.residenceAddr}" size="20" /></td>
			
				<td>婚姻状况:</td>
				<td><input type="text"  name ="maritalStatus" value="${patient.maritalStatus}" size="20" /></td>
			</tr><tr>
				<td>邮编:</td>
				<td><input type="text"  name ="postcode" value="${patient.postcode}" size="20" /></td>
			
				<td>职业类别:</td>
				<td><input type="text"  name ="jobType" value="${patient.jobType}" size="20" /></td>
			</tr><tr>
				<td>ABO血型:</td>
				<td><input type="text"  name ="aboBloodType" value="${patient.aboBloodType}" size="20" /></td>
			
				<td>RH血型:</td>
				<td><input type="text"  name ="rhBloodType" value="${patient.rhBloodType}" size="20" /></td>
			</tr><tr>
				<td>更新者标识:</td>
				<td><input type="text"  name ="updater" value="${patient.updater}" size="20" /></td>
			
				<td>更新时间:</td>
				<td><input type="text"  name ="updateTime" value="${patient.updateTime}" size="20" /></td>
			</tr><tr>
				<td>所在医院:</td>
				<td><input type="text"  name ="hospitalName" value="${patient.hospitalName}" size="20" /></td>
			
				<td>所在科室编码:</td>
				<td><input type="text"  name ="clinicCode" value="${patient.clinicCode}" size="20" /></td>
			</tr>
		</table>
		<p align=center>
			<input type="submit" value="更新" />
		</p>
	</form>
</body>
</html>

