/*
 * Copyright (c) 2010-2020 Founder Ltd. All Rights Reserved.
 *
 * This software is the confidential and proprietary information of
 * Founder. You shall not disclose such Confidential Information
 * and shall use it only in accordance with the terms of the agreements
 * you entered into with Founder.
 */

package org.breeze.core.web;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class BeanUtil {
	
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
}
