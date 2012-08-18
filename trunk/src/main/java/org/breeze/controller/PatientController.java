package org.breeze.controller;

import java.util.List;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import org.breeze.entity.Page;
import org.breeze.entity.Patient;
import org.breeze.service.PatientService;


@Controller
@RequestMapping("patient")
public class PatientController {

	private final Logger logger = LoggerFactory.getLogger(PatientController.class); 

    @Resource(name = "patientService")
	private PatientService patientService;


    @RequestMapping("/list")
	public String list(ModelMap model, Page page) {
		List<Patient> list = patientService.select(page);
		model.addAttribute("list", list);
		model.addAttribute("page", page);
		return "patient/list";
	}
    
    @RequestMapping("/edit")
	public String edit(ModelMap model, Long patientId) {
	    Patient patient = patientService.get(patientId);
		model.addAttribute("patient", patient);
		return "patient/edit";
	}    
    
    @RequestMapping("/save")
	public String save(Patient patient) {
       patientService.save(patient);
		return "redirect:list";
	}
    
    @RequestMapping("/delete")
	public String delete(Long patientId) {
		patientService.delete(patientId);
		return "redirect:list";
	}
    
	
	
}