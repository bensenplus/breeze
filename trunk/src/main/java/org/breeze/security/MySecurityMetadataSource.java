package org.breeze.security;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import javax.annotation.Resource;

import org.breeze.dao.ResourceUrlMapper;
import org.breeze.dao.RoleResourceMapper;
import org.breeze.entity.ResourceUrl;
import org.breeze.entity.RoleResource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.security.access.ConfigAttribute;
import org.springframework.security.access.SecurityConfig;
import org.springframework.security.web.FilterInvocation;
import org.springframework.security.web.access.intercept.FilterInvocationSecurityMetadataSource;
import org.springframework.stereotype.Service;

/**
 * 
 * 此类在初始化时，应该取到所有资源及其对应角色的定义
 * 
 * @author Robin
 * 
 */
//1 加载资源与权限的对应关系
@Service("mySecurityMetadataSource")
public class MySecurityMetadataSource implements FilterInvocationSecurityMetadataSource {

	final Logger logger = LoggerFactory.getLogger(MySecurityMetadataSource.class);
	
	@Resource(name="resourceUrlMapper")
	private ResourceUrlMapper resourceUrlMapper;
	
	@Resource(name="roleResourceMapper")
	private RoleResourceMapper roleResourceMapper;

	public MySecurityMetadataSource() {
		super();
	}

	public Collection<ConfigAttribute> getAllConfigAttributes() {
		logger.debug("getAllConfigAttributes");
		Collection<ConfigAttribute> configAttributeList= new ArrayList<ConfigAttribute>();
		List<ResourceUrl> resourceUrl = this.resourceUrlMapper.select(null );
		for (ResourceUrl resource : resourceUrl) {
			ConfigAttribute configAttribute = new SecurityConfig(resource.getUrl());
			configAttributeList.add(configAttribute);
		}
		return configAttributeList;
	}

	public boolean supports(Class<?> clazz) {
		return true;
	}

	//返回所请求资源所需要的权限
	@Override
	public Collection<ConfigAttribute> getAttributes(Object obj) throws IllegalArgumentException {
		
		Collection<ConfigAttribute> configAttributeList= new ArrayList<ConfigAttribute>();
		FilterInvocation fi = (FilterInvocation)obj;
	
		List<ResourceUrl> resourceUrl = this.resourceUrlMapper.select(null);
		 
		if (resourceUrl != null && resourceUrl.size() > 0) {
			List<RoleResource> rolesResource = roleResourceMapper.select(null);
			if (rolesResource != null && rolesResource.size() > 0) {
				for (RoleResource roleResource : rolesResource) {
					configAttributeList.add(new SecurityConfig(String.valueOf(roleResource.getRoleId())));
				}
			}
		}
		return configAttributeList;
	}

}