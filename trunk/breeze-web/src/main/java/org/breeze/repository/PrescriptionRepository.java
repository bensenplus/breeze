package org.breeze.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import org.breeze.entity.Prescription;

public interface PrescriptionRepository extends JpaRepository<Prescription, Long> {
   
        //sample
        //List<Prescription> findByAdviceIdOrderByAdviceIdAsc(Long adviceId);
        
}