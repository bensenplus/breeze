
package org.breeze.core.security;

import java.util.ArrayList;
import java.util.Collection;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service("userDetailsService")
public class UserDetailsServiceImpl implements UserDetailsService{

	@Override
	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
		
		Collection<GrantedAuthority> authoritys=  getAuthorityList(username);
		Collection<String> menus=  getMenus(username);		
		UserDetails loginUser = new LoginUser(username,authoritys, menus);
		return loginUser ;
	}
	
	private Collection<GrantedAuthority>  getAuthorityList(String username){
		ArrayList<GrantedAuthority>  authorityList  = new ArrayList<GrantedAuthority>();
		GrantedAuthority grantedAuthority  = new SimpleGrantedAuthority("ROLE_USER");
		authorityList.add(grantedAuthority);
		return authorityList;
	}
	
	private Collection<String>  getMenus(String username){
		Collection<String> menus = new ArrayList<String>();
		//TODO
		return menus;
	}
	
}
