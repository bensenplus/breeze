package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import javax.servlet.http.HttpServletResponse;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

import org.breeze.core.view.BaseController;
import org.breeze.core.view.document.ExcelView;
import org.breeze.core.view.document.PdfView;
import org.breeze.core.view.document.XmlView;
import org.breeze.entity.Prescription;
import org.breeze.service.PrescriptionService;


@Controller
@RequestMapping("prescription")
public class PrescriptionController extends BaseController {

	//private final Logger logger = LoggerFactory.getLogger(PrescriptionController.class); 

    @Resource(name = "prescriptionService")
	private PrescriptionService prescriptionService;
    
    
    @RequestMapping("/index")
    public String index(){
        return "prescription/index";
    }

    @RequestMapping("/search")
	public String search(ModelMap model, Prescription prescription,int page) {
        Pageable pageable = new PageRequest(0, 20);
		Page<Prescription> result = prescriptionService.findAll(pageable );
		model.addAttribute("result", result);
		return "prescription/list";
	}
    
    @RequestMapping("/excel")
	public ModelAndView excel(ModelMap model, Prescription prescription) {
		List<Prescription> list = prescriptionService.findAll();
		model.addAttribute("list", list);
    	ExcelView  excelView=new ExcelView(Prescription.class);
    	return new ModelAndView(excelView,model);
	}
    
    @RequestMapping("/pdf")
	public ModelAndView pdf(ModelMap model, Prescription prescription) {
		List<Prescription> list = prescriptionService.findAll();
		model.addAttribute("list", list);
    	PdfView  pdfView=new PdfView(Prescription.class);
    	return new ModelAndView(pdfView,model);
	}
    
    @RequestMapping(value="/{adviceId}.xml",method=RequestMethod.GET)
	public ModelAndView xml(ModelMap model, @PathVariable Long adviceId) {
        if(adviceId != null){
    	    Prescription prescription = prescriptionService.get(adviceId);
    		model.addAttribute("model", prescription);
        }
        XmlView  xmlView=new XmlView(Prescription.class);
    	return new ModelAndView(xmlView, model);
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Long adviceId) {
        if(adviceId != null){
    	    Prescription prescription = prescriptionService.get(adviceId);
    		model.addAttribute("prescription", prescription);
        }
		return "prescription/edit";
	}    
    
    @RequestMapping("/save")
	public void save(Prescription prescription, HttpServletResponse response) {
       prescriptionService.save(prescription);
       response.setHeader("ContentType", "text/json");
    }
    
    @RequestMapping("/remove")
	public void remove(Long adviceId, HttpServletResponse response) {
		prescriptionService.remove(adviceId);
        response.setHeader("ContentType", "text/json");
	}
	
}