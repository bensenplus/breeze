/*
 * Copyright (c) 2010-2020 Founder Ltd. All Rights Reserved.
 * 
 * This software is the confidential and proprietary information of Founder. You
 * shall not disclose such Confidential Information and shall use it only in
 * accordance with the terms of the agreements you entered into with Founder.
 */

package org.breeze.core.aop;

import java.lang.reflect.Method;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.aop.MethodBeforeAdvice;

public class SpringAOPInterceptor implements MethodBeforeAdvice {

	private final Logger logger = LoggerFactory.getLogger(MethodBeforeAdvice.class); 
	
	@Override
	public void before(Method method, Object[] objects, Object object)
			throws Throwable {
		 logger.debug("The Interceptor method name is: "
				+ method.getDeclaringClass().getName() + ". " + method.getName());
		String value = "";
		for (int i = 0; i < objects.length; i++) {
			value += objects[i].toString() + "&";
		}
		logger.debug("The method parames is:" + value);
		logger.debug("The target class is:" + object.getClass().getName());
	}
}