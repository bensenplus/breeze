package org.breeze.dao;

import java.util.List;

import org.breeze.entity.Page;
import org.breeze.entity.Patient;

public interface PatientMapper {

	int count();
    
    List<Patient> select(Page page);
    
    int insert(Patient patient);
    
    Patient get(Long patientId);
	
	int update(Patient patient);
	
	int delete(Long patientId);
    

}