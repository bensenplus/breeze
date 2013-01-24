package org.breeze.controller;

import java.util.List;

import javax.annotation.Resource;
import javax.servlet.http.HttpServletResponse;

import org.breeze.core.view.BaseController;
import org.breeze.core.view.Page;
import org.breeze.core.view.document.ExcelView;
import org.breeze.core.view.document.PdfView;
import org.breeze.core.view.document.XmlView;
import org.breeze.entity.Clinic;
import org.breeze.service.ClinicService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;


@Controller
@RequestMapping("clinic")
public class ClinicController extends BaseController {

	private final Logger logger = LoggerFactory.getLogger(ClinicController.class); 

    @Resource(name = "clinicService")
	private ClinicService clinicService;
    
    
    @RequestMapping("/index")
    public String index(){
        return "clinic/index";
    }

    @RequestMapping("/search")
	public String search(ModelMap model, Clinic clinic,Page page) {
		page.setCount(clinicService.countBy(clinic));
		List<Clinic> list = clinicService.selectBy(clinic, page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "clinic/list";
	}
    
    @RequestMapping("/excel")
	public ModelAndView excel(ModelMap model, Clinic clinic) {
		List<Clinic> list = clinicService.selectBy(clinic, null);
		model.addAttribute("list", list);
    	ExcelView  excelView=new ExcelView(Clinic.class);
    	return new ModelAndView(excelView,model);
	}
    
    @RequestMapping("/pdf")
	public ModelAndView pdf(ModelMap model, Clinic clinic) {
		List<Clinic> list = clinicService.selectBy(clinic, null);
		model.addAttribute("list", list);
    	PdfView  pdfView=new PdfView(Clinic.class);
    	return new ModelAndView(pdfView,model);
	}
    
    @RequestMapping(value="/{id}.xml",method=RequestMethod.GET)
	public ModelAndView xml(ModelMap model, @PathVariable String id) {
        if(id != null){
    	    Clinic clinic = clinicService.get(id);
    		model.addAttribute("model", clinic);
        }
        XmlView  xmlView=new XmlView(Clinic.class);
    	return new ModelAndView(xmlView, model);
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, String id) {
        if(id != null){
    	    Clinic clinic = clinicService.get(id);
    		model.addAttribute("clinic", clinic);
        }
		return "clinic/edit";
	}    
    
    @RequestMapping("/save")
	public void save(Clinic clinic, HttpServletResponse response) {
       clinicService.save(clinic);
       response.setHeader("ContentType", "text/json");
    }
    
    @RequestMapping("/remove")
	public void remove(String id, HttpServletResponse response) {
		clinicService.remove(id);
        response.setHeader("ContentType", "text/json");
	}
	
}