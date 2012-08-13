package org.mybatis.extend.interceptor;

public class MysqlDialect extends Dialect{

	@Override
	public String getLimitString(String sql, int offset, int limit) {

		StringBuffer pagingSelect = new StringBuffer(sql.length() + 100);		
		pagingSelect.append("select * from (");		
		pagingSelect.append(sql);		
		pagingSelect.append(" )  t limit ").append(offset).append(",").append(limit);
		return pagingSelect.toString();
	}

}