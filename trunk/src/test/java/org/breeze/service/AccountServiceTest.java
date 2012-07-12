package org.breeze.service;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.breeze.entity.Account;
import org.breeze.entity.Page;
import org.breeze.service.AccountService;
import org.junit.BeforeClass;
import org.junit.Test;
import org.springframework.beans.factory.BeanFactory;
import org.springframework.context.support.ClassPathXmlApplicationContext;


public class AccountServiceTest {
	
	private static AccountService accountService;
	private static BeanFactory factory;
	
	private static Log log = LogFactory.getLog(AccountServiceTest.class);

	@BeforeClass
	public static void setUpBeforeClass() throws Exception {
		log.debug(System.getProperty("file.encoding"));  
		factory = new ClassPathXmlApplicationContext("applicationContext.xml");
		accountService = (AccountService)factory.getBean("accountService");
	}

	@Test
	public void testGetAccountByUserID() {
		Account account = accountService.getAccountByUserID(10001l);
		assertNotNull(account);
	}

	@Test
	public void testGetAllAccount() {
		
		Page page = new Page();
		List<Account> accountList = accountService.getAllAccount(page);
		log.debug(page);
		assertNotNull(accountList);
	}

	@Test
	public void testGetAccount() {
		Account account = accountService.getAccountByUserID(10001l);
		assertNotNull(account);
	}

	@Test
	public void testUpdateAccount() {
		
		Account account = accountService.getAccountByUserID(10001l);
		account.setCity("shanghai");
		int count = accountService.updateAccount(account);
		log.debug(count);
		account = accountService.getAccountByUserID(10001l);
		assertEquals("shanghai",account.getCity());
	}
	
	@Test
	public void testInsertAccount() {
		Account account = accountService.getAccountByUserID(10001l);
		accountService.deleteAccountByUserID(99999l);
		account.setUserid(99999l);
		accountService.insertAccount(account);
	}

}
