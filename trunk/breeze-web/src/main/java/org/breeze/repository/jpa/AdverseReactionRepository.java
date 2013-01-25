package org.breeze.repository.jpa;

import java.util.List;
import java.util.Date;
import org.springframework.data.jpa.repository.JpaRepository;

import org.breeze.entity.AdverseReaction;

public interface AdverseReactionRepository extends JpaRepository<AdverseReaction, String> {
   
        //sample
        //List<AdverseReaction> findByIdOrderByIdAsc(String id);
        
}