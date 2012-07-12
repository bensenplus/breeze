package org.breeze.entity;

public class Page {
	
	  private int count;
	  private int currentPage;
	  private int sizePerPage = 10;
	  private int startIndex;
	  	
	public int getCount() {
		return count;
	}

	public void setCount(int count) {
		this.count = count;
	}

	public int getCurrentPage() {
		return currentPage;
	}

	public void setCurrentPage(int currentPage) {
		this.currentPage = currentPage;
	}

	public int getSizePerPage() {
		return sizePerPage;
	}

	public void setSizePerPage(int sizePerPage) {
		this.sizePerPage = sizePerPage;
	}

	public int getStartIndex() {
		startIndex = currentPage * sizePerPage;
		return startIndex;
	}

	@Override
	public String toString(){
		return "Page result: count{"+count+"} currentPage{"+currentPage+"} sizePerPage{"+ sizePerPage + "} startIndex{" + startIndex+"}";
	}
	  

}
