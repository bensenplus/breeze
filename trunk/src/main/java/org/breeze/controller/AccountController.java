
package org.breeze.controller;

import java.util.List;

import javax.annotation.Resource;

import org.breeze.entity.Account;
import org.breeze.entity.Page;
import org.breeze.service.AccountService;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping("/account")
public class AccountController {

	@Resource(name = "accountService")
	private AccountService accountService;

	@RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<Account> accountList = accountService.select(page);
		model.addAttribute("accountList", accountList);
		model.addAttribute("page", page);
		return "account/list";
	}
	
	@RequestMapping("/edit")
	public String edit(ModelMap model, Long userid) {
		Account account = accountService.get(userid);
		model.addAttribute("account", account);
		return "account/edit";
	}
	
	@RequestMapping("/copy")
	public String copy(ModelMap model, Long userid) {
		Account account = accountService.get(userid);
		model.addAttribute("account", account);
		return "account/edit";
	}
	
	@RequestMapping("/save")
	public String save(Account account) {
		accountService.updateAccount(account);
		return "redirect:list";
	}
	
	@RequestMapping("/delete")
	public String delete(Long userid) {
		accountService.delete(userid);
		return "redirect:list";
	}	
	
}
