package org.breeze.dao;

import java.util.List;

import org.breeze.core.web.Page;
import org.breeze.entity.RoleResource;

public interface RoleResourceMapper {

	int count();
    
    List<RoleResource> select(Page page);
    
    int insert(RoleResource roleResource);
    
    RoleResource get(Integer id);
	
	int update(RoleResource roleResource);
	
	int delete(Integer id);
    

}