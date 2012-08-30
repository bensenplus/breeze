package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import org.breeze.core.web.Page;
import org.breeze.entity.ResourceUrl;
import org.breeze.service.ResourceUrlService;


@Controller
@RequestMapping("resourceUrl")
public class ResourceUrlController {

	private final Logger logger = LoggerFactory.getLogger(ResourceUrlController.class); 

    @Resource(name = "resourceUrlService")
	private ResourceUrlService resourceUrlService;


    @RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<ResourceUrl> list = resourceUrlService.select(page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "resourceUrl/list";
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Integer resourceId) {
	    ResourceUrl resourceUrl = resourceUrlService.get(resourceId);
		model.addAttribute("resourceUrl", resourceUrl);
		return "resourceUrl/edit";
	}    
    
    @RequestMapping("/save")
	public String save(ResourceUrl resourceUrl) {
       resourceUrlService.save(resourceUrl);
		return "redirect:list";
	}
    
    @RequestMapping("/delete")
	public String delete(Integer resourceId) {
		resourceUrlService.delete(resourceId);
		return "redirect:list";
	}
    
	
	
}