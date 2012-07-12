
package org.breeze.service;

import java.util.List;

import javax.annotation.Resource;

import org.apache.ibatis.session.RowBounds;
import org.breeze.dao.AccountDao;
import org.breeze.dao.AccountMapper;
import org.breeze.entity.Account;
import org.breeze.entity.Page;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service("accountService")
public class AccountService {

	@Resource(name = "accountMapper")
	private AccountMapper accountMapper;

	@Resource(name = "accountDao")
	private AccountDao accountDao;

	public List<Account> getAllAccount(Page page) {
		page.setCount(accountMapper.getAllAccountCount());
		RowBounds rowBounds = new RowBounds(page.getStartIndex(), page.getSizePerPage());
		return accountDao.getAccountList(rowBounds);
	}

	public Account getAccountByUserID(Long userid) {
		return accountMapper.getAccountByUserID(userid);
	}

	public Account getAccount(String username, String password) {
		Account account = new Account();
		account.setPassword(password);
		return accountMapper.getAccountByUsernameAndPassword(account);
	}

	public void insertAccount(Account account) {
		accountMapper.insertAccount(account);
	}

	@Transactional
	public int updateAccount(Account account) {
		return accountMapper.updateAccount(account);
	}

	public int deleteAccountByUserID(Long userid) {
		return accountMapper.deleteAccountByUserID(userid);
	}
}
