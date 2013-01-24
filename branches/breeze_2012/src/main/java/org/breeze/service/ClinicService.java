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
import org.breeze.entity.Clinic;
import org.breeze.repository.jpa.ClinicRepository;
import org.breeze.repository.mapper.ClinicMapper;


@Service("clinicService")
public class ClinicService {

	private final Logger logger = LoggerFactory.getLogger(ClinicService.class); 

    @Resource(name = "clinicRepository")
	private ClinicRepository clinicRepository;
    
    @Resource(name = "clinicMapper")
	private ClinicMapper clinicMapper;
    
    public int countBy(Clinic clinic){
		return clinicMapper.countBy(Util.objToHash(clinic));
	}

	public List<Clinic> selectBy(Clinic clinic, Page page) {
        HashMap<String, Object> map = Util.objToHash(clinic, page);
		List<Clinic> list = clinicMapper.selectBy(map);
		return list;
	}

	public Clinic get(String id) {
	    Clinic clinic = clinicRepository.findOne(id);
        return clinic;
	} 
    
    public Clinic save(Clinic clinic) {   
        return clinicRepository.save(clinic);
	}

	public void remove(String id) {
		clinicRepository.delete(id);
	}

}