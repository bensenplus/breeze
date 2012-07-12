package org.breeze.webservice;

import javax.jws.WebService;

import org.breeze.entity.Account;
import org.breeze.webservice.AccountService;
import org.springframework.beans.factory.annotation.Autowired;


@WebService(endpointInterface = "com.google.code.springbreeze.web.service.WebServiceAcount") 
public class AccountServiceImpl implements AccountService {

	@Autowired
	private AccountService accountService;
	
	@Override
	 public Account get(Long userid) {
		Account account = accountService.get(userid);
		return account;
	}
}
