<%@ page language="java" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<script type="text/javascript">
function startEdit() {
    $( "#recipelDt" ).datepicker();
}
$(function() {
    $( "#tabs" ).tabs();
});
</script>

<div id="tabs">
    <ul>
        <li><a href="#prescription">prescription</a></li>
    </ul>

<div id="prescription">
 
<input type="hidden" name="adviceId" value="${prescription.adviceId}" />
<table class="table-form">
    <tr>
            <th>就诊号:</th>
		    <td><input type="text" name ="medicalNo"  size="0" value="${prescription.medicalNo}"  validate="{required:false, digits:true}"/></td>

            <th>患者标识:</th>
		    <td><input type="text" name ="patientId"  size="0" value="${prescription.patientId}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

            <th>患者姓名:</th>
		    <td><input type="text" name ="patientName"  size="40" value="${prescription.patientName}"  validate="{required:false,maxlength:200}"/></td>

            <th>性别代码:</th>
		    <td><input type="text" name ="sexCode"  size="40" value="${prescription.sexCode}"  validate="{required:false,maxlength:80}"/></td>
    </tr>
    <tr>

            <th>年龄:</th>
		    <td><input type="text" name ="age"  size="0" value="${prescription.age}"  validate="{required:false, digits:true}"/></td>

            <th>处方编号:</th>
		    <td><input type="text" name ="recipelNo"  size="40" value="${prescription.recipelNo}"  validate="{required:false,maxlength:80}"/></td>
    </tr>
    <tr>

        <th>开处方日期:</th>
		<td><input type="text" id="recipelDt" name ="recipelDt" value='<fmt:formatDate value="${prescription.recipelDt}" pattern="yyyy/MM/dd"/>' size="20"  validate="{required:false, date:true}"/></td>

            <th>处方有效天数:</th>
		    <td><input type="text" name ="recipelDays"  size="0" value="${prescription.recipelDays}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

            <th>处方类别代码:</th>
		    <td><input type="text" name ="recipelType"  size="40" value="${prescription.recipelType}"  validate="{required:false,maxlength:80}"/></td>

            <th>中药处方标志:</th>
		    <td><input type="text" name ="chnRecipelFlag"  size="40" value="${prescription.chnRecipelFlag}"  validate="{required:false,maxlength:80}"/></td>
    </tr>
    <tr>

    </tr>
    <tr>
            <th>中药饮片处方:</th>
            <td colspan="3"><textarea name ="chnDrug" rows=5 style="width:100%"  validate="{required:false,maxlength:2000}">${prescription.chnDrug}</textarea></td>
    </tr>
    <tr>

            <th>中药饮片剂数:</th>
		    <td><input type="text" name ="chnDrugAmount"  size="0" value="${prescription.chnDrugAmount}"  validate="{required:false, digits:true}"/></td>

            <th>中药饮片煎煮法:</th>
		    <td><input type="text" name ="chnDrugDecoctionMethod"  size="40" value="${prescription.chnDrugDecoctionMethod}"  validate="{required:false,maxlength:400}"/></td>
    </tr>
    <tr>

            <th>中药用药方法:</th>
		    <td><input type="text" name ="chnDrugUseMethod"  size="40" value="${prescription.chnDrugUseMethod}"  validate="{required:false,maxlength:200}"/></td>

            <th>处方医师工号:</th>
		    <td><input type="text" name ="recipelDoctorNo"  size="40" value="${prescription.recipelDoctorNo}"  validate="{required:false,maxlength:80}"/></td>
    </tr>
    <tr>

            <th>处方医师标识:</th>
		    <td><input type="text" name ="recipelDoctorId"  size="0" value="${prescription.recipelDoctorId}"  validate="{required:false, digits:true}"/></td>

            <th>处方医师职称:</th>
		    <td><input type="text" name ="recipelDoctorTitle"  size="40" value="${prescription.recipelDoctorTitle}"  validate="{required:false,maxlength:80}"/></td>
    </tr>
    <tr>

            <th>处方医师签名:</th>
		    <td><input type="text" name ="recipelDoctorSign"  size="40" value="${prescription.recipelDoctorSign}"  validate="{required:false,maxlength:200}"/></td>

            <th>处方状态:</th>
		    <td><input type="text" name ="recipelState"  size="40" value="${prescription.recipelState}"  validate="{required:false,maxlength:80}"/></td>
    </tr>
    <tr>

            <th>审核医师工号:</th>
		    <td><input type="text" name ="confirmDoctorNo"  size="40" value="${prescription.confirmDoctorNo}"  validate="{required:false,maxlength:80}"/></td>

            <th>审核医师标识:</th>
		    <td><input type="text" name ="confirmDoctorId"  size="0" value="${prescription.confirmDoctorId}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

            <th>审核医师职称:</th>
		    <td><input type="text" name ="confirmDoctorTitle"  size="40" value="${prescription.confirmDoctorTitle}"  validate="{required:false,maxlength:80}"/></td>

            <th>审核医师签名:</th>
		    <td><input type="text" name ="confirmDoctorSign"  size="40" value="${prescription.confirmDoctorSign}"  validate="{required:false,maxlength:200}"/></td>
    </tr>
    <tr>

            <th>总金额:</th>
		    <td><input type="text" name ="sumFee"  size="0" value="${prescription.sumFee}"  validate="{required:false, digits:true}"/></td>

            <th>科室标识:</th>
		    <td><input type="text" name ="deptId"  size="0" value="${prescription.deptId}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

            <th>开处方科室名称:</th>
		    <td><input type="text" name ="recipelDeptName"  size="40" value="${prescription.recipelDeptName}"  validate="{required:false,maxlength:200}"/></td>

            <th>医疗机构标识:</th>
		    <td><input type="text" name ="orgId"  size="0" value="${prescription.orgId}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

            <th>医疗机构名称:</th>
		    <td><input type="text" name ="orgName"  size="40" value="${prescription.orgName}"  validate="{required:false,maxlength:280}"/></td>

            <th>体重:</th>
		    <td><input type="text" name ="weight"  size="0" value="${prescription.weight}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

            <th>打回标志:</th>
		    <td><input type="text" name ="returnFlag"  size="40" value="${prescription.returnFlag}"  validate="{required:false,maxlength:80}"/></td>

            <th>总金额(应收):</th>
		    <td><input type="text" name ="sumFeeShow"  size="0" value="${prescription.sumFeeShow}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

            <th>舍入误差(应收-总金额）:</th>
		    <td><input type="text" name ="errorFee"  size="0" value="${prescription.errorFee}"  validate="{required:false, digits:true}"/></td>

            <th>临床路径用药标志:</th>
		    <td><input type="text" name ="clinicPathDrugFlag"  size="40" value="${prescription.clinicPathDrugFlag}"  validate="{required:false,maxlength:80}"/></td>
    </tr>
    <tr>

            <th>经方标识:</th>
		    <td><input type="text" name ="classicRecipelId"  size="0" value="${prescription.classicRecipelId}"  validate="{required:false, digits:true}"/></td>
	</tr>
</table>
</div>

</div>
