<%@ page language="java" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<script type="text/javascript">
function startEdit() {
    $( "#startdate" ).datepicker();
    $( "#enddate" ).datepicker();
}
$(function() {
    $( "#tabs" ).tabs();
});
</script>

<div id="tabs">
    <ul>
        <li><a href="#clinic">clinic</a></li>
    </ul>

<div id="clinic">
 
<input type="hidden" name="id" value="${clinic.id}" />
<table class="table-form">
    <tr>
            <th>数据库唯一编号（自增长）:</th>
		    <td><input type="text" name ="id"  size="40" value="${clinic.id}"  validate="{required:false,maxlength:254}"/></td>

            <th>机构科室编码:</th>
		    <td><input type="text" name ="agencydepartmentcode"  size="40" value="${clinic.agencydepartmentcode}"  validate="{required:false,maxlength:254}"/></td>
    </tr>
    <tr>

            <th>上级科室编码:</th>
		    <td><input type="text" name ="supdepartmentscode"  size="40" value="${clinic.supdepartmentscode}"  validate="{required:false,maxlength:254}"/></td>

            <th>机构科室简称:</th>
		    <td><input type="text" name ="agencydepartmentabbreviation"  size="40" value="${clinic.agencydepartmentabbreviation}"  validate="{required:false,maxlength:254}"/></td>
    </tr>
    <tr>

            <th>机构科室全称:</th>
		    <td><input type="text" name ="agencydepartmentfullname"  size="40" value="${clinic.agencydepartmentfullname}"  validate="{required:false,maxlength:254}"/></td>

            <th>机构科室类别:</th>
		    <td><input type="text" name ="agenciesdepartmentscategory"  size="40" value="${clinic.agenciesdepartmentscategory}"  validate="{required:false,maxlength:254}"/></td>
    </tr>
    <tr>

            <th>所属机构编码:</th>
		    <td><input type="text" name ="subordinateorganizationscode"  size="40" value="${clinic.subordinateorganizationscode}"  validate="{required:false,maxlength:254}"/></td>

            <th>所属机构名称:</th>
		    <td><input type="text" name ="subordinateorganizationsname"  size="40" value="${clinic.subordinateorganizationsname}"  validate="{required:false,maxlength:254}"/></td>
    </tr>
    <tr>

            <th>科室电话:</th>
		    <td><input type="text" name ="departmentphone"  size="40" value="${clinic.departmentphone}"  validate="{required:false,maxlength:254}"/></td>

            <th>科室传真:</th>
		    <td><input type="text" name ="departmentfax"  size="40" value="${clinic.departmentfax}"  validate="{required:false,maxlength:254}"/></td>
    </tr>
    <tr>

            <th>男性职工人数:</th>
		    <td><input type="text" name ="mnumber"  size="0" value="${clinic.mnumber}"  validate="{required:false, digits:true}"/></td>

            <th>女性职工人数:</th>
		    <td><input type="text" name ="fnumber"  size="0" value="${clinic.fnumber}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

        <th>科室成立日期:</th>
		<td><input type="text" id="startdate" name ="startdate" value='<fmt:formatDate value="${clinic.startdate}" pattern="yyyy/MM/dd"/>' size="20"  validate="{required:false, date:true}"/></td>

        <th>科室撤销日期:</th>
		<td><input type="text" id="enddate" name ="enddate" value='<fmt:formatDate value="${clinic.enddate}" pattern="yyyy/MM/dd"/>' size="20"  validate="{required:false, date:true}"/></td>
	</tr>
</table>
</div>

</div>
