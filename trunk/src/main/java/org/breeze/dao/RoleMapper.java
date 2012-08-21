package org.breeze.dao;

import java.util.List;

import org.breeze.entity.Page;
import org.breeze.entity.Role;

public interface RoleMapper {

	int count();
    
    List<Role> select(Page page);
    
    int insert(Role role);
    
    Role get(Integer roleId);
	
	int update(Role role);
	
	int delete(Integer roleId);
    

}