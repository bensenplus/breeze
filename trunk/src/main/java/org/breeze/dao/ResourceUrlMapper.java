package org.breeze.dao;

import java.util.List;

import org.breeze.core.web.Page;
import org.breeze.entity.ResourceUrl;

public interface ResourceUrlMapper {

	int count();
    
    List<ResourceUrl> select(Page page);
    
    int insert(ResourceUrl resourceUrl);
    
    ResourceUrl get(Integer resourceId);
	
	int update(ResourceUrl resourceUrl);
	
	int delete(Integer resourceId);
    

}