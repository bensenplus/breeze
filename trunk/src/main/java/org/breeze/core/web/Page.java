package org.breeze.core.web;

public class Page {
	
	  private int count;
	  private int page;
	  private int size = 20;
	  private int start;
	  	
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

}
