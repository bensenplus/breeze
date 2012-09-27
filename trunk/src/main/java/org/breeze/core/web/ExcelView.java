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
import java.util.Date;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFCellStyle;
import org.apache.poi.hssf.usermodel.HSSFSheet;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.springframework.web.servlet.view.document.AbstractExcelView;


/**
 * 
 * 
 * @version 1.0, 2012-9-26
 * @author Chen Maohua
 */
public class ExcelView extends AbstractExcelView {

	private  Class<?> modeClass;
	
	public ExcelView(Class<?> modeClass){
		super();
		this.modeClass = modeClass;
	}

	@Override
	protected void buildExcelDocument(Map<String, Object> model, HSSFWorkbook workbook,
			HttpServletRequest request, HttpServletResponse response) throws Exception {
		HSSFSheet sheet = workbook.createSheet("sheet");	 
		int m=0, n =0;
		Field[] fields = modeClass.getDeclaredFields();
		HSSFCellStyle style = workbook.createCellStyle();
		style.setFillBackgroundColor((short)0xff0);
		for (Field field : fields) {
			  HSSFCell cell = getCell(sheet, m, n);
			  cell.setCellStyle(style);
			  cell.setCellValue(field.getName());
			  n++;
		}
		 m++;
		 ArrayList<?>  list = (ArrayList<?>)model.get("list");
		if (list != null){
			for(Object object: list){
				 	n=0;
					for (Field field : fields) {
						  HSSFCell cell = getCell(sheet, m, n);
						  field.setAccessible(true);
						  Object value = field.get(object);
						  if(value!=null){
							  if(value instanceof  Date){
								  cell.setCellValue(Util.formatDate("yyyy/MM/dd hh:mm:ss", (Date)value));
							  }else{
								  cell.setCellValue(value.toString());
							  }
						  }
						  n++;
					}
					m++;
			}
		}
		
    
/*	       HSSFCell cell = getCell(sheet, 0, 0);
	       setText(cell, "Spring Excel test");
	  
	       HSSFCellStyle dateStyle = workbook.createCellStyle();
	       dateStyle.setDataFormat(HSSFDataFormat.getBuiltinFormat("m/d/yy"));
	       cell = getCell(sheet, 1, 0);
	       cell.setCellValue(new Date());
	       cell.setCellStyle(dateStyle);
	       getCell(sheet, 2, 0).setCellValue(458);
	  
	       HSSFRow sheetRow = sheet.createRow(3);
	       for (short i = 0; i < 10; i++) {
	             sheetRow.createCell(i).setCellValue(i * 10);
	       }
		*/
	}
}
