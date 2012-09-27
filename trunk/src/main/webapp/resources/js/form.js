var lastLineId = "";  
var curren_page = 0;
var dbfiled="";
var desc = false;
var options = {};
var action ="";

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


function  queryString(){
	var order ="";
	if(dbfiled && desc) order = dbfiled+ "%20desc"; else order = dbfiled;
	return $("#search-form").serialize()+"&order="+order+"&date="+new Date().getMilliseconds();
}

function doSearch(page){
	var url = "./search?page="+(page+1) +"&"+queryString();
	$("#result-list-warp").load(url, function(response, status, xhr){
		if(xhr.status == "200"){
		    $(".table-list th").dblclick(function(ev){
		    	if($(this).attr("filed")){
		    		if(dbfiled == $(this).attr("filed")){
		    			desc  = !desc;
		    		}else{
		    			dbfiled = $(this).attr("filed");
		    			desc  =0;
		    		}
		    		doSearch(0);
		    	}
				ev.preventDefault();
		    });
			$(".table-list th").each(function(){
				if(dbfiled == $(this).attr("filed")){
					$(this).css("color","yellow");
					$(this).append(desc?"▼":"▲");
				}
			});
		}else{
			alert(xhr.status);
		}
	});	
}


function readonlyform(form, readonly){
	if(readonly){
		$(form+" input" ).attr("readonly",readonly);
		$(form+" input" ).addClass("input-readonly");
		$(form+" textarea" ).attr("readonly",readonly); 
		$(form+" textarea" ).addClass("textarea-readonly");
		$(form+" textarea" ).each(function(){
			$(this).height($(this)[0].scrollHeight);
		});
		$("#update-btn").hide();
	}else{
		$(form+" input" ).removeAttr("readonly");
		$(form+" input" ).removeClass("input-readonly");
		$(form+" textarea" ).removeAttr("readonly");
		$(form+" textarea" ).removeClass("textarea-readonly");
		$(form+" textarea" ).each(function(){
			$(this).height($(this)[0].rows*30);
		});
		$("#update-btn").show();
	}
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

function select_page(page, jq) {
	curren_page = page;
	doSearch(page);
	return false;
}

/** 
 * Initialisation Form
 */
function initForm(){
	
	$("#search-form-warp").hide();
	$("#update-form-warp").hide();
	$("#save-button").hide();
	$("#update-form-warp").addClass("align-center");
    $("#search-icon").addClass("ui-icon ui-icon-triangle-1-e cursor-point");
	
	doSearch(0);
	
    $("#search-icon").click(function(){
    	$("#search-form-warp").toggle("blind");
    	$("#search-icon").toggleClass("ui-icon-triangle-1-e");
    	$("#search-icon").toggleClass("ui-icon-triangle-1-s");
    });
    
	
    $("#back-button").click(function(){
		$("#update-form-warp" ).hide();			
		$("#upper-warp" ).show("slide");
    	$("#edit-button").show();
    	$("#save-button").hide();
    });
    
    $("#edit-button").click(function(){
    	readonlyform("#update-form", false);
    	$("#edit-button").hide();
    	$("#save-button").show();
    	startEdit();
    });
    
	$("#save-button").click(function(ev){
		var url = "./"+action+"?"+$("#update-form").serialize()+"&date="+new Date().getMilliseconds();; 
		$.post(url, function(data){
			$("#update-form-warp" ).hide();			
			$("#upper-warp" ).show("slide");
	    	$("#save-button").hide();
			doSearch(curren_page);
		});	
	});
    
	$("#search-button").click(function(ev){
		doSearch(0);
		ev.preventDefault();
	});
	
	$("#create-button").click(function(ev){
		edit("create");
		ev.preventDefault();
	});
	
	$("#excel-button").click(function(ev){
		ev.preventDefault();
		var url = "./excel?"+queryString();
		location.href = url;
	});
	
	$("#pdf-button").click(function(ev){
		ev.preventDefault();
		var url = "./pdf?"+queryString();
		window.open(url);
	});

}

function edit(param){
	if(param=="create"){
		action = "create";
	    var url = "./edit?date="+new Date().getMilliseconds();
		$( "#update-form" ).load(url, function() {
			$("#upper-warp" ).hide("slide", options, 500, function(){
				$("#update-form-warp" ).show();
		    	readonlyform("#update-form", false);
		    	$("#edit-button").hide();
		    	$("#save-button").show();
		    	startEdit();
			});
		});
	}else{
		action = "update";
	    var url = "./edit?"+param+"&date="+new Date().getMilliseconds();
		$( "#update-form" ).load(url, function() {
			$("#upper-warp" ).hide("slide", options, 500, function(){
				$("#update-form-warp" ).show();
		    	$("#edit-button").show();
		    	$("#save-button").hide();
				readonlyform("#update-form", true);
			});
		});
	}
}

function remove(param){
	if(confirm("Are you sure to delete the record?")){
        var url = "./remove?"+param;
        $.get(url,function(data){
        	doSearch(curren_page);
        });
    }
}