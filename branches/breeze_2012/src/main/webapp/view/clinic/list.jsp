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
		    <th filed="Id">数据库唯一编号（自增长）</th>		
		    <th filed="AgencyDepartmentCode">机构科室编码</th>		
		    <th filed="SupDepartmentsCode">上级科室编码</th>		
		    <th filed="AgencyDepartmentAbbreviation">机构科室简称</th>		
		    <th filed="AgencyDepartmentFullName">机构科室全称</th>		
		    <th filed="AgenciesDepartmentsCategory">机构科室类别</th>		
		    <th filed="SubordinateOrganizationsCode">所属机构编码</th>		
		    <th filed="SubordinateOrganizationsName">所属机构名称</th>		
		    <th filed="DepartmentPhone">科室电话</th>		
		    <th filed="DepartmentFax">科室传真</th>		
		    <th width="120px" filed="StartDate">科室成立日期</th>		
		    <th width="120px" filed="EndDate">科室撤销日期</th>		
			<th width="30px">操作</th>
		</tr>
		<c:forEach var="clinic" items="${list}" varStatus="status">
            <tr ondblclick="edit('id=${clinic.id}')" onclick = 'rowclick(this)' id = '${status.count}'>
                <td><a href="${clinic.id}.xml">${status.count+page.start}</a></td>
				<td>${clinic.id}</td>
				<td>${clinic.agencydepartmentcode}</td>
				<td>${clinic.supdepartmentscode}</td>
				<td>${clinic.agencydepartmentabbreviation}</td>
				<td>${clinic.agencydepartmentfullname}</td>
				<td>${clinic.agenciesdepartmentscategory}</td>
				<td>${clinic.subordinateorganizationscode}</td>
				<td>${clinic.subordinateorganizationsname}</td>
				<td>${clinic.departmentphone}</td>
				<td>${clinic.departmentfax}</td>
				<td><fmt:formatDate value="${clinic.startdate}" pattern="yyyy/MM/dd" /></td>
				<td><fmt:formatDate value="${clinic.enddate}" pattern="yyyy/MM/dd" /></td>
				<td><span class="ui-icon ui-icon-close cursor-point" title="delete" onclick="remove('id=${clinic.id}')"></span></td>
        	</tr>
		</c:forEach>
</table>
<div id="page-footer"></div>