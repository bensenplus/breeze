package org.breeze.entity;

import java.io.Serializable;
import java.util.Date;

public class Role implements Serializable {

    private static final long serialVersionUID = 1L;
    
	private Integer roleId;    
    
	private Integer enable;    
    
	private String name;    


	public Integer getRoleId() {
		return roleId;
	}

	public void setRoleId(Integer roleId) {
		this.roleId = roleId;
	}

	public Integer getEnable() {
		return enable;
	}

	public void setEnable(Integer enable) {
		this.enable = enable;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
}
