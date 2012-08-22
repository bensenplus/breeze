package org.breeze.core.security;
import java.util.Collection;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import javax.annotation.Resource;

import org.breeze.dao.UserMapper;
import org.breeze.dao.UserRoleMapper;
import org.breeze.entity.User;
import org.breeze.entity.UserRole;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service("myUserDetailService")
public class MyUserDetailService implements UserDetailsService {  
    
	final Logger logger = LoggerFactory.getLogger(MyUserDetailService.class);
	
	@Resource(name="userMapper")
    private UserMapper userMapper;  
	@Resource(name="userRoleMapper")
    private UserRoleMapper userRoleMapper; 
	
	public MyUserDetailService() {
		super();
	}

	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {  
    	logger.debug("username is " + username);  
		User user = this.userMapper.select(null).get(0);
        if(user == null) {  
            throw new UsernameNotFoundException(username);  
        }  
        Collection<GrantedAuthority> grantedAuths = obtionGrantedAuthorities(user);  
          
        boolean enables = true;  
        boolean accountNonExpired = true;  
        boolean credentialsNonExpired = true;  
        boolean accountNonLocked = true;  
          
        UserDetails userdetail = new org.springframework.security.core.userdetails.User(
        		user.getAccount(), 
        		user.getPassword(), 
        		enables, 
        		accountNonExpired, 
        		credentialsNonExpired, 
        		accountNonLocked, 
        		grantedAuths);  
        return userdetail;  
    }  
      
    //取得用户的权限  
    private Set<GrantedAuthority> obtionGrantedAuthorities(User user) {  
        Set<GrantedAuthority> authSet = new HashSet<GrantedAuthority>();  
        
        List<UserRole>  userRoleList = userRoleMapper.select(null);
          
        for(UserRole userRole : userRoleList) {  
                authSet.add(new SimpleGrantedAuthority(String.valueOf(userRole.getRoleId())));  
        }  
        return authSet;  
    }  
}  