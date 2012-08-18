<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="../common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>列表</title>

<link rel="stylesheet" href="../resources/css/common.css" />
<%@ include file="../common/page.jsp"%>

</head>

<body>

	<table border=0 cellspacing=0 cellpadding=5 align=center>
		<thead>
			<tr>
				<td colspan=9><div id="Pagination"></div></td>
			<tr>
			<tr>
			    <td>PATIENT_ID</td>		
			    <td>PATIENT_HIS_ID</td>		
			    <td>HEALTH_CARD_ID</td>		
			    <td>DEFRAY_TYPE</td>		
			    <td>NAME</td>		
			    <td>GENDER</td>		
			    <td>BIRTH_DATE</td>		
			    <td>CERT_TYPE</td>		
			    <td>CERT_ID</td>		
			    <td>UNIT_NAME</td>		
			    <td>UNIT_ADDRESS</td>		
			    <td>UNIT_POSTCODE</td>		
			    <td>UNIT_TELEPHONE</td>		
			    <td>CLINIC_NAME</td>		
			    <td>CELLPHONE</td>		
			    <td>CONTACT_NAME</td>		
			    <td>CONTACT_TELEPHONE</td>		
			    <td>RELATION_OF_PATIENT</td>		
			    <td>NATIONALITY</td>		
			    <td>REGISTER_PLACE</td>		
			    <td>BIRTH_PLACE</td>		
			    <td>ETHNIC_GROUP</td>		
			    <td>RESIDENCE_ADDR</td>		
			    <td>MARITAL_STATUS</td>		
			    <td>POSTCODE</td>		
			    <td>JOB_TYPE</td>		
			    <td>ABO_BLOOD_TYPE</td>		
			    <td>RH_BLOOD_TYPE</td>		
			    <td>UPDATER</td>		
			    <td>UPDATE_TIME</td>		
			    <td>HOSPITAL_NAME</td>		
			    <td>CLINIC_CODE</td>		
				<td colspan=3>操作</td>
			</tr>
		</thead>
		<tbody>
			<c:forEach var="patient" items="${list}" varStatus="status">
                <tr>
					<td>${patient.patientId}</td>		
					<td>${patient.patientHisId}</td>		
					<td>${patient.healthCardId}</td>		
					<td>${patient.defrayType}</td>		
					<td>${patient.name}</td>		
					<td>${patient.gender}</td>		
					<td>${patient.birthDate}</td>		
					<td>${patient.certType}</td>		
					<td>${patient.certId}</td>		
					<td>${patient.unitName}</td>		
					<td>${patient.unitAddress}</td>		
					<td>${patient.unitPostcode}</td>		
					<td>${patient.unitTelephone}</td>		
					<td>${patient.clinicName}</td>		
					<td>${patient.cellphone}</td>		
					<td>${patient.contactName}</td>		
					<td>${patient.contactTelephone}</td>		
					<td>${patient.relationOfPatient}</td>		
					<td>${patient.nationality}</td>		
					<td>${patient.registerPlace}</td>		
					<td>${patient.birthPlace}</td>		
					<td>${patient.ethnicGroup}</td>		
					<td>${patient.residenceAddr}</td>		
					<td>${patient.maritalStatus}</td>		
					<td>${patient.postcode}</td>		
					<td>${patient.jobType}</td>		
					<td>${patient.aboBloodType}</td>		
					<td>${patient.rhBloodType}</td>		
					<td>${patient.updater}</td>		
					<td>${patient.updateTime}</td>		
					<td>${patient.hospitalName}</td>		
					<td>${patient.clinicCode}</td>		
					<td><a href="edit?patientId=${patient.patientId}">编辑</a></td>
					<td><a href="delete?patientId=${patient.patientId}">删除</a></td>
            	</tr>
			</c:forEach>
		</tbody>
	</table>
</body>
</html>

