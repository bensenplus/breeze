<%@ page language="java" pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<script type="text/javascript">
function startEdit() {
    $( "#modifyTime" ).datepicker();
}
$(function() {
    $( "#tabs" ).tabs();
});
</script>

<div id="tabs">
    <ul>
        <li><a href="#adverseReaction">adverseReaction</a></li>
    </ul>

<div id="adverseReaction">
 
<input type="hidden" name="id" value="${adverseReaction.id}" />
<table class="table-form">
    <tr>
            <th>RegNo:</th>
		    <td><input type="text" name ="regNo"  size="20" value="${adverseReaction.regNo}"  validate="{required:true,maxlength:20}"/></td>

            <th>PatientName:</th>
		    <td><input type="text" name ="patientName"  size="40" value="${adverseReaction.patientName}"  validate="{required:false,maxlength:50}"/></td>
    </tr>
    <tr>

            <th>Type:</th>
		    <td><input type="text" name ="type"  size="2" value="${adverseReaction.type}"  validate="{required:false,maxlength:2}"/></td>

            <th>ExistReaction:</th>
		    <td><input type="text" name ="existReaction"  size="0" value="${adverseReaction.existReaction}"  validate="{required:false, digits:true}"/></td>
    </tr>
    <tr>

    </tr>
    <tr>
            <th>Descriple:</th>
            <td colspan="3"><textarea name ="descriple" rows=10 style="width:100%"  validate="{required:false,maxlength:4000}">${adverseReaction.descriple}</textarea></td>
    </tr>
    <tr>

            <th>RecorderNo:</th>
		    <td><input type="text" name ="recorderNo"  size="10" value="${adverseReaction.recorderNo}"  validate="{required:false,maxlength:10}"/></td>

            <th>RecorderName:</th>
		    <td><input type="text" name ="recorderName"  size="40" value="${adverseReaction.recorderName}"  validate="{required:false,maxlength:50}"/></td>
    </tr>
    <tr>

            <th>Creator:</th>
		    <td><input type="text" name ="creator"  size="10" value="${adverseReaction.creator}"  validate="{required:false,maxlength:10}"/></td>

            <th>Modifier:</th>
		    <td><input type="text" name ="modifier"  size="10" value="${adverseReaction.modifier}"  validate="{required:false,maxlength:10}"/></td>
    </tr>
    <tr>

        <th>ModifyTime:</th>
		<td><input type="text" id="modifyTime" name ="modifyTime" value='<fmt:formatDate value="${adverseReaction.modifyTime}" pattern="yyyy/MM/dd"/>' size="20"  validate="{required:false, date:true}"/></td>
	</tr>
</table>
</div>

</div>
