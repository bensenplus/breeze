<%@ page language="java" contentType="text/html; charset=UTF-8"   pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<form action="${pageContext.request.contextPath}/j_spring_security_check" method="post">
    	USERNAME:<input type="text" name="j_username" value="${sessionScope['SPRING_SECURITY_LAST_USERNAME']}" /><br/>
    	PASSWORD:<input type="password" name="j_password" value="" /><br/>
    	<input type="checkbox" name="_spring_security_remember_me" />两周之内不必登陆<br/>
		<input type="submit">    	
 </form>