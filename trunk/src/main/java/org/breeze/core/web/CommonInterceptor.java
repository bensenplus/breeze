/*
 * Copyright (c) 2010-2020 Founder Ltd. All Rights Reserved.
 *
 * This software is the confidential and proprietary information of
 * Founder. You shall not disclose such Confidential Information
 * and shall use it only in accordance with the terms of the agreements
 * you entered into with Founder.
 */

package org.breeze.core.web;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;


public class CommonInterceptor extends HandlerInterceptorAdapter {

	private final Logger logger = LoggerFactory.getLogger(HandlerInterceptorAdapter.class); 
	
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler)
		    throws Exception {
			logger.debug(request.getRequestURI());
			return true;
		}

}
