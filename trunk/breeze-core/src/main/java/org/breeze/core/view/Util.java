/*
 * Copyright (c) 2010-2020 Founder Ltd. All Rights Reserved.
 *
 * This software is the confidential and proprietary information of
 * Founder. You shall not disclose such Confidential Information
 * and shall use it only in accordance with the terms of the agreements
 * you entered into with Founder.
 */

package org.breeze.core.view;

import java.io.IOException;
import java.io.PrintWriter;
import java.lang.reflect.Field;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;

import javax.servlet.http.HttpServletResponse;

public class Util {
	
	@SuppressWarnings("rawtypes")
	public static HashMap<String, Object> objToHash(Object obj) {
		HashMap<String, Object> hashMap = new HashMap<String, Object>();
		Class clazz = obj.getClass();
		List<Class> clazzs = new ArrayList<Class>();
		
		do {
			clazzs.add(clazz);
			clazz = clazz.getSuperclass();
		} while (!clazz.equals(Object.class));
		
		for (Class iClazz : clazzs) {
			Field[] fields = iClazz.getDeclaredFields();
			for (Field field : fields) {
				Object objVal = null;
				field.setAccessible(true);
				try {
					objVal = field.get(obj);
				} catch (IllegalArgumentException e) {
					e.printStackTrace();
				} catch (IllegalAccessException e) {
					e.printStackTrace();
				}
				if(objVal != null){
					String temp = String.valueOf(objVal).trim();
					if(!temp.isEmpty()) hashMap.put(field.getName(), temp);
				}
			}
		}
		
		return hashMap;
	}
	
	public static HashMap<String, Object> objToHash(Object obj, Page page) {
		HashMap<String, Object> map  =  objToHash(obj);
        if(page !=null){
            map.put("start",page.getStart());
            map.put("size",page.getSize());
            if(page.getOrder() != null && page.getOrder().length() >0){
            	map.put("order",page.getOrder());
            }
        }
		return map;
	}
	
	public static void outputJSONResult(String result, HttpServletResponse response) {
		try {
			response.setHeader("ContentType", "text/json");
			response.setCharacterEncoding("utf-8");
			PrintWriter pw = response.getWriter();
			pw.write(result);
			pw.flush();
			pw.close();

		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
    public static String formatDate(String aMask, Date aDate) {
        SimpleDateFormat df = null;
        String returnValue = "";

        if (aDate == null) {
        	return null;
        } else {
            df = new SimpleDateFormat(aMask);
            returnValue = df.format(aDate);
        }

        return (returnValue);
    }
	
}
