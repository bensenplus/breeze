package org.breeze.core.web;

import java.awt.Color;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Date;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.view.document.AbstractPdfView;

import com.lowagie.text.Cell;
import com.lowagie.text.Document;
import com.lowagie.text.Element;
import com.lowagie.text.Font;
import com.lowagie.text.PageSize;
import com.lowagie.text.Paragraph;
import com.lowagie.text.Table;
import com.lowagie.text.pdf.BaseFont;
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
		response.setHeader("Content-disposition", "attachment;filename=" + modeClass.getSimpleName()+".pdf");
		document.setPageSize(PageSize.A3.rotate());
		document.setMargins(20, 20, 20, 20);
		document.open();
		
		//Chinese font
		BaseFont bfChinese = BaseFont.createFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);  
		Font FontChinese = new Font(bfChinese, 12, Font.NORMAL); 		
		document.add(new Paragraph(" 产生的报告",FontChinese));  
		
		Field[] fields = modeClass.getDeclaredFields();
		Table table = new Table(fields.length);

        table.getDefaultCell().setUseAscender(false);
        table.getDefaultCell().setUseDescender(true);
        table.getDefaultCell().setBackgroundColor(Color.LIGHT_GRAY);
        table.getDefaultCell().setVerticalAlignment(Element.ALIGN_MIDDLE);
        table.getDefaultCell().setHorizontalAlignment(Element.ALIGN_CENTER);
        table.getDefaultCell().setMaxLines(1);
		
		for (Field field : fields) {
			table.addCell(field.getName());
		}
		

		
		table.getDefaultCell().setBackgroundColor(Color.WHITE);
	    table.getDefaultCell().setHorizontalAlignment(Element.ALIGN_LEFT);
		  
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
								  table.addCell(new Cell(new Paragraph(value.toString(), FontChinese)));
							  }
						  }else{
							  table.addCell("");
						  }
					}
			}
		}
		table.setWidth(100);
		table.setCellsFitPage(true); 
		table.setConvert2pdfptable(true); 
		document.add(table);
	}
}
