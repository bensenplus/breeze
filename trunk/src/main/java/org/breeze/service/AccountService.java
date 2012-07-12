
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

	public List<Account> select(Page page) {
		page.setCount(accountMapper.count());
		RowBounds rowBounds = new RowBounds(page.getStartIndex(), page.getSizePerPage());
		return accountDao.select(rowBounds);
	}

	public Account get(Long userid) {
		return accountMapper.get(userid);
	}

	public Account get(String username, String password) {
		Account account = new Account();
		account.setPassword(password);
		return accountMapper.get(account);
	}

	public void create(Account account) {
		accountMapper.insert(account);
	}

	@Transactional
	public int updateAccount(Account account) {
		return accountMapper.update(account);
	}

	public int delete(Long userid) {
		return accountMapper.delete(userid);
	}
}
