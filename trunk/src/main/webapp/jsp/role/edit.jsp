﻿<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="/common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title></title>

<link rel="stylesheet" href="../resources/css/common.css" />

</head>

<body>
	<form action="save">
		<input type="hidden" name="roleId" value="${role.roleId}" />
		<table border=0 cellspacing=0 cellpadding=5 align=center>
			<tr>
			
				<td>:</td>
							<td><input type="text"  name ="enable" value="${role.enable}" size="20" /></td>
				
			</tr><tr>
				<td>:</td>
							<td><input type="text"  name ="name" value="${role.name}" size="20" /></td>
				
			</tr>
		</table>
		<p align=center>
			<input type="submit" value="更新" />
		</p>
	</form>
</body>
</html>

