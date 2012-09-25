//上此选中行的id  
var lastLineId = "";  	
function rowclick(obj){
    if (lastLineId != "") {  
        $("#" + lastLineId).removeClass("l-selected");  
    }  
    $(obj).addClass("l-selected");  
    lastLineId = $(obj).attr("id");      	
}

function dbClick(param) {
	edit(param);
}

function loadHtml(url, formName, divName) {
	var queryString="";
	if (formName) {
		queryString = "&"+queryString+$("#"+formName).serialize()+"&date="+new Date().getMilliseconds();
	}else{
		queryString = "&date="+new Date().getMilliseconds();
	}
	$("#"+divName).load(url+queryString,function(response, status, xhr){
		if(xhr.status == "200"){

		}
	});	
}



function refreshList(page){
	loadHtml("./search?page="+(page+1),"search-form","result-list-warp");
}

/** 
 * Initialisation function for pagination
 */
function initPage(count, page, size) {
    // Create content inside pagination element
    if(page >0){
    	page = page -1;
    }
    $("#page-footer").pagination(count, {
        callback: select_page,
        prev_text:"上一页",
        next_text:"下一页",
        num_display_entries: 4,//连续分页主体部分显示的分页条目数.默认是11
        num_edge_entries: 1,//两侧显示的首尾分页的条目数.默认是0
        current_page: page,
        items_per_page: size// 每页显示的记录数
    });
}

var curren_page = 0;
function select_page(page, jq) {
	curren_page = page;
	refreshList(page);
	return false;
}

/** 
 * Initialisation Form
 */
function initForm(){
	
	$("#search-form-warp").hide();
	$("#update-form-warp").hide();
	$("#update-button").hide();
	$("#update-form-warp").addClass("align-center");
    $("#search-icon").addClass("ui-icon ui-icon-triangle-1-e cursor-point");
	
	refreshList(0);
	
    $("#search-icon").click(function(){
    	$("#search-form-warp").toggle("blind");
    	$("#search-icon").toggleClass("ui-icon-triangle-1-e");
    	$("#search-icon").toggleClass("ui-icon-triangle-1-s");
    });
    
	
    $("#back-button").click(function(){
		$("#update-form-warp" ).hide();			
		$("#upper-warp" ).show("slide");
    	$("#edit-button").show();
    	$("#update-button").hide();
    });
    
    $("#edit-button").click(function(){
    	readonlyform("#update-form", false);
    	$("#edit-button").hide();
    	$("#update-button").show();
    });
    
	$("#update-button").click(function(ev){
		var url = "./update?"+$("#update-form").serialize()+"&date="+new Date().getMilliseconds();; 
		$.post(url, function(data){
			$("#update-form-warp" ).hide();			
			$("#upper-warp" ).show("slide");
	    	$("#edit-button").show();
	    	$("#update-button").hide();
			refreshList(curren_page);
		});	
	});
    
	$("#search-button").click(function(ev){
		refreshList(0);
		ev.preventDefault();
	});	

}

var options = {};

function edit(param){
    var url = "./edit?"+param+"&date="+new Date().getMilliseconds();; 
	$( "#update-form" ).load(url, function() {	
		$("#upper-warp" ).hide("slide", options, 500, function(){
			$("#update-form-warp" ).show();
			readonlyform("#update-form", true);
		});
	});
}

function readonlyform(form, readonly){
	if(readonly){
		$(form+" input" ).attr("readonly",readonly);
		$(form+" input" ).addClass("input-readonly");
		$(form+" textarea" ).attr("readonly",readonly); 
		$(form+" textarea" ).addClass("textarea-readonly");
		$(form+" textarea" ).each(function(){
			$(this)[0].heght_old = $(this)[0].height;
			$(this).height($(this)[0].scrollHeight);
		});
		$("#update-btn").hide();
	}else{
		$(form+" input" ).removeAttr("readonly");
		$(form+" input" ).removeClass("input-readonly");
		$(form+" textarea" ).removeAttr("readonly");
		$(form+" textarea" ).removeClass("textarea-readonly");
		$(form+" textarea" ).height(250);
		$("#update-btn").show();
	}
}


function remove(param){
	if(confirm("Are you sure to delete the record?")){
        var url = "./remove?"+param;
        $.get(url,function(data){
        	refreshList(curren_page);
        });
    }
}