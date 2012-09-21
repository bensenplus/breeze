<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="/common/taglibs.jsp"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt" %>
<html>
<head>
<title>列表</title>
<link rel="stylesheet" href="../resources/themes/base/jquery.ui.all.css">
<link rel="stylesheet" href="../resources/css/global.css" />
<link rel="stylesheet" href="../resources/css/layout.css" />
<link rel="stylesheet" href="../resources/css/pagination.css" />

<script  type="text/javascript" src="../resources/js/jquery-1.8.0.min.js"></script>
<script  type="text/javascript" src="../resources/js/jquery-ui-1.8.23.custom.min.js"></script>
<script type="text/javascript" src="../resources/js/jquery.pagination.js"></script>
<script  type="text/javascript" src="../resources/js/form.js"></script>
<script type="text/javascript">
$(function() {	
	initForm();
});
</script>
</head>
<body>

<div id="top-bar">
	<img src="../resources/images/logo.png" alt="DZone" class="floatleft" />
	<div id="right-side">
		<a href="#" class="first">User Name</a>&ensp;
		<a href="#">Login</a> &emsp;
	</div>
</div>
<%@ include file="menu.jsp"%>
<img  id="search-icon" src="../resources/images/search.png" alt="Search"   class="floatright" />