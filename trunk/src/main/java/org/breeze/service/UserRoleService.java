package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.ui.ModelMap;

import org.breeze.entity.Page;
import org.breeze.entity.UserRole;
import org.breeze.dao.UserRoleMapper;


@Service("userRoleService")
public class UserRoleService {

	private final Logger logger = LoggerFactory.getLogger(UserRoleService.class); 

    @Resource(name = "userRoleMapper")
	private UserRoleMapper userRoleMapper;


	public List<UserRole> select(Page page) {
        page.setCount(userRoleMapper.count());
		List<UserRole> list = userRoleMapper.select(page);
		return list;
	}

	public UserRole get(Integer id) {
	    UserRole userRole = userRoleMapper.get(id);
        return userRole;
	}    
    
	public int save(UserRole userRole) {
    
       if(userRole.getId() == null) {
            return userRoleMapper.insert(userRole);
       }else{
            return userRoleMapper.update(userRole);
       }
	}

	public int delete(Integer id) {
		return userRoleMapper.delete(id);
	}

}