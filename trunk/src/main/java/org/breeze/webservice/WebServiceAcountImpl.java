package org.breeze.webservice;

import javax.jws.WebService;

import org.breeze.entity.Account;
import org.breeze.service.AccountService;
import org.springframework.beans.factory.annotation.Autowired;


@WebService(endpointInterface = "com.google.code.springbreeze.web.service.WebServiceAcount") 
public class WebServiceAcountImpl implements WebServiceAcount {

	@Autowired
	private AccountService accountService;
	
	@Override
	 public Account getAccountByUserID(Long userid) {
		Account account = accountService.getAccountByUserID(userid);
		return account;
	}
}
