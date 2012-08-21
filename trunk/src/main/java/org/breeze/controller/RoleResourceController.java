package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import org.breeze.entity.Page;
import org.breeze.entity.RoleResource;
import org.breeze.service.RoleResourceService;


@Controller
@RequestMapping("roleResource")
public class RoleResourceController {

	private final Logger logger = LoggerFactory.getLogger(RoleResourceController.class); 

    @Resource(name = "roleResourceService")
	private RoleResourceService roleResourceService;


    @RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<RoleResource> list = roleResourceService.select(page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "roleResource/list";
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Integer id) {
	    RoleResource roleResource = roleResourceService.get(id);
		model.addAttribute("roleResource", roleResource);
		return "roleResource/edit";
	}    
    
    @RequestMapping("/save")
	public String save(RoleResource roleResource) {
       roleResourceService.save(roleResource);
		return "redirect:list";
	}
    
    @RequestMapping("/delete")
	public String delete(Integer id) {
		roleResourceService.delete(id);
		return "redirect:list";
	}
    
	
	
}