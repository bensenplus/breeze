/*
 * Copyright (c) 2010-2020 Founder Ltd. All Rights Reserved.
 *
 * This software is the confidential and proprietary information of
 * Founder. You shall not disclose such Confidential Information
 * and shall use it only in accordance with the terms of the agreements
 * you entered into with Founder.
 */

package org.breeze.controller;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.junit.BeforeClass;
import org.springframework.mock.web.MockServletContext;
import org.springframework.web.context.WebApplicationContext;
import org.springframework.web.context.support.XmlWebApplicationContext;
import org.springframework.web.servlet.HandlerAdapter;
import org.springframework.web.servlet.HandlerExecutionChain;
import org.springframework.web.servlet.HandlerMapping;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.mvc.annotation.AnnotationMethodHandlerAdapter;
import org.springframework.web.servlet.mvc.annotation.DefaultAnnotationHandlerMapping;

public class BaseController {
    
    private static HandlerMapping handlerMapping;  
   private static HandlerAdapter handlerAdapter; 
	
	@BeforeClass
    public static  void setUp() {  
        String[] configs = { "/config.database.xml","/config.jpa.xml","/config.mvc.xml" };  
        XmlWebApplicationContext context = new XmlWebApplicationContext();  
        context.setConfigLocations(configs);  
        MockServletContext msc = new MockServletContext();  
        context.setServletContext(msc);
        context.refresh();  
        msc.setAttribute(WebApplicationContext.ROOT_WEB_APPLICATION_CONTEXT_ATTRIBUTE, context);  
        handlerMapping = (HandlerMapping) context   .getBean(DefaultAnnotationHandlerMapping.class);  
        handlerAdapter = (HandlerAdapter) context.getBean(context.getBeanNamesForType(AnnotationMethodHandlerAdapter.class)[0]); 
    } 
	
    public ModelAndView excuteAction(HttpServletRequest request, HttpServletResponse response)  	throws Exception {  
       HandlerExecutionChain chain = handlerMapping.getHandler(request);  
       final ModelAndView model = handlerAdapter.handle(request, response,    chain.getHandler());  
       return model;  
   }
	
}
