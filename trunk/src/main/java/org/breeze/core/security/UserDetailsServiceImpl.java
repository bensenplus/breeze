
package org.breeze.core.security;

import java.util.ArrayList;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service("userDetailsService")
public class UserDetailsServiceImpl implements UserDetailsService{

	@Override
	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
		GrantedAuthority grantedAuthority  = new SimpleGrantedAuthority("ROLE_USER");
		ArrayList<GrantedAuthority>  authorityList  = new ArrayList<GrantedAuthority>();
		authorityList.add(grantedAuthority);
		UserDetails userDetails = new User(username, "", authorityList);
		return userDetails ;
	}
	
}
