/*
 * Copyright (c) 2010-2020 Founder Ltd. All Rights Reserved.
 *
 * This software is the confidential and proprietary information of
 * Founder. You shall not disclose such Confidential Information
 * and shall use it only in accordance with the terms of the agreements
 * you entered into with Founder.
 */

package org.breeze.core.view;


public class Response{
	
	private String token;
	private int  time;
	
	private int status;
	
	private int msgCode;
	private String[] msgParam;	
	
	
	private Object resultObject;
	
	/**
	 * 返回成功结果
	 * @param resultObject
	 */
	 public Response(Object resultObject){
		 this.setStatus(1);
		 this.setResultObject(resultObject);
	 }
	 /**
	  * 返回失败结果
	  * @param msgCode
	  * @param msgParam
	  */
	 public Response(int msgCode, String[] msgParam){
		 this.setStatus(0);
		 this.setMsgCode(msgCode);
		 this.setMsgParam(msgParam);
	 }
	/**
	 * @return the token
	 */
	public String getToken() {
		return token;
	}
	/**
	 * @param token the token to set
	 */
	public void setToken(String token) {
		this.token = token;
	}
	/**
	 * @return the time
	 */
	public int getTime() {
		return time;
	}
	/**
	 * @param time the time to set
	 */
	public void setTime(int time) {
		this.time = time;
	}
	/**
	 * @return the status
	 */
	public int getStatus() {
		return status;
	}
	/**
	 * @param status the status to set
	 */
	public void setStatus(int status) {
		this.status = status;
	}
	/**
	 * @return the msgCode
	 */
	public int getMsgCode() {
		return msgCode;
	}
	/**
	 * @param msgCode the msgCode to set
	 */
	public void setMsgCode(int msgCode) {
		this.msgCode = msgCode;
	}
	/**
	 * @return the msgParam
	 */
	public String[] getMsgParam() {
		return msgParam;
	}
	/**
	 * @param msgParam the msgParam to set
	 */
	public void setMsgParam(String[] msgParam) {
		this.msgParam = msgParam;
	}
	/**
	 * @return the resultObject
	 */
	public Object getResultObject() {
		return resultObject;
	}
	/**
	 * @param resultObject the resultObject to set
	 */
	public void setResultObject(Object resultObject) {
		this.resultObject = resultObject;
	}
	
	
	
}
