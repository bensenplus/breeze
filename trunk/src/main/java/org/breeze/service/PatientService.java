package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.ui.ModelMap;

import org.breeze.entity.Page;
import org.breeze.entity.Patient;
import org.breeze.dao.PatientMapper;


@Service("patientService")
public class PatientService {

	private final Logger logger = LoggerFactory.getLogger(PatientService.class); 

    @Resource(name = "patientMapper")
	private PatientMapper patientMapper;


	public List<Patient> select(Page page) {
        page.setCount(patientMapper.count());
		List<Patient> list = patientMapper.select(page);
		return list;
	}

	public Patient get(Long patientId) {
	    Patient patient = patientMapper.get(patientId);
        return patient;
	}    
    
	public int save(Patient patient) {
    
       if(patient.getPatientId() == null) {
            return patientMapper.insert(patient);
       }else{
            return patientMapper.update(patient);
       }
	}

	public int delete(Long patientId) {
		return patientMapper.delete(patientId);
	}

}