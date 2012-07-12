package org.breeze.dao;

import org.breeze.entity.Account;

public interface AccountMapper {

	int count();
	
	Account get(Account account);
	
	Account get(Long userid);

	int insert(Account account);
	
	int update(Account account);
	
	int delete(Long userid);

}
