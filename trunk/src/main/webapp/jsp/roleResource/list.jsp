<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="/common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>列表</title>

<link rel="stylesheet" href="../resources/css/common.css" />
<%@ include file="/common/page.jsp"%>

</head>

<body>

	<table border=0 cellspacing=0 cellpadding=5 align=center>
		<thead>
			<tr>
				<td colspan=9><div id="Pagination"></div></td>
			<tr>
			<tr>
			    <td></td>		
			    <td></td>		
			    <td></td>		
				<td colspan=3>操作</td>
			</tr>
		</thead>
		<tbody>
			<c:forEach var="roleResource" items="${list}" varStatus="status">
                <tr>
					<td>${roleResource.id}</td>
					<td>${roleResource.roleId}</td>
					<td>${roleResource.resourceId}</td>
					<td><a href="edit?id=${roleResource.id}">编辑</a></td>
					<td><a href="delete?id=${roleResource.id}">删除</a></td>
            	</tr>
			</c:forEach>
		</tbody>
	</table>
</body>
</html>

