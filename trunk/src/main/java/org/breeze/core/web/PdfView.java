package org.breeze.core.web;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Date;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFCellStyle;
import org.springframework.web.servlet.view.document.AbstractPdfView;

import com.lowagie.text.Cell;
import com.lowagie.text.Document;
import com.lowagie.text.Paragraph;
import com.lowagie.text.Rectangle;
import com.lowagie.text.Table;
import com.lowagie.text.pdf.PdfPCell;
import com.lowagie.text.pdf.PdfWriter;


public class PdfView extends AbstractPdfView{
	
	private  Class<?> modeClass;
	
	public PdfView(Class<?> modeClass){
		super();
		this.modeClass = modeClass;
	}

	@Override
	protected void buildPdfDocument(Map<String, Object> model, Document document, PdfWriter writer,
			HttpServletRequest request, HttpServletResponse response) throws Exception {

		Field[] fields = modeClass.getDeclaredFields();
		Table table = new Table(fields.length);
		table.setCellsFitPage(false);
		//document.setPageSize(new Rectangle(fields.length*50,1000));
		table.setBorderWidth(1);		
		for (Field field : fields) {
			table.addCell(field.getName());
		}
		
		 Cell cell = new Cell();
		 cell.setWidth(10000);
		 cell.setMaxLines(1);
		 table.setDefaultCell(cell);

		 ArrayList<?>  list = (ArrayList<?>)model.get("list");
		if (list != null){
			for(Object object: list){
					for (Field field : fields) {
						  field.setAccessible(true);
						  Object value = field.get(object);
						  if(value!=null){
							  if(value instanceof  Date){
								  table.addCell(Util.formatDate("yyyy/MM/dd hh:mm:ss", (Date)value));
							  }else{
								  table.addCell(value.toString());
							  }
						  }else{
							  table.addCell("");
						  }
					}
			}
		}
		document.add(table);
	}
}
