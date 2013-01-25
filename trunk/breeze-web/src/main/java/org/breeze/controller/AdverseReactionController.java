package org.breeze.controller;

import java.util.Date;
import java.util.List;
import javax.annotation.Resource;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
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
import org.breeze.core.view.Page;
import org.breeze.core.view.Util;
import org.breeze.entity.AdverseReaction;
import org.breeze.service.AdverseReactionService;


@Controller
@RequestMapping("adverseReaction")
public class AdverseReactionController extends BaseController {

	private final Logger logger = LoggerFactory.getLogger(AdverseReactionController.class); 

    @Resource(name = "adverseReactionService")
	private AdverseReactionService adverseReactionService;
    
    
    @RequestMapping("/index")
    public String index(){
        return "adverseReaction/index";
    }

    @RequestMapping("/search")
	public String search(ModelMap model, AdverseReaction adverseReaction,Page page) {
		page.setCount(adverseReactionService.countBy(adverseReaction));
		List<AdverseReaction> list = adverseReactionService.selectBy(adverseReaction, page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "adverseReaction/list";
	}
    
    @RequestMapping("/excel")
	public ModelAndView excel(ModelMap model, AdverseReaction adverseReaction) {
		List<AdverseReaction> list = adverseReactionService.selectBy(adverseReaction, null);
		model.addAttribute("list", list);
    	ExcelView  excelView=new ExcelView(AdverseReaction.class);
    	return new ModelAndView(excelView,model);
	}
    
    @RequestMapping("/pdf")
	public ModelAndView pdf(ModelMap model, AdverseReaction adverseReaction) {
		List<AdverseReaction> list = adverseReactionService.selectBy(adverseReaction, null);
		model.addAttribute("list", list);
    	PdfView  pdfView=new PdfView(AdverseReaction.class);
    	return new ModelAndView(pdfView,model);
	}
    
    @RequestMapping(value="/{id}.xml",method=RequestMethod.GET)
	public ModelAndView xml(ModelMap model, @PathVariable String id) {
        if(id != null){
    	    AdverseReaction adverseReaction = adverseReactionService.get(id);
    		model.addAttribute("model", adverseReaction);
        }
        XmlView  xmlView=new XmlView(AdverseReaction.class);
    	return new ModelAndView(xmlView, model);
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, String id) {
        if(id != null){
    	    AdverseReaction adverseReaction = adverseReactionService.get(id);
    		model.addAttribute("adverseReaction", adverseReaction);
        }
		return "adverseReaction/edit";
	}    
    
    @RequestMapping("/save")
	public void save(AdverseReaction adverseReaction, HttpServletResponse response) {
       adverseReactionService.save(adverseReaction);
       response.setHeader("ContentType", "text/json");
    }
    
    @RequestMapping("/remove")
	public void remove(String id, HttpServletResponse response) {
		adverseReactionService.remove(id);
        response.setHeader("ContentType", "text/json");
	}
	
}