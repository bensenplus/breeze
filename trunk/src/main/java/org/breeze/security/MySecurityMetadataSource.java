package org.breeze.security;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import javax.annotation.Resource;

import org.breeze.dao.ResourcesMapper;
import org.breeze.dao.RolesResourcesMapper;
import org.breeze.entity.Resources;
import org.breeze.entity.ResourcesExample;
import org.breeze.entity.RolesResources;
import org.breeze.entity.RolesResourcesExample;
import org.mortbay.log.Log;
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
	
	@Resource(name="resourcesMapper")
	private ResourcesMapper resourcesMapper;
	
	@Resource(name="rolesResourcesMapper")
	private RolesResourcesMapper rolesResourcesMapper;

	public MySecurityMetadataSource() {
		super();
	}

	public Collection<ConfigAttribute> getAllConfigAttributes() {
		logger.debug("getAllConfigAttributes");
		Collection<ConfigAttribute> configAttributeList= new ArrayList<ConfigAttribute>();
		List<Resources> resources = this.resourcesMapper.selectByExample(null );
		for (Resources resource : resources) {
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
		
		ResourcesExample example1 = new ResourcesExample() ;
		example1.createCriteria().andUrlEqualTo(fi.getHttpRequest().getServletPath());
		List<Resources> resources = this.resourcesMapper.selectByExample(example1);
		 
		if (resources != null && resources.size() > 0) {
			RolesResourcesExample example2 = new RolesResourcesExample();
			example2.createCriteria().andRsidEqualTo(resources.get(0).getId());
			List<RolesResources> rolesResources = rolesResourcesMapper.selectByExample(example2);
			if (rolesResources != null && rolesResources.size() > 0) {
				for (RolesResources roleResource : rolesResources) {
					configAttributeList.add(new SecurityConfig(String.valueOf(roleResource.getRid())));
				}
			}
		}
		return configAttributeList;
	}

}