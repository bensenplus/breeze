package org.breeze.dao;

import java.util.List;

import org.breeze.entity.Page;
import org.breeze.entity.Account;

public interface AccountMapper {

	int count();
    
    List<Account> select(Page page);
    
    int insert(Account account);
    
    Account get(Long uSERID);
	
	int update(Account account);
	
	int delete(Long uSERID);
    

}