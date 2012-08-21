package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import org.breeze.entity.Page;
import org.breeze.entity.Role;
import org.breeze.service.RoleService;


@Controller
@RequestMapping("role")
public class RoleController {

	private final Logger logger = LoggerFactory.getLogger(RoleController.class); 

    @Resource(name = "roleService")
	private RoleService roleService;


    @RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<Role> list = roleService.select(page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "role/list";
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Integer roleId) {
	    Role role = roleService.get(roleId);
		model.addAttribute("role", role);
		return "role/edit";
	}    
    
    @RequestMapping("/save")
	public String save(Role role) {
       roleService.save(role);
		return "redirect:list";
	}
    
    @RequestMapping("/delete")
	public String delete(Integer roleId) {
		roleService.delete(roleId);
		return "redirect:list";
	}
    
	
	
}