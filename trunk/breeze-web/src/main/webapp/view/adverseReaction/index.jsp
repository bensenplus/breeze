<%@ page language="java" pageEncoding="UTF-8"%>
<div id="search-form-warp">
    <form id="search-form">
    	<input type="hidden" name="id" value="${adverseReaction.id}" />
    	<table class="table-form">
    	    <tr>
    			<th>RegNo:</th>
    			<td><input type="text" name ="regNo"  size="20" value=""  /></td>
    			<th>PatientName:</th>
    			<td><input type="text" name ="patientName"  size="20" value=""  /></td>
    			<th>Type:</th>
    			<td><input type="text" name ="type"  size="20" value=""  /></td>
    			<th>RecorderNo:</th>
    			<td><input type="text" name ="recorderNo"  size="20" value=""  /></td>
    </tr>
    <tr>
    			<th>RecorderName:</th>
    			<td><input type="text" name ="recorderName"  size="20" value=""  /></td>
    			<th>Creator:</th>
    			<td><input type="text" name ="creator"  size="20" value=""  /></td>
    			<th>Modifier:</th>
    			<td><input type="text" name ="modifier"  size="20" value=""  /></td>
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