package org.breeze.repository.mapper;

import java.util.List;
import java.util.HashMap;

import org.breeze.core.view.Page;
import org.breeze.entity.Clinic;

public interface ClinicMapper {

	int countBy(HashMap<String, Object> map);
    List<Clinic> selectBy(HashMap<String, Object> map);    

}