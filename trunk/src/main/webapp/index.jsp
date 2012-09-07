<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>index</title>
<link rel="stylesheet" href="./resources/css/common.css">
<link rel="stylesheet" href="./resources/themes/base/jquery.ui.all.css">
<script  type="text/javascript" src="./resources/js/jquery-1.8.0.min.js"></script>
<script  type="text/javascript" src="./resources/js/jquery-ui-1.8.23.custom.min.js"></script>
<script type="text/javascript" src="../resources/js/jquery.pagination.js"></script>
<script type="text/javascript">
	$(function() {		
		$('.left-nav a').click(function(ev) {
			window.location.hash = this.href;
			//loadPage(this.href);
			$("#mainframe").attr("src", this.href);
			$('.left-nav a.selected').removeClass('selected');
			$(this).addClass('selected');
			ev.preventDefault();
		});
	});
	</script>
</head>
<body>
<div  class="left-nav">
	<dl class="demos-nav">
			<dd><a href="./flash/index.swf">flash</a></dd>
			<dd><a href="./accezz/list" >accezz</a></dd>
			<dd><a href="./actionControl/list" >actionControl</a></dd>
			<dd><a href="./activity/list" >activity</a></dd>
			<dd><a href="./actCfg/list" >actCfg</a></dd>
			<dd><a href="./attachment/list" >attachment</a></dd>
			<dd><a href="./clinic/list" >clinic</a></dd>
			<dd><a href="./conprocess/list" >conprocess</a></dd>
			<dd><a href="./consultation/list" >consultation</a></dd>
			<dd><a href="./consultationClinic/list" >consultationClinic</a></dd>
			<dd><a href="./consultationDoctor/list" >consultationDoctor</a></dd>
			<dd><a href="./cvDicmeta/list" >cvDicmeta</a></dd>
			<dd><a href="./cvDictionary/list" >cvDictionary</a></dd>
			<dd><a href="./doctor/list" >doctor</a></dd>
			<dd><a href="./doctorSuggestion/list" >doctorSuggestion</a></dd>
			<dd><a href="./evaluation/list" >evaluation</a></dd>
			<dd><a href="./evaluationItem/list" >evaluationItem</a></dd>
			<dd><a href="./evaluationOption/list" >evaluationOption</a></dd>
			<dd><a href="./fieldControl/list" >fieldControl</a></dd>
			<dd><a href="./hospital/list" >hospital</a></dd>
			<dd><a href="./log/list" >log</a></dd>
			<dd><a href="./menu/list" >menu</a></dd>
			<dd><a href="./messageNodeConfig/list" >messageNodeConfig</a></dd>
			<dd><a href="./notice/list" >notice</a></dd>
			<dd><a href="./patient/list" >patient</a></dd>
			<dd><a href="./poll/list" >poll</a></dd>
			<dd><a href="./pollItem/list" >pollItem</a></dd>
			<dd><a href="./receivedMessage/list" >receivedMessage</a></dd>
			<dd><a href="./role/list" >role</a></dd>
			<dd><a href="./roleAccess/list" >roleAccess</a></dd>
			<dd><a href="./setting/list" >setting</a></dd>
			<dd><a href="./siteConfig/list" >siteConfig</a></dd>
			<dd><a href="./suggestion/list" >suggestion</a></dd>
			<dd><a href="./suggestionVer/list" >suggestionVer</a></dd>
			<dd><a href="./sysControl/list" >sysControl</a></dd>
			<dd><a href="./task/list" >task</a></dd>
			<dd><a href="./template/list" >template</a></dd>
			<dd><a href="./tempmeta/list" >tempmeta</a></dd>
			<dd><a href="./uaccess/list" >uaccess</a></dd>
			<dd><a href="./users/list" >users</a></dd>
			<dd><a href="./userRole/list" >userRole</a></dd>
			<dd><a href="./workday/list" >workday</a></dd>
		</dl>
	</div>
	<div class="main-frame">
			<iframe frameborder="0"  name="mainframe"  id="mainframe" width="100%" height="100%"></iframe>
	</div>
</body>
</html>


