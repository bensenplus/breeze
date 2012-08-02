package org.breeze.dao;

import java.util.List;

import javax.annotation.Resource;

import org.apache.ibatis.session.RowBounds;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.breeze.entity.Account;
import org.springframework.stereotype.Repository;

@Repository("accountDao")
public class AccountDao {

	@Resource(name = "sqlSessionFactory")
	private SqlSessionFactory sqlSessionFactory;

	public List<Account> select(RowBounds rowBounds) {
		SqlSession session = sqlSessionFactory.openSession();
		List<Account>  list =  session.selectList("org.breeze.dao.AccountMapper.select", null, rowBounds);
		session.close();
		return list;
	}
}
