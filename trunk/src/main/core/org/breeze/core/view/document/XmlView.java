package org.breeze.core.view.document;

import java.beans.XMLEncoder;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.view.AbstractView;

public class XmlView extends AbstractView {

	private static final String EXTENSION = ".xml";
	private static final String CONTENT_TYPE = "html/xml";
	
	private  Class<?> modeClass;
	
	public XmlView(Class<?> modeClass) {
		this.modeClass = modeClass;
		setContentType(CONTENT_TYPE);
	}
	
	@Override
	protected void renderMergedOutputModel(Map<String, Object> model, HttpServletRequest request,
			HttpServletResponse response) throws Exception {
		XMLEncoder encoder = new XMLEncoder(response.getOutputStream());
		encoder.writeObject(model.get("model"));
		encoder.close(); 
	}	
	
}
