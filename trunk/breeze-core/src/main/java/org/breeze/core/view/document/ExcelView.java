package org.breeze.core.view.document;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Date;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFCellStyle;
import org.apache.poi.hssf.usermodel.HSSFFont;
import org.apache.poi.hssf.usermodel.HSSFSheet;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.hssf.util.HSSFColor;
import org.breeze.core.view.Util;
import org.springframework.web.servlet.view.document.AbstractExcelView;

public class ExcelView extends AbstractExcelView {

	private  Class<?> modeClass;
	
	public ExcelView(Class<?> modeClass){
		super();
		this.modeClass = modeClass;
	}
	

	@Override
	protected void buildExcelDocument(Map<String, Object> model, HSSFWorkbook workbook,
			HttpServletRequest request, HttpServletResponse response) throws Exception {
		
		response.setHeader("Content-disposition", "attachment;filename=" + modeClass.getSimpleName()+".xls");
		HSSFSheet sheet = workbook.createSheet(modeClass.getSimpleName());
		
		sheet.setDisplayGridlines(false);
		sheet.setDefaultRowHeightInPoints(20);
		sheet.setDisplayGuts(true);
		int m=0, n =0;
		Field[] fields = modeClass.getDeclaredFields();

		//head style
		HSSFCellStyle style = workbook.createCellStyle();
		style.setFillPattern(HSSFCellStyle.SOLID_FOREGROUND );// 设置背景色
		style.setFillForegroundColor(HSSFColor.BLUE_GREY.index);		
		style.setAlignment(HSSFCellStyle.ALIGN_CENTER); // 居中
		style.setBorderBottom(HSSFCellStyle.BORDER_THIN); //下边框
		style.setBorderLeft(HSSFCellStyle.BORDER_THIN);//左边框
		style.setBorderTop(HSSFCellStyle.BORDER_THIN);//上边框
		style.setBorderRight(HSSFCellStyle.BORDER_THIN);//右边框
		HSSFFont font = workbook.createFont();
		font.setFontName("宋体");
		font.setBoldweight(HSSFFont.BOLDWEIGHT_BOLD);
		font.setFontHeightInPoints((short)12);//设置字体大小
		font.setColor(HSSFColor.WHITE.index);
		style.setFont(font);
		style.setWrapText(true);//设置自动换行
		
		//body style
		HSSFCellStyle style2 = workbook.createCellStyle();
		style2.setWrapText(true);//设置自动换行
		style2.setBorderBottom(HSSFCellStyle.BORDER_THIN); //下边框
		style2.setBorderLeft(HSSFCellStyle.BORDER_THIN);//左边框
		style2.setBorderTop(HSSFCellStyle.BORDER_THIN);//上边框
		style2.setBorderRight(HSSFCellStyle.BORDER_THIN);//右边框
		
		for (Field field : fields) {
			  HSSFCell cell = getCell(sheet, m, n);
			  cell.setCellStyle(style);
			  sheet.setColumnWidth(n, 6000);
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
						  cell.setCellStyle(style2);
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
	}
}
