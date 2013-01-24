<%@ page language="java" pageEncoding="UTF-8"%>
<div id="search-form-warp">
    <form id="search-form">
    	<input type="hidden" name="id" value="${clinic.id}" />
    	<table class="table-form">
    	    <tr>
    			<th>数据库唯一编号（自增长）:</th>
    			<td><input type="text" name ="id"  size="20" value=""  /></td>
    			<th>机构科室编码:</th>
    			<td><input type="text" name ="agencydepartmentcode"  size="20" value=""  /></td>
    			<th>上级科室编码:</th>
    			<td><input type="text" name ="supdepartmentscode"  size="20" value=""  /></td>
    			<th>机构科室简称:</th>
    			<td><input type="text" name ="agencydepartmentabbreviation"  size="20" value=""  /></td>
    </tr>
    <tr>
    			<th>机构科室全称:</th>
    			<td><input type="text" name ="agencydepartmentfullname"  size="20" value=""  /></td>
    			<th>机构科室类别:</th>
    			<td><input type="text" name ="agenciesdepartmentscategory"  size="20" value=""  /></td>
    			<th>所属机构编码:</th>
    			<td><input type="text" name ="subordinateorganizationscode"  size="20" value=""  /></td>
    			<th>所属机构名称:</th>
    			<td><input type="text" name ="subordinateorganizationsname"  size="20" value=""  /></td>
    </tr>
    <tr>
    			<th>科室电话:</th>
    			<td><input type="text" name ="departmentphone"  size="20" value=""  /></td>
    			<th>科室传真:</th>
    			<td><input type="text" name ="departmentfax"  size="20" value=""  /></td>
    		</tr>
    	</table>
        <ul class="tool-bar" >
            <li><span id="pdf-button">Pdf</span></li>
            <li><span id="excel-button">Excel</span></li>
            <li><span id="create-button">Create</span></li>
    		<li><span id="search-button">Search</span></li>
    	</ul>
    </form>
</div>