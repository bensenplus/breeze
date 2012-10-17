<%@ page language="java" pageEncoding="UTF-8" %>
<div id="top-bar">
	<img src="../resources/images/logo.png" alt="DZone" class="float-left" />
	<div id="right-side">
		<a href="#" class="first"><%=request.getUserPrincipal().getName()%></a>&ensp;
		<a href="../j_spring_security_logout">Logout</a> &emsp;
	</div>
</div>