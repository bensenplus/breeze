<%@ page language="java" pageEncoding="UTF-8"%>
<%@ taglib prefix="sitemesh" uri="http://www.opensymphony.com/sitemesh/decorator"%>
<html>
<head>
<title><sitemesh:title /></title>
<sitemesh:head />
<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
<meta http-equiv="Cache-Control" content="no-store" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />

<link rel="stylesheet" href="../resources/themes/base/jquery.ui.all.css">
<link rel="stylesheet" href="../resources/css/global.css" />
<link rel="stylesheet" href="../resources/css/pagination.css" />
<link rel="stylesheet" href="../resources/css/pro_drop/pro_drop.css" />

<script type="text/javascript" src="../resources/js/jquery-1.8.0.min.js"></script>
<script type="text/javascript" src="../resources/js/jquery-ui-1.8.23.custom.min.js"></script>
<script type="text/javascript" src="../resources/js/jquery.pagination.js"></script>
<script type="text/javascript" src="../resources/js/jquery-validation/jquery.validate.min.js"></script>
<script type="text/javascript" src="../resources/js/jquery-validation/jquery.validate.expand.js"></script>
<script type="text/javascript" src="../resources/js/jquery-validation/jquery.metadata.js"></script>
<script type="text/javascript" src="../resources/js/jquery-validation/messages_cn.js"></script>
<script  type="text/javascript" src="../resources/js/pro_drop.js"></script>
<script type="text/javascript" src="../resources/js/form.js"></script>

<script type="text/javascript">
$(function() {	
	initForm();
	initDatepicker();
});
</script>

</head>
<body>
	<%@ include file="header.jsp"%>
	<%@ include file="menu.jsp"%>
	<div id="upper-warp" style="clear: both;">
		<span id="search-icon" title="search"></span>
		<sitemesh:body />
		<div id="result-list-warp"></div>
	</div>
	<div id="update-form-warp">
		<ul class="tool-bar">
			<li><span id="edit-button">Edit</span></li>
			<li><span id="save-button">Save</span></li>
			<li><span id="back-button">Back</span></li>
		</ul>
		<form id="update-form"></form>
	</div>
	<%@ include file="footer.jsp"%>
</body>
</html>