package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import org.breeze.entity.Page;
import org.breeze.entity.UserRole;
import org.breeze.service.UserRoleService;


@Controller
@RequestMapping("userRole")
public class UserRoleController {

	private final Logger logger = LoggerFactory.getLogger(UserRoleController.class); 

    @Resource(name = "userRoleService")
	private UserRoleService userRoleService;


    @RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<UserRole> list = userRoleService.select(page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "userRole/list";
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Integer id) {
	    UserRole userRole = userRoleService.get(id);
		model.addAttribute("userRole", userRole);
		return "userRole/edit";
	}    
    
    @RequestMapping("/save")
	public String save(UserRole userRole) {
       userRoleService.save(userRole);
		return "redirect:list";
	}
    
    @RequestMapping("/delete")
	public String delete(Integer id) {
		userRoleService.delete(id);
		return "redirect:list";
	}
    
	
	
}