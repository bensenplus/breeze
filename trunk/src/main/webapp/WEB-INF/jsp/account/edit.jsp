<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="../common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>$ {account.lastName}</title>
<link rel="stylesheet" href="../resources/css/common.css" />
</head>
<body>
	<form action="save">
		<input type="hidden" name="userid" value="${account.userid}" />
		<table border=0 cellspacing=0 cellpadding=5 align=center>
			<tr>
				<td>姓名:</td>
				<td><input type="text"  name ="lastName" value="${account.lastName}" size="10" /> <input type="text"  name ="firstName"  value="${account.firstName}" size="10" /></td>
			</tr>
			<tr>
				<td>状态:</td>
				<td><input type="text"  name ="status"  value="${account.status}"/></td>
			</tr>
			<tr>
				<td>密码:</td>
				<td><input type="text"  name ="password"  value="${account.password}"/></td>
			</tr>
			<tr>
				<td>电子邮件:</td>
				<td><input type="text" name ="email"  value="${account.email}" size="40" /></td>
			</tr>
			<tr>
				<td>电话:</td>
				<td><input type="text"  name ="phone"  value="${account.phone}" size="40" /></td>
			</tr>
			<tr>
				<td>区域</td>
				<td>国家:<input type="text"  name ="country"  value="${account.country}" size="10" /> 省份:<input type="text" name ="state"  value="${account.state}" size="10" /> 市(县):<input type="text"  name ="city"  value="${account.city}" size="10" /></td>
			</tr>
			<tr>
				<td>地址1:</td>
				<td><input type="text" name ="address1"  value="${account.address1}" size="60" /></td>
			</tr>
			<tr>
				<td>地址2:</td>
				<td><input type="text" name ="address2"  value="${account.address2}" size="60" /></td>
			</tr>
			<tr>
				<td>邮编:</td>
				<td><input type="text"  name ="zip" value="${account.zip}" size="20" /></td>
			</tr>
		</table>
		<p align=center>
			<input type="submit" value="更新" />
		</p>
	</form>




</body>
</html>