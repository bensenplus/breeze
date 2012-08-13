package org.breeze.security;
import java.util.Collection;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import javax.annotation.Resource;

import org.breeze.dao.UsersMapper;
import org.breeze.dao.UsersRolesMapper;
import org.breeze.entity.Users;
import org.breeze.entity.UsersExample;
import org.breeze.entity.UsersRoles;
import org.breeze.entity.UsersRolesExample;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service("myUserDetailService")
public class MyUserDetailService implements UserDetailsService {  
    
	final Logger logger = LoggerFactory.getLogger(MyUserDetailService.class);
	
	@Resource(name="usersMapper")
    private UsersMapper usersDao;  
	@Resource(name="usersRolesMapper")
    private UsersRolesMapper usersRolesDao; 
	
	public MyUserDetailService() {
		super();
	}

	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {  
    	logger.debug("username is " + username);  
        UsersExample example = new UsersExample();
        example.createCriteria().andAccountEqualTo(username);
		Users users = this.usersDao.selectByExample(example).get(0);
        if(users == null) {  
            throw new UsernameNotFoundException(username);  
        }  
        Collection<GrantedAuthority> grantedAuths = obtionGrantedAuthorities(users);  
          
        boolean enables = true;  
        boolean accountNonExpired = true;  
        boolean credentialsNonExpired = true;  
        boolean accountNonLocked = true;  
          
        User userdetail = new User(users.getAccount(), users.getPassword(), enables, accountNonExpired, credentialsNonExpired, accountNonLocked, grantedAuths);  
        return userdetail;  
    }  
      
    //取得用户的权限  
    private Set<GrantedAuthority> obtionGrantedAuthorities(Users user) {  
        Set<GrantedAuthority> authSet = new HashSet<GrantedAuthority>();  
        
        UsersRolesExample example = new UsersRolesExample() ;
        example.createCriteria().andUidEqualTo(user.getId());
        List<UsersRoles>  usersRoles = usersRolesDao.selectByExample(example);
          
        for(UsersRoles userRole : usersRoles) {  
                authSet.add(new SimpleGrantedAuthority(String.valueOf(userRole.getRid())));  
        }  
        return authSet;  
    }  
}  