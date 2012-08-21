package org.breeze.dao;

import java.util.List;

import org.breeze.entity.Page;
import org.breeze.entity.UserRole;

public interface UserRoleMapper {

	int count();
    
    List<UserRole> select(Page page);
    
    int insert(UserRole userRole);
    
    UserRole get(Integer id);
	
	int update(UserRole userRole);
	
	int delete(Integer id);
    

}