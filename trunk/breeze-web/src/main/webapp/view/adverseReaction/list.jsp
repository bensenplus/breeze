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
		    <th filed="REG_NO">RegNo</th>		
		    <th filed="PATIENT_NAME">PatientName</th>		
		    <th filed="TYPE">Type</th>		
		    <th filed="RECORDER_NO">RecorderNo</th>		
		    <th filed="RECORDER_NAME">RecorderName</th>		
		    <th filed="CREATOR">Creator</th>		
		    <th width="120px" filed="CREATE_TIME">CreateTime</th>		
		    <th filed="MODIFIER">Modifier</th>		
		    <th width="120px" filed="MODIFY_TIME">ModifyTime</th>		
			<th width="30px">操作</th>
		</tr>
		<c:forEach var="adverseReaction" items="${list}" varStatus="status">
            <tr ondblclick="edit('id=${adverseReaction.id}')" onclick = 'rowclick(this)' id = '${status.count}'>
                <td><a href="${adverseReaction.id}.xml">${status.count+page.start}</a></td>
				<td>${adverseReaction.regNo}</td>
				<td>${adverseReaction.patientName}</td>
				<td>${adverseReaction.type}</td>
				<td>${adverseReaction.recorderNo}</td>
				<td>${adverseReaction.recorderName}</td>
				<td>${adverseReaction.creator}</td>
				<td><fmt:formatDate value="${adverseReaction.createTime}" pattern="yyyy/MM/dd" /></td>
				<td>${adverseReaction.modifier}</td>
				<td><fmt:formatDate value="${adverseReaction.modifyTime}" pattern="yyyy/MM/dd" /></td>
				<td><span class="ui-icon ui-icon-close cursor-point" title="delete" onclick="remove('id=${adverseReaction.id}')"></span></td>
        	</tr>
		</c:forEach>
</table>
<div id="page-footer"></div>