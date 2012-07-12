package org.breeze.webservice;

import javax.jws.WebService;

import org.breeze.entity.Account;


@WebService
public interface AccountService {


	/**
	 * @param hello
	 * @return
	 */
	 public Account get(Long userid);

}
