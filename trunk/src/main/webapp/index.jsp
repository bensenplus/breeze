
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<html>
<head>
<title>index</title>
<link rel="stylesheet" href="./resources/themes/base/jquery.ui.all.css">
<link rel="stylesheet" href="./resources/css/common.css">
<script  type="text/javascript" src="./resources/js/jquery-1.8.0.min.js"></script>
<script  type="text/javascript" src="./resources/js/jquery-ui-1.8.23.custom.min.js"></script>
<script type="text/javascript">
	$(function() {		
		$('.left-nav a').click(function(ev) {
			//window.location.hash = "index.html";
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
		<dt>Menu</dt>
			<dd><a href="./log/list" target="mainframe">Log</a></dd>
			<dd><a href="./menu/list" target="mainframe">Menu</a></dd>
			<dd><a href="./messageCache/list" target="mainframe">MessageCache</a></dd>
			<dd><a href="./messageNodeConfig/list" target="mainframe">MessageNodeConfig</a></dd>
			<dd><a href="./notice/list" target="mainframe">Notice</a></dd>
			<dd><a href="./patient/list" target="mainframe">Patient</a></dd>
			<dd><a href="./poll/list" target="mainframe">Poll</a></dd>
			<dd><a href="./pollItem/list" target="mainframe">PollItem</a></dd>
			<dd><a href="./receivedMessage/list" target="mainframe">ReceivedMessage</a></dd>
			<dd><a href="./role/list" target="mainframe">Role</a></dd>
			<dd><a href="./roleAccess/list" target="mainframe">RoleAccess</a></dd>
			<dd><a href="./setting/list" target="mainframe">Setting</a></dd>
			<dd><a href="./siteConfig/list" target="mainframe">SiteConfig</a></dd>
			<dd><a href="./suggestion/list" target="mainframe">Suggestion</a></dd>
			<dd><a href="./suggestionVer/list" target="mainframe">SuggestionVer</a></dd>
			<dd><a href="./sysControl/list" target="mainframe">SysControl</a></dd>
			<dd><a href="./task/list" target="mainframe">Task</a></dd>
			<dd><a href="./template/list" target="mainframe">Template</a></dd>
			<dd><a href="./tempmeta/list" target="mainframe">Tempmeta</a></dd>
			<dd><a href="./uaccess/list" target="mainframe">Uaccess</a></dd>
			<dd><a href="./users/list" target="mainframe">Users</a></dd>
			<dd><a href="./userRole/list" target="mainframe">UserRole</a></dd>
			<dd><a href="./workday/list" target="mainframe">Workday</a></dd>
	</dl>
</div>
<div class="main-frame">
	  <iframe frameborder="0"  name="mainframe"  id="mainframe" width="100%" height="100%"></iframe>
</div>
</body>
</html>
