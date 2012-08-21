package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.ui.ModelMap;

import org.breeze.entity.Page;
import org.breeze.entity.ResourceUrl;
import org.breeze.dao.ResourceUrlMapper;


@Service("resourceUrlService")
public class ResourceUrlService {

	private final Logger logger = LoggerFactory.getLogger(ResourceUrlService.class); 

    @Resource(name = "resourceUrlMapper")
	private ResourceUrlMapper resourceUrlMapper;


	public List<ResourceUrl> select(Page page) {
        page.setCount(resourceUrlMapper.count());
		List<ResourceUrl> list = resourceUrlMapper.select(page);
		return list;
	}

	public ResourceUrl get(Integer resourceId) {
	    ResourceUrl resourceUrl = resourceUrlMapper.get(resourceId);
        return resourceUrl;
	}    
    
	public int save(ResourceUrl resourceUrl) {
    
       if(resourceUrl.getResourceId() == null) {
            return resourceUrlMapper.insert(resourceUrl);
       }else{
            return resourceUrlMapper.update(resourceUrl);
       }
	}

	public int delete(Integer resourceId) {
		return resourceUrlMapper.delete(resourceId);
	}

}