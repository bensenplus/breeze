package org.breeze.entity;

import java.io.Serializable;
import java.util.Date;

public class ResourceUrl implements Serializable {

    private static final long serialVersionUID = 1L;
    
	private Integer resourceId;    
    
	private String name;    
    
	private String url;    


	public Integer getResourceId() {
		return resourceId;
	}

	public void setResourceId(Integer resourceId) {
		this.resourceId = resourceId;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}
}
