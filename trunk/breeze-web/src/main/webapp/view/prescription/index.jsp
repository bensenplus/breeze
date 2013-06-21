<%@ page language="java" pageEncoding="UTF-8"%>
<div id="search-form-warp">
    <form id="search-form">
    	<input type="hidden" name="adviceId" value="${prescription.adviceId}" />
    	<table class="table-form">
    	    <tr>
    			<th>患者姓名:</th>
    			<td><input type="text" name ="patientName"  size="20" value=""  /></td>
    			<th>性别代码:</th>
    			<td><input type="text" name ="sexCode"  size="20" value=""  /></td>
    			<th>处方编号:</th>
    			<td><input type="text" name ="recipelNo"  size="20" value=""  /></td>
    			<th>处方类别代码:</th>
    			<td><input type="text" name ="recipelType"  size="20" value=""  /></td>
    </tr>
    <tr>
    			<th>中药处方标志:</th>
    			<td><input type="text" name ="chnRecipelFlag"  size="20" value=""  /></td>
    			<th>中药饮片煎煮法:</th>
    			<td><input type="text" name ="chnDrugDecoctionMethod"  size="20" value=""  /></td>
    			<th>中药用药方法:</th>
    			<td><input type="text" name ="chnDrugUseMethod"  size="20" value=""  /></td>
    			<th>处方医师工号:</th>
    			<td><input type="text" name ="recipelDoctorNo"  size="20" value=""  /></td>
    </tr>
    <tr>
    			<th>处方医师职称:</th>
    			<td><input type="text" name ="recipelDoctorTitle"  size="20" value=""  /></td>
    			<th>处方医师签名:</th>
    			<td><input type="text" name ="recipelDoctorSign"  size="20" value=""  /></td>
    			<th>处方状态:</th>
    			<td><input type="text" name ="recipelState"  size="20" value=""  /></td>
    			<th>审核医师工号:</th>
    			<td><input type="text" name ="confirmDoctorNo"  size="20" value=""  /></td>
    </tr>
    <tr>
    			<th>审核医师职称:</th>
    			<td><input type="text" name ="confirmDoctorTitle"  size="20" value=""  /></td>
    			<th>审核医师签名:</th>
    			<td><input type="text" name ="confirmDoctorSign"  size="20" value=""  /></td>
    			<th>开处方科室名称:</th>
    			<td><input type="text" name ="recipelDeptName"  size="20" value=""  /></td>
    			<th>医疗机构名称:</th>
    			<td><input type="text" name ="orgName"  size="20" value=""  /></td>
    </tr>
    <tr>
    			<th>打回标志:</th>
    			<td><input type="text" name ="returnFlag"  size="20" value=""  /></td>
    			<th>临床路径用药标志:</th>
    			<td><input type="text" name ="clinicPathDrugFlag"  size="20" value=""  /></td>
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