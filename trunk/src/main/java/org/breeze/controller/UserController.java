package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import org.breeze.core.web.Page;
import org.breeze.entity.User;
import org.breeze.service.UserService;


@Controller
@RequestMapping("user")
public class UserController {

	private final Logger logger = LoggerFactory.getLogger(UserController.class); 

    @Resource(name = "userService")
	private UserService userService;


    @RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<User> list = userService.select(page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "user/list";
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Integer userId) {
	    User user = userService.get(userId);
		model.addAttribute("user", user);
		return "user/edit";
	}    
    
    @RequestMapping("/save")
	public String save(User user) {
       userService.save(user);
		return "redirect:list";
	}
    
    @RequestMapping("/delete")
	public String delete(Integer userId) {
		userService.delete(userId);
		return "redirect:list";
	}
    
	
	
}