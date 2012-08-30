package org.breeze.dao;

import java.util.List;

import org.breeze.core.web.Page;
import org.breeze.entity.User;

public interface UserMapper {

	int count();
    
    List<User> select(Page page);
    
    int insert(User user);
    
    User get(Integer userId);
	
	int update(User user);
	
	int delete(Integer userId);
    

}