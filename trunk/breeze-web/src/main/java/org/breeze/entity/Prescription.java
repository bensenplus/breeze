package org.breeze.entity;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GenerationType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="PRESCRIPTION")
public class Prescription {
    
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    @Column(name="ADVICE_ID",columnDefinition="", precision=19, nullable=false)
	private Long adviceId; //处方标识     

    @Column(name="MEDICAL_NO",columnDefinition="", precision=19, nullable=true)
	private Long medicalNo; //就诊号     

    @Column(name="PATIENT_ID",columnDefinition="", precision=10, nullable=true)
	private Long patientId; //患者标识     

    @Column(name="PATIENT_NAME",columnDefinition="", length=200, nullable=true)
	private String patientName; //患者姓名     

    @Column(name="SEX_CODE",columnDefinition="", length=80, nullable=true)
	private String sexCode; //性别代码     

    @Column(name="AGE",columnDefinition="", precision=3, nullable=true)
	private Short age; //年龄     

    @Column(name="RECIPEL_NO",columnDefinition="", length=80, nullable=true)
	private String recipelNo; //处方编号     

    @Column(name="RECIPEL_DT",columnDefinition="", nullable=true)
	private Date recipelDt; //开处方日期     

    @Column(name="RECIPEL_DAYS",columnDefinition="", precision=2, nullable=true)
	private Byte recipelDays; //处方有效天数     

    @Column(name="RECIPEL_TYPE",columnDefinition="", length=80, nullable=true)
	private String recipelType; //处方类别代码     

    @Column(name="CHN_RECIPEL_FLAG",columnDefinition="", length=80, nullable=true)
	private String chnRecipelFlag; //中药处方标志     

    @Column(name="CHN_DRUG",columnDefinition="", length=2000, nullable=true)
	private String chnDrug; //中药饮片处方     

    @Column(name="CHN_DRUG_AMOUNT",columnDefinition="", precision=2, nullable=true)
	private Byte chnDrugAmount; //中药饮片剂数     

    @Column(name="CHN_DRUG_DECOCTION_METHOD",columnDefinition="", length=400, nullable=true)
	private String chnDrugDecoctionMethod; //中药饮片煎煮法     

    @Column(name="CHN_DRUG_USE_METHOD",columnDefinition="", length=200, nullable=true)
	private String chnDrugUseMethod; //中药用药方法     

    @Column(name="RECIPEL_DOCTOR_NO",columnDefinition="", length=80, nullable=true)
	private String recipelDoctorNo; //处方医师工号     

    @Column(name="RECIPEL_DOCTOR_ID",columnDefinition="", precision=10, nullable=true)
	private Long recipelDoctorId; //处方医师标识     

    @Column(name="RECIPEL_DOCTOR_TITLE",columnDefinition="", length=80, nullable=true)
	private String recipelDoctorTitle; //处方医师职称     

    @Column(name="RECIPEL_DOCTOR_SIGN",columnDefinition="", length=200, nullable=true)
	private String recipelDoctorSign; //处方医师签名     

    @Column(name="RECIPEL_STATE",columnDefinition="", length=80, nullable=true)
	private String recipelState; //处方状态     

    @Column(name="CONFIRM_DOCTOR_NO",columnDefinition="", length=80, nullable=true)
	private String confirmDoctorNo; //审核医师工号     

    @Column(name="CONFIRM_DOCTOR_ID",columnDefinition="", precision=10, nullable=true)
	private Long confirmDoctorId; //审核医师标识     

    @Column(name="CONFIRM_DOCTOR_TITLE",columnDefinition="", length=80, nullable=true)
	private String confirmDoctorTitle; //审核医师职称     

    @Column(name="CONFIRM_DOCTOR_SIGN",columnDefinition="", length=200, nullable=true)
	private String confirmDoctorSign; //审核医师签名     

    @Column(name="SUM_FEE",columnDefinition="", precision=14, scale=6, nullable=true)
	private Double sumFee; //总金额     

    @Column(name="DEPT_ID",columnDefinition="", precision=10, nullable=true)
	private Long deptId; //科室标识     

    @Column(name="RECIPEL_DEPT_NAME",columnDefinition="", length=200, nullable=true)
	private String recipelDeptName; //开处方科室名称     

    @Column(name="ORG_ID",columnDefinition="", precision=10, nullable=true)
	private Long orgId; //医疗机构标识     

    @Column(name="ORG_NAME",columnDefinition="", length=280, nullable=true)
	private String orgName; //医疗机构名称     

    @Column(name="WEIGHT",columnDefinition="", precision=6, scale=3, nullable=true)
	private Long weight; //体重     

    @Column(name="RETURN_FLAG",columnDefinition="", length=80, nullable=true)
	private String returnFlag; //打回标志     

    @Column(name="SUM_FEE_SHOW",columnDefinition="", precision=10, scale=2, nullable=true)
	private Double sumFeeShow; //总金额(应收)     

    @Column(name="ERROR_FEE",columnDefinition="", precision=14, scale=6, nullable=true)
	private Double errorFee; //舍入误差(应收-总金额）     

    @Column(name="CLINIC_PATH_DRUG_FLAG",columnDefinition="", length=80, nullable=true)
	private String clinicPathDrugFlag; //临床路径用药标志     

    @Column(name="CLASSIC_RECIPEL_ID",columnDefinition="", precision=10, nullable=true)
	private Long classicRecipelId; //经方标识     


	public Long getAdviceId() {
		return adviceId;
	}

	public void setAdviceId(Long adviceId) {
		this.adviceId = adviceId;
	}

	public Long getMedicalNo() {
		return medicalNo;
	}

	public void setMedicalNo(Long medicalNo) {
		this.medicalNo = medicalNo;
	}

	public Long getPatientId() {
		return patientId;
	}

	public void setPatientId(Long patientId) {
		this.patientId = patientId;
	}

	public String getPatientName() {
		return patientName;
	}

	public void setPatientName(String patientName) {
		this.patientName = patientName;
	}

	public String getSexCode() {
		return sexCode;
	}

	public void setSexCode(String sexCode) {
		this.sexCode = sexCode;
	}

	public Short getAge() {
		return age;
	}

	public void setAge(Short age) {
		this.age = age;
	}

	public String getRecipelNo() {
		return recipelNo;
	}

	public void setRecipelNo(String recipelNo) {
		this.recipelNo = recipelNo;
	}

	public Date getRecipelDt() {
		return recipelDt;
	}

	public void setRecipelDt(Date recipelDt) {
		this.recipelDt = recipelDt;
	}

	public Byte getRecipelDays() {
		return recipelDays;
	}

	public void setRecipelDays(Byte recipelDays) {
		this.recipelDays = recipelDays;
	}

	public String getRecipelType() {
		return recipelType;
	}

	public void setRecipelType(String recipelType) {
		this.recipelType = recipelType;
	}

	public String getChnRecipelFlag() {
		return chnRecipelFlag;
	}

	public void setChnRecipelFlag(String chnRecipelFlag) {
		this.chnRecipelFlag = chnRecipelFlag;
	}

	public String getChnDrug() {
		return chnDrug;
	}

	public void setChnDrug(String chnDrug) {
		this.chnDrug = chnDrug;
	}

	public Byte getChnDrugAmount() {
		return chnDrugAmount;
	}

	public void setChnDrugAmount(Byte chnDrugAmount) {
		this.chnDrugAmount = chnDrugAmount;
	}

	public String getChnDrugDecoctionMethod() {
		return chnDrugDecoctionMethod;
	}

	public void setChnDrugDecoctionMethod(String chnDrugDecoctionMethod) {
		this.chnDrugDecoctionMethod = chnDrugDecoctionMethod;
	}

	public String getChnDrugUseMethod() {
		return chnDrugUseMethod;
	}

	public void setChnDrugUseMethod(String chnDrugUseMethod) {
		this.chnDrugUseMethod = chnDrugUseMethod;
	}

	public String getRecipelDoctorNo() {
		return recipelDoctorNo;
	}

	public void setRecipelDoctorNo(String recipelDoctorNo) {
		this.recipelDoctorNo = recipelDoctorNo;
	}

	public Long getRecipelDoctorId() {
		return recipelDoctorId;
	}

	public void setRecipelDoctorId(Long recipelDoctorId) {
		this.recipelDoctorId = recipelDoctorId;
	}

	public String getRecipelDoctorTitle() {
		return recipelDoctorTitle;
	}

	public void setRecipelDoctorTitle(String recipelDoctorTitle) {
		this.recipelDoctorTitle = recipelDoctorTitle;
	}

	public String getRecipelDoctorSign() {
		return recipelDoctorSign;
	}

	public void setRecipelDoctorSign(String recipelDoctorSign) {
		this.recipelDoctorSign = recipelDoctorSign;
	}

	public String getRecipelState() {
		return recipelState;
	}

	public void setRecipelState(String recipelState) {
		this.recipelState = recipelState;
	}

	public String getConfirmDoctorNo() {
		return confirmDoctorNo;
	}

	public void setConfirmDoctorNo(String confirmDoctorNo) {
		this.confirmDoctorNo = confirmDoctorNo;
	}

	public Long getConfirmDoctorId() {
		return confirmDoctorId;
	}

	public void setConfirmDoctorId(Long confirmDoctorId) {
		this.confirmDoctorId = confirmDoctorId;
	}

	public String getConfirmDoctorTitle() {
		return confirmDoctorTitle;
	}

	public void setConfirmDoctorTitle(String confirmDoctorTitle) {
		this.confirmDoctorTitle = confirmDoctorTitle;
	}

	public String getConfirmDoctorSign() {
		return confirmDoctorSign;
	}

	public void setConfirmDoctorSign(String confirmDoctorSign) {
		this.confirmDoctorSign = confirmDoctorSign;
	}

	public Double getSumFee() {
		return sumFee;
	}

	public void setSumFee(Double sumFee) {
		this.sumFee = sumFee;
	}

	public Long getDeptId() {
		return deptId;
	}

	public void setDeptId(Long deptId) {
		this.deptId = deptId;
	}

	public String getRecipelDeptName() {
		return recipelDeptName;
	}

	public void setRecipelDeptName(String recipelDeptName) {
		this.recipelDeptName = recipelDeptName;
	}

	public Long getOrgId() {
		return orgId;
	}

	public void setOrgId(Long orgId) {
		this.orgId = orgId;
	}

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
	}

	public Long getWeight() {
		return weight;
	}

	public void setWeight(Long weight) {
		this.weight = weight;
	}

	public String getReturnFlag() {
		return returnFlag;
	}

	public void setReturnFlag(String returnFlag) {
		this.returnFlag = returnFlag;
	}

	public Double getSumFeeShow() {
		return sumFeeShow;
	}

	public void setSumFeeShow(Double sumFeeShow) {
		this.sumFeeShow = sumFeeShow;
	}

	public Double getErrorFee() {
		return errorFee;
	}

	public void setErrorFee(Double errorFee) {
		this.errorFee = errorFee;
	}

	public String getClinicPathDrugFlag() {
		return clinicPathDrugFlag;
	}

	public void setClinicPathDrugFlag(String clinicPathDrugFlag) {
		this.clinicPathDrugFlag = clinicPathDrugFlag;
	}

	public Long getClassicRecipelId() {
		return classicRecipelId;
	}

	public void setClassicRecipelId(Long classicRecipelId) {
		this.classicRecipelId = classicRecipelId;
	}
}
