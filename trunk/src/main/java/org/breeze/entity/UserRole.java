package org.breeze.entity;

import java.io.Serializable;
import java.util.Date;

public class UserRole implements Serializable {

    private static final long serialVersionUID = 1L;
    
	private Integer id;    
    
	private Integer userId;    
    
	private Integer rolId;    


	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public Integer getUserId() {
		return userId;
	}

	public void setUserId(Integer userId) {
		this.userId = userId;
	}

	public Integer getRolId() {
		return rolId;
	}

	public void setRolId(Integer rolId) {
		this.rolId = rolId;
	}
}
