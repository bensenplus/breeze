package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.ui.ModelMap;

import org.breeze.entity.Page;
import org.breeze.entity.Role;
import org.breeze.dao.RoleMapper;


@Service("roleService")
public class RoleService {

	private final Logger logger = LoggerFactory.getLogger(RoleService.class); 

    @Resource(name = "roleMapper")
	private RoleMapper roleMapper;


	public List<Role> select(Page page) {
        page.setCount(roleMapper.count());
		List<Role> list = roleMapper.select(page);
		return list;
	}

	public Role get(Integer roleId) {
	    Role role = roleMapper.get(roleId);
        return role;
	}    
    
	public int save(Role role) {
    
       if(role.getRoleId() == null) {
            return roleMapper.insert(role);
       }else{
            return roleMapper.update(role);
       }
	}

	public int delete(Integer roleId) {
		return roleMapper.delete(roleId);
	}

}