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
			    <td>患者标识</td>		
			    <td>患者医院HIS标识</td>		
			    <td>医保号</td>		
			    <td>费用支付类别</td>		
			    <td>姓名</td>		
			    <td>性别</td>		
			    <td>出生日期</td>		
			    <td>证件类别</td>		
			    <td>证件号码</td>		
			    <td>工作单位名称</td>		
			    <td>工作单位地址</td>		
			    <td>工作单位邮编</td>		
			    <td>工作单位电话</td>		
			    <td>所在科室</td>		
			    <td>本人电话号码</td>		
			    <td>联系人姓名</td>		
			    <td>联系人电话号码</td>		
			    <td>患者与联系人关系</td>		
			    <td>国籍</td>		
			    <td>户籍地类别</td>		
			    <td>籍贯</td>		
			    <td>民族</td>		
			    <td>现住址</td>		
			    <td>婚姻状况</td>		
			    <td>邮编</td>		
			    <td>职业类别</td>		
			    <td>ABO血型</td>		
			    <td>RH血型</td>		
			    <td>更新者标识</td>		
			    <td>更新时间</td>		
			    <td>所在医院</td>		
			    <td>所在科室编码</td>		
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
					<td><a href="copy?patientId=${patient.patientId}">复制</a></td>
            	</tr>
			</c:forEach>
		</tbody>
	</table>
</body>
</html>

