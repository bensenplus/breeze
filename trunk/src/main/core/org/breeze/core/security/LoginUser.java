package org.breeze.core.security;

import java.util.Collection;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.User;

class LoginUser extends User{

	private static final long serialVersionUID = -8114758011214814591L;

	private  Collection<String> menus;
	
	private String displayName;
	
	/**
	 * @param username
	 * @param authorities
	 */
	public LoginUser(String username,	Collection<GrantedAuthority> authorities, Collection<String> menus) {
		super(username, "", authorities);
		this.menus = menus;
	}
	/**
	 * @return the menus
	 */
	public Collection<String> getMenus() {
		return menus;
	}
	/**
	 * @return the displayName
	 */
	public String getDisplayName() {
		return displayName;
	}
	/**
	 * @param displayName the displayName to set
	 */
	public void setDisplayName(String displayName) {
		this.displayName = displayName;
	}
	
}
