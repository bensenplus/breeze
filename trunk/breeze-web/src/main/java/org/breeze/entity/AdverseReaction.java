package org.breeze.entity;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GenerationType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="ADVERSE_REACTION")
public class AdverseReaction {
    
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    @Column(name="ID",columnDefinition="", length=36, nullable=false)
	private String id; //     

    @Column(name="REG_NO",columnDefinition="", length=20, nullable=false)
	private String regNo; //     

    @Column(name="PATIENT_NAME",columnDefinition="", length=50, nullable=true)
	private String patientName; //     

    @Column(name="TYPE",columnDefinition="", length=2, nullable=true)
	private String type; //     

    @Column(name="EXIST_REACTION",columnDefinition="", precision=1, nullable=true)
	private Byte existReaction; //     

    @Column(name="DESCRIPLE",columnDefinition="", length=4000, nullable=true)
	private String descriple; //     

    @Column(name="RECORDER_NO",columnDefinition="", length=10, nullable=true)
	private String recorderNo; //     

    @Column(name="RECORDER_NAME",columnDefinition="", length=50, nullable=true)
	private String recorderName; //     

    @Column(name="CREATOR",columnDefinition="", length=10, nullable=true)
	private String creator; //     

    @Column(name="CREATE_TIME",columnDefinition="", nullable=true)
	private Date createTime; //     

    @Column(name="MODIFIER",columnDefinition="", length=10, nullable=true)
	private String modifier; //     

    @Column(name="MODIFY_TIME",columnDefinition="", nullable=true)
	private Date modifyTime; //     


	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getRegNo() {
		return regNo;
	}

	public void setRegNo(String regNo) {
		this.regNo = regNo;
	}

	public String getPatientName() {
		return patientName;
	}

	public void setPatientName(String patientName) {
		this.patientName = patientName;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public Byte getExistReaction() {
		return existReaction;
	}

	public void setExistReaction(Byte existReaction) {
		this.existReaction = existReaction;
	}

	public String getDescriple() {
		return descriple;
	}

	public void setDescriple(String descriple) {
		this.descriple = descriple;
	}

	public String getRecorderNo() {
		return recorderNo;
	}

	public void setRecorderNo(String recorderNo) {
		this.recorderNo = recorderNo;
	}

	public String getRecorderName() {
		return recorderName;
	}

	public void setRecorderName(String recorderName) {
		this.recorderName = recorderName;
	}

	public String getCreator() {
		return creator;
	}

	public void setCreator(String creator) {
		this.creator = creator;
	}

	public Date getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Date createTime) {
		this.createTime = createTime;
	}

	public String getModifier() {
		return modifier;
	}

	public void setModifier(String modifier) {
		this.modifier = modifier;
	}

	public Date getModifyTime() {
		return modifyTime;
	}

	public void setModifyTime(Date modifyTime) {
		this.modifyTime = modifyTime;
	}
}
