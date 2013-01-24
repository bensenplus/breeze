package org.breeze.entity;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="Clinic")
public class Clinic {
    
    @Id
    @Column(name="Id",columnDefinition="", length=254, nullable=true)
	private String id; //数据库唯一编号（自增长）     

    @Column(name="AgencyDepartmentCode",columnDefinition="", length=254, nullable=true)
	private String agencydepartmentcode; //机构科室编码     

    @Column(name="SupDepartmentsCode",columnDefinition="", length=254, nullable=true)
	private String supdepartmentscode; //上级科室编码     

    @Column(name="AgencyDepartmentAbbreviation",columnDefinition="", length=254, nullable=true)
	private String agencydepartmentabbreviation; //机构科室简称     

    @Column(name="AgencyDepartmentFullName",columnDefinition="", length=254, nullable=true)
	private String agencydepartmentfullname; //机构科室全称     

    @Column(name="AgenciesDepartmentsCategory",columnDefinition="", length=254, nullable=true)
	private String agenciesdepartmentscategory; //机构科室类别     

    @Column(name="SubordinateOrganizationsCode",columnDefinition="", length=254, nullable=true)
	private String subordinateorganizationscode; //所属机构编码     

    @Column(name="SubordinateOrganizationsName",columnDefinition="", length=254, nullable=true)
	private String subordinateorganizationsname; //所属机构名称     

    @Column(name="DepartmentPhone",columnDefinition="", length=254, nullable=true)
	private String departmentphone; //科室电话     

    @Column(name="DepartmentFax",columnDefinition="", length=254, nullable=true)
	private String departmentfax; //科室传真     

    @Column(name="MNumber",columnDefinition="", nullable=true)
	private Long mnumber; //男性职工人数     

    @Column(name="FNumber",columnDefinition="", nullable=true)
	private Long fnumber; //女性职工人数     

    @Column(name="StartDate",columnDefinition="", nullable=true)
	private Date startdate; //科室成立日期     

    @Column(name="EndDate",columnDefinition="", nullable=true)
	private Date enddate; //科室撤销日期     


	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getAgencydepartmentcode() {
		return agencydepartmentcode;
	}

	public void setAgencydepartmentcode(String agencydepartmentcode) {
		this.agencydepartmentcode = agencydepartmentcode;
	}

	public String getSupdepartmentscode() {
		return supdepartmentscode;
	}

	public void setSupdepartmentscode(String supdepartmentscode) {
		this.supdepartmentscode = supdepartmentscode;
	}

	public String getAgencydepartmentabbreviation() {
		return agencydepartmentabbreviation;
	}

	public void setAgencydepartmentabbreviation(String agencydepartmentabbreviation) {
		this.agencydepartmentabbreviation = agencydepartmentabbreviation;
	}

	public String getAgencydepartmentfullname() {
		return agencydepartmentfullname;
	}

	public void setAgencydepartmentfullname(String agencydepartmentfullname) {
		this.agencydepartmentfullname = agencydepartmentfullname;
	}

	public String getAgenciesdepartmentscategory() {
		return agenciesdepartmentscategory;
	}

	public void setAgenciesdepartmentscategory(String agenciesdepartmentscategory) {
		this.agenciesdepartmentscategory = agenciesdepartmentscategory;
	}

	public String getSubordinateorganizationscode() {
		return subordinateorganizationscode;
	}

	public void setSubordinateorganizationscode(String subordinateorganizationscode) {
		this.subordinateorganizationscode = subordinateorganizationscode;
	}

	public String getSubordinateorganizationsname() {
		return subordinateorganizationsname;
	}

	public void setSubordinateorganizationsname(String subordinateorganizationsname) {
		this.subordinateorganizationsname = subordinateorganizationsname;
	}

	public String getDepartmentphone() {
		return departmentphone;
	}

	public void setDepartmentphone(String departmentphone) {
		this.departmentphone = departmentphone;
	}

	public String getDepartmentfax() {
		return departmentfax;
	}

	public void setDepartmentfax(String departmentfax) {
		this.departmentfax = departmentfax;
	}

	public Long getMnumber() {
		return mnumber;
	}

	public void setMnumber(Long mnumber) {
		this.mnumber = mnumber;
	}

	public Long getFnumber() {
		return fnumber;
	}

	public void setFnumber(Long fnumber) {
		this.fnumber = fnumber;
	}

	public Date getStartdate() {
		return startdate;
	}

	public void setStartdate(Date startdate) {
		this.startdate = startdate;
	}

	public Date getEnddate() {
		return enddate;
	}

	public void setEnddate(Date enddate) {
		this.enddate = enddate;
	}
}
