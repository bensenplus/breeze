
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>列表</title>
<style type="text/css">
#left {position:absolute;width: 20%;height:100%; border: 1px solid #000;}
#right {position:absolute; margin-left:20%; width: 80%; height:100%;border: 1px solid #000;}
</style>
</head>
<body>
	<div id="left">
		<ul>
			<li><a href="./j_spring_security_logout">logout</a></li>
			<li><a href="./resourceUrl/list" target="mainframe">resourceUrl</a></li>
			<li><a href="./role/list" target="mainframe">role</a></li>
			<li><a href="./roleResource/list" target="mainframe">roleResource</a></li>
			<li><a href="./user/list" target="mainframe">user</a></li>
			<li><a href="./userRole/list" target="mainframe">userRole</a></li>
		</ul>
	</div>
	<div id="right">
		<iframe frameborder="0"  name="mainframe"  id="mainframe" width="100%" height="100%"></iframe>
	</div>
</body>
</html>

