package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.ui.ModelMap;

import org.breeze.entity.Page;
import org.breeze.entity.RoleResource;
import org.breeze.dao.RoleResourceMapper;


@Service("roleResourceService")
public class RoleResourceService {

	private final Logger logger = LoggerFactory.getLogger(RoleResourceService.class); 

    @Resource(name = "roleResourceMapper")
	private RoleResourceMapper roleResourceMapper;


	public List<RoleResource> select(Page page) {
        page.setCount(roleResourceMapper.count());
		List<RoleResource> list = roleResourceMapper.select(page);
		return list;
	}

	public RoleResource get(Integer id) {
	    RoleResource roleResource = roleResourceMapper.get(id);
        return roleResource;
	}    
    
	public int save(RoleResource roleResource) {
    
       if(roleResource.getId() == null) {
            return roleResourceMapper.insert(roleResource);
       }else{
            return roleResourceMapper.update(roleResource);
       }
	}

	public int delete(Integer id) {
		return roleResourceMapper.delete(id);
	}

}