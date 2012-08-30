package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.ui.ModelMap;

import org.breeze.core.web.Page;
import org.breeze.entity.User;
import org.breeze.dao.UserMapper;


@Service("userService")
public class UserService {

	private final Logger logger = LoggerFactory.getLogger(UserService.class); 

    @Resource(name = "userMapper")
	private UserMapper userMapper;


	public List<User> select(Page page) {
        page.setCount(userMapper.count());
		List<User> list = userMapper.select(page);
		return list;
	}

	public User get(Integer userId) {
	    User user = userMapper.get(userId);
        return user;
	}    
    
	public int save(User user) {
    
       if(user.getUserId() == null) {
            return userMapper.insert(user);
       }else{
            return userMapper.update(user);
       }
	}

	public int delete(Integer userId) {
		return userMapper.delete(userId);
	}

}