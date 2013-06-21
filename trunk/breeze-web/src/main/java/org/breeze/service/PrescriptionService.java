package org.breeze.service;

import java.util.List;
import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

import org.breeze.entity.Prescription;
import org.breeze.repository.PrescriptionRepository;

@Service("prescriptionService")
public class PrescriptionService {

	private final Logger logger = LoggerFactory.getLogger(PrescriptionService.class); 

    @Resource(name = "prescriptionRepository")
	private PrescriptionRepository prescriptionRepository;
 

	public Page<Prescription> findAll(Pageable pageable) {
		return prescriptionRepository.findAll(pageable);
	}
    
    public List<Prescription> findAll() {
		return prescriptionRepository.findAll();
	}

	public Prescription get(Long adviceId) {
	    Prescription prescription = prescriptionRepository.findOne(adviceId);
        return prescription;
	} 
    
    public Prescription save(Prescription prescription) {   
        return prescriptionRepository.save(prescription);
	}

	public void remove(Long adviceId) {
		prescriptionRepository.delete(adviceId);
	}

}