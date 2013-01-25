package org.breeze.service;

import java.util.Date;
import java.util.HashMap;
import java.util.List;
import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import org.breeze.core.view.Util;
import org.breeze.core.view.Page;
import org.breeze.entity.AdverseReaction;
import org.breeze.repository.jpa.AdverseReactionRepository;
import org.breeze.repository.mapper.AdverseReactionMapper;


@Service("adverseReactionService")
public class AdverseReactionService {

	private final Logger logger = LoggerFactory.getLogger(AdverseReactionService.class); 

    @Resource(name = "adverseReactionRepository")
	private AdverseReactionRepository adverseReactionRepository;
    
    @Resource(name = "adverseReactionMapper")
	private AdverseReactionMapper adverseReactionMapper;
    
    public int countBy(AdverseReaction adverseReaction){
		return adverseReactionMapper.countBy(Util.objToHash(adverseReaction));
	}

	public List<AdverseReaction> selectBy(AdverseReaction adverseReaction, Page page) {
        HashMap<String, Object> map = Util.objToHash(adverseReaction, page);
		List<AdverseReaction> list = adverseReactionMapper.selectBy(map);
		return list;
	}

	public AdverseReaction get(String id) {
	    AdverseReaction adverseReaction = adverseReactionRepository.findOne(id);
        return adverseReaction;
	} 
    
    public AdverseReaction save(AdverseReaction adverseReaction) {   
        return adverseReactionRepository.save(adverseReaction);
	}

	public void remove(String id) {
		adverseReactionRepository.delete(id);
	}

}