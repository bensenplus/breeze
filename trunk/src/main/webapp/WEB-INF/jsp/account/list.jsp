<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="../common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>用户列表</title>
<link rel="stylesheet" href="../resources/css/common.css" />
<%@ include file="../common/page.jsp"%>
<script type="text/javascript">
	function pageselectCallback(page_index, jq) {
		var prev_page = $("current_page").val();
		if (page_index != prev_page) {
			$("#current_page").val(page_index);
			$("#condition").submit();
		}
		return false;
	}
</script>
</head>
<body>

	<form action="list" id="condition">
		<input type="hidden" id="current_page" name="currentPage" />
	</form>
	<table border=0 cellspacing=0 cellpadding=5 align=center>
		<thead>
			<tr>
				<td colspan=9><div id="Pagination"></div></td>
			<tr>
			<tr>
				<td>用户ID</td>
				<td>姓名</td>
				<td>电子邮件</td>
				<td>电话</td>
				<td>状态</td>
				<td>区域</td>
				<td colspan=3>操作</td>
			</tr>
		</thead>
		<tbody>
			<c:forEach var="account" items="${accountList}" varStatus="status">
				<tr>
					<td>${account.userid}</td>
					<td>${account.lastName}</td>
					<td>${account.email}</td>
					<td>${account.phone}</td>
					<td>${account.status}</td>
					<td>${account.country}"${account.state}"${account.city}</td>
					<td><a href="edit?userid=${account.userid}">编辑</a></td>
					<td><a href="delete?userid=${account.userid}">删除</a></td>
					<td><a href="copy?userid=${account.userid}">复制</a></td>
				</tr>
			</c:forEach>
		</tbody>
	</table>
</body>
</html>