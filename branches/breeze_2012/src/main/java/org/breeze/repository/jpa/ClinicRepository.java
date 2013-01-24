package org.breeze.repository.jpa;

import java.util.List;
import java.util.Date;
import org.springframework.data.jpa.repository.JpaRepository;

import org.breeze.entity.Clinic;

public interface ClinicRepository extends JpaRepository<Clinic, String> {
   
        //sample
        //List<Clinic> findByIdOrderByIdAsc(String id);
        
}