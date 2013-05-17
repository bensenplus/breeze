package org.breeze.service;

import static org.junit.Assert.fail;

import javax.annotation.Resource;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations={"../../../config.database.xml","../../../config.jpa.xml","../../../config.mvc.xml"})
public class PrescriptionServiceTest {

	@Resource(name="prescriptionService")
	PrescriptionService prescriptionService;

	@Test
	public void testFindAll() {
		prescriptionService.findAll();
	}

	@Test
	public void testFindOne() {
		fail("Not yet implemented");
	}

	@Test
	public void testSave() {
		fail("Not yet implemented");
	}

	@Test
	public void testDelete() {
		fail("Not yet implemented");
	}

}
