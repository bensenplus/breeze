package org.breeze.core.view;

public class Page {
	
	  private int count;
	  private int page;
	  private int size = 20;
	  private int start;
	  private String order;
	  	
	public int getCount() {
		return count;
	}

	public void setCount(int count) {
		this.count = count;
	}

	public int getPage() {
		return page;
	}

	public void setPage(int page) {
		this.page = page;
	}

	public int getSize() {
		return size;
	}

	public void setSize(int size) {
		this.size = size;
	}

	public int getStart() {
		if(page >0){
			start = (page-1) * size;
		}else{
			start = 0;
		}
		return start;
	}	  
	
	public String getParam() {		
		return String.valueOf(count)+"," +String.valueOf(page)+"," +String.valueOf(size);
	}

	/**
	 * @return the order
	 */
	public String getOrder() {
		return order;
	}

	/**
	 * @param order the order to set
	 */
	public void setOrder(String order) {
		this.order = order;
	}

}
