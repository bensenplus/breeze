package org.breeze.webservice;

import static org.junit.Assert.assertNotNull;

import org.apache.cxf.interceptor.LoggingInInterceptor;
import org.apache.cxf.interceptor.LoggingOutInterceptor;
import org.apache.cxf.jaxws.JaxWsProxyFactoryBean;
import org.breeze.entity.Account;
import org.breeze.webservice.AccountService;
import org.junit.Test;


public class AcountTest {

	@Test
	public void testGetAccountByUserID() {
		JaxWsProxyFactoryBean  factoryBean=new JaxWsProxyFactoryBean();
		factoryBean.getInInterceptors().add(new LoggingInInterceptor());
		factoryBean.getOutInterceptors().add(new LoggingOutInterceptor());
		factoryBean.setServiceClass(AccountService.class);
		factoryBean.setAddress("http://localhost:8080/breeze/services/Acount");
		AccountService webServiceAcount=(AccountService) factoryBean.create();
		Account account = webServiceAcount.get(99999l);
		System.out.println("BEGIN>>>>>>> testGetAccountByUserID");
		System.out.println(account.getUserid());
		System.out.println(account.getCity());
		System.out.println(account.getAddress1());
		System.out.println("END<<<<<<<<< testGetAccountByUserID");
		assertNotNull(account);
	}
	
}
