<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="../common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title></title>

<link rel="stylesheet" href="../resources/css/common.css" />
<%@ include file="../common/page.jsp"%>

</head>

<body>
	<form action="save">
		<input type="hidden" name="uSERID" value="${account.uSERID}" />
		<table border=0 cellspacing=0 cellpadding=5 align=center>
			<tr>
			
				<td>:</td>
				<td><input type="text"  name ="password" value="${account.password}" size="20" /></td>
			</tr><tr>
				<td>:</td>
				<td><input type="text"  name ="email" value="${account.email}" size="20" /></td>
			
				<td>:</td>
				<td><input type="text"  name ="firstname" value="${account.firstname}" size="20" /></td>
			</tr><tr>
				<td>:</td>
				<td><input type="text"  name ="lastname" value="${account.lastname}" size="20" /></td>
			
				<td>:</td>
				<td><input type="text"  name ="status" value="${account.status}" size="20" /></td>
			</tr><tr>
				<td>:</td>
				<td><input type="text"  name ="address1" value="${account.address1}" size="20" /></td>
			
				<td>:</td>
				<td><input type="text"  name ="address2" value="${account.address2}" size="20" /></td>
			</tr><tr>
				<td>:</td>
				<td><input type="text"  name ="city" value="${account.city}" size="20" /></td>
			
				<td>:</td>
				<td><input type="text"  name ="state" value="${account.state}" size="20" /></td>
			</tr><tr>
				<td>:</td>
				<td><input type="text"  name ="zip" value="${account.zip}" size="20" /></td>
			
				<td>:</td>
				<td><input type="text"  name ="country" value="${account.country}" size="20" /></td>
			</tr><tr>
				<td>:</td>
				<td><input type="text"  name ="phone" value="${account.phone}" size="20" /></td>
			</tr>
		</table>
		<p align=center>
			<input type="submit" value="更新" />
		</p>
	</form>
</body>
</html>

