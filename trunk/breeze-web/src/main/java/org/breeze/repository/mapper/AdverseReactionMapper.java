package org.breeze.repository.mapper;

import java.util.List;
import java.util.HashMap;

import org.breeze.core.view.Page;
import org.breeze.entity.AdverseReaction;

public interface AdverseReactionMapper {

	int countBy(HashMap<String, Object> map);
    List<AdverseReaction> selectBy(HashMap<String, Object> map);    

}