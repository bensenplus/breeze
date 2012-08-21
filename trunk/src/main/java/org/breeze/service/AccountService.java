package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.ui.ModelMap;

import org.breeze.entity.Page;
import org.breeze.entity.Account;
import org.breeze.dao.AccountMapper;


@Service("accountService")
public class AccountService {

	private final Logger logger = LoggerFactory.getLogger(AccountService.class); 

    @Resource(name = "accountMapper")
	private AccountMapper accountMapper;


	public List<Account> select(Page page) {
        page.setCount(accountMapper.count());
		List<Account> list = accountMapper.select(page);
		return list;
	}

	public Account get(Long userid) {
	    Account account = accountMapper.get(userid);
        return account;
	}    
    
	public int save(Account account) {
    
       if(account.getUserid() == null) {
            return accountMapper.insert(account);
       }else{
            return accountMapper.update(account);
       }
	}

	public int delete(Long userid) {
		return accountMapper.delete(userid);
	}

}