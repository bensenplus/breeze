package org.breeze.security;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.breeze.dao.UserMapper;
import org.breeze.entity.User;
import org.springframework.security.authentication.AuthenticationServiceException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;


public class MyUsernamePasswordAuthenticationFilter extends UsernamePasswordAuthenticationFilter{
	
	private UserMapper userMapper; 	
	
	public static final String VALIDATE_CODE = "validateCode";
	public static final String USERNAME = "username";
	public static final String PASSWORD = "password";


	@Override
	public Authentication attemptAuthentication(HttpServletRequest request, HttpServletResponse response) throws AuthenticationException {
		if (!request.getMethod().equals("POST")) {
			throw new AuthenticationServiceException("Authentication method not supported: " + request.getMethod());
		}
		
		//checkValidateCode(request);
		
		String username = obtainUsername(request);
		String password = obtainPassword(request);
		
		//验证用户账号与密码是否对应
		username = username.trim();
		
		User user = this.userMapper.select(null).get(0);
		
		if(user == null || !user.getPassword().equals(password)) {
	/*
		if (forwardToDestination) {
            request.setAttribute(WebAttributes.AUTHENTICATION_EXCEPTION, exception);
        } else {
            HttpSession session = request.getSession(false);

            if (session != null || allowSessionCreation) {
                request.getSession().setAttribute(WebAttributes.AUTHENTICATION_EXCEPTION, exception);
            }
        }
	 */
			throw new AuthenticationServiceException("password or username is notEquals"); 
		}
		
		//UsernamePasswordAuthenticationToken实现 Authentication
		UsernamePasswordAuthenticationToken authRequest = new UsernamePasswordAuthenticationToken(username, password);
		// Place the last username attempted into HttpSession for views
		
		// 允许子类设置详细属性
        setDetails(request, authRequest);
		
        // 运行UserDetailsService的loadUserByUsername 再次封装Authentication
		return this.getAuthenticationManager().authenticate(authRequest);
	}
	
	protected void checkValidateCode(HttpServletRequest request) { 
		HttpSession session = request.getSession();
		
	    String sessionValidateCode = obtainSessionValidateCode(session); 
	    //让上一次的验证码失效
	    session.setAttribute(VALIDATE_CODE, null);
	    String validateCodeParameter = obtainValidateCodeParameter(request);  
	    if (validateCodeParameter==null || !sessionValidateCode.equalsIgnoreCase(validateCodeParameter)) {  
	        throw new AuthenticationServiceException("validateCode.notEquals");  
	    }  
	}
	
	private String obtainValidateCodeParameter(HttpServletRequest request) {
		Object obj = request.getParameter(VALIDATE_CODE);
		return null == obj ? "" : obj.toString();
	}

	protected String obtainSessionValidateCode(HttpSession session) {
		Object obj = session.getAttribute(VALIDATE_CODE);
		return null == obj ? "" : obj.toString();
	}

	@Override
	protected String obtainUsername(HttpServletRequest request) {
		Object obj = request.getParameter(USERNAME);
		return null == obj ? "" : obj.toString();
	}

	@Override
	protected String obtainPassword(HttpServletRequest request) {
		Object obj = request.getParameter(PASSWORD);
		return null == obj ? "" : obj.toString();
	}

	/**
	 * @return the userMapper
	 */
	public UserMapper getUserMapper() {
		return userMapper;
	}

	/**
	 * @param userMapper the userMapper to set
	 */
	public void setUserMapper(UserMapper userMapper) {
		this.userMapper = userMapper;
	}
	
	
}
