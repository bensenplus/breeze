package org.breeze.dao;

import java.util.List;

import javax.annotation.Resource;

import org.apache.ibatis.session.RowBounds;
import org.apache.ibatis.session.SqlSessionFactory;
import org.breeze.entity.Account;
import org.springframework.stereotype.Repository;

@Repository("accountDao")
public class AccountDao {

	@Resource(name = "sqlSessionFactory")
	private SqlSessionFactory sqlSessionFactory;

	public List<Account> getAccountList(RowBounds rowBounds) {
		return sqlSessionFactory.openSession().selectList(
				"org.breeze.dao.AccountMapper.getAllAccount", null, rowBounds);
	}
}
