<%@ page language="java" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt" %>
<script type="text/javascript">
$(function() {
    initPage(${page.param});
});
</script>
<div id="page-header"></div>
<table class="table-list">
		<tr>
            <th width="20px">No.</th>
		    <th filed="PATIENT_NAME">患者姓名</th>		
		    <th filed="SEX_CODE">性别代码</th>		
		    <th filed="RECIPEL_NO">处方编号</th>		
		    <th width="120px" filed="RECIPEL_DT">开处方日期</th>		
		    <th filed="RECIPEL_TYPE">处方类别代码</th>		
		    <th filed="CHN_RECIPEL_FLAG">中药处方标志</th>		
		    <th filed="CHN_DRUG_DECOCTION_METHOD">中药饮片煎煮法</th>		
		    <th filed="CHN_DRUG_USE_METHOD">中药用药方法</th>		
		    <th filed="RECIPEL_DOCTOR_NO">处方医师工号</th>		
		    <th filed="RECIPEL_DOCTOR_TITLE">处方医师职称</th>		
		    <th filed="RECIPEL_DOCTOR_SIGN">处方医师签名</th>		
		    <th filed="RECIPEL_STATE">处方状态</th>		
		    <th filed="CONFIRM_DOCTOR_NO">审核医师工号</th>		
		    <th filed="CONFIRM_DOCTOR_TITLE">审核医师职称</th>		
		    <th filed="CONFIRM_DOCTOR_SIGN">审核医师签名</th>		
		    <th filed="RECIPEL_DEPT_NAME">开处方科室名称</th>		
		    <th filed="ORG_NAME">医疗机构名称</th>		
		    <th filed="RETURN_FLAG">打回标志</th>		
		    <th filed="CLINIC_PATH_DRUG_FLAG">临床路径用药标志</th>		
			<th width="30px">操作</th>
		</tr>
		<c:forEach var="prescription" items="${result.content}" varStatus="status">
            <tr ondblclick="edit('adviceId=${prescription.adviceId}')" onclick = 'rowclick(this)' id = '${status.count}'>
                <td><a href="${prescription.adviceId}.xml">${status.count+page.start}</a></td>
				<td>${prescription.patientName}</td>
				<td>${prescription.sexCode}</td>
				<td>${prescription.recipelNo}</td>
				<td><fmt:formatDate value="${prescription.recipelDt}" pattern="yyyy/MM/dd" /></td>
				<td>${prescription.recipelType}</td>
				<td>${prescription.chnRecipelFlag}</td>
				<td>${prescription.chnDrugDecoctionMethod}</td>
				<td>${prescription.chnDrugUseMethod}</td>
				<td>${prescription.recipelDoctorNo}</td>
				<td>${prescription.recipelDoctorTitle}</td>
				<td>${prescription.recipelDoctorSign}</td>
				<td>${prescription.recipelState}</td>
				<td>${prescription.confirmDoctorNo}</td>
				<td>${prescription.confirmDoctorTitle}</td>
				<td>${prescription.confirmDoctorSign}</td>
				<td>${prescription.recipelDeptName}</td>
				<td>${prescription.orgName}</td>
				<td>${prescription.returnFlag}</td>
				<td>${prescription.clinicPathDrugFlag}</td>
				<td><span class="ui-icon ui-icon-close cursor-point" title="delete" onclick="remove('adviceId=${prescription.adviceId}')"></span></td>
        	</tr>
		</c:forEach>
</table>
<div id="page-footer"></div>