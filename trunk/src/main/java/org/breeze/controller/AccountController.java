package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import org.breeze.entity.Page;
import org.breeze.entity.Account;
import org.breeze.service.AccountService;


@Controller
@RequestMapping("account")
public class AccountController {

	private final Logger logger = LoggerFactory.getLogger(AccountController.class); 

    @Resource(name = "accountService")
	private AccountService accountService;


    @RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<Account> list = accountService.select(page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "account/list";
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Long userid) {
	    Account account = accountService.get(userid);
		model.addAttribute("account", account);
		return "account/edit";
	}    
    
    @RequestMapping("/save")
	public String save(Account account) {
       accountService.save(account);
		return "redirect:list";
	}
    
    @RequestMapping("/delete")
	public String delete(Long userid) {
		accountService.delete(userid);
		return "redirect:list";
	}
    
	
	
}