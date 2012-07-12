package org.breeze.webservice;

import javax.jws.WebService;

import org.breeze.entity.Account;


@WebService
public interface WebServiceAcount {


	/**
	 * @param hello
	 * @return
	 */
	 public Account getAccountByUserID(Long userid);

}
