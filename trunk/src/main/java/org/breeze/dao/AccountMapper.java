package org.breeze.dao;

import java.util.List;

import org.breeze.entity.Account;
import org.breeze.entity.Page;


public interface AccountMapper {

	List<Account> getAllAccount(Page page);
	int getAllAccountCount();
	
	Account getAccountByUsernameAndPassword(Account account);
	
	Account getAccountByUserID(Long userid);

	void insertAccount(Account account);
	
	int updateAccount(Account account);
	
	int deleteAccountByUserID(Long userid);

}
