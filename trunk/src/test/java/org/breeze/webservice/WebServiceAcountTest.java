package org.breeze.webservice;

import static org.junit.Assert.assertNotNull;

import org.apache.cxf.interceptor.LoggingInInterceptor;
import org.apache.cxf.interceptor.LoggingOutInterceptor;
import org.apache.cxf.jaxws.JaxWsProxyFactoryBean;
import org.breeze.entity.Account;
import org.breeze.webservice.WebServiceAcount;
import org.junit.Test;


public class WebServiceAcountTest {

	@Test
	public void testGetAccountByUserID() {
		JaxWsProxyFactoryBean  factoryBean=new JaxWsProxyFactoryBean();
		factoryBean.getInInterceptors().add(new LoggingInInterceptor());
		factoryBean.getOutInterceptors().add(new LoggingOutInterceptor());
		factoryBean.setServiceClass(WebServiceAcount.class);
		factoryBean.setAddress("http://localhost:8080/springbreeze/services/WebServiceAcount");
		WebServiceAcount webServiceAcount=(WebServiceAcount) factoryBean.create();
		Account account = webServiceAcount.getAccountByUserID(99999l);
		System.out.println("BEGIN>>>>>>> testGetAccountByUserID");
		System.out.println(account.getUserid());
		System.out.println(account.getCity());
		System.out.println(account.getAddress1());
		System.out.println("END<<<<<<<<< testGetAccountByUserID");
		assertNotNull(account);
	}
	
}
