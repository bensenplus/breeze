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
	loadHtml("./search?page="+(page+1),"search-form","result-list");
}

/** 
 * Initialisation function for pagination
 */
function initPage(count, page, size) {
    // Create content inside pagination element
    if(page >0){
    	page = page -1;
    }
    $("#pagehead").pagination(count, {
        callback: select_page,
        prev_text:"上一页",
        next_text:"下一页",
        num_display_entries: 4,//连续分页主体部分显示的分页条目数.默认是11
        num_edge_entries: 1,//两侧显示的首尾分页的条目数.默认是0
        current_page: page,
        items_per_page: size// 每页显示的记录数
    });
    $("#pagefoot").pagination(count, {
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
	
    $( "#dialog:ui-dialog" ).dialog( "destroy" );
	$( "#dialog-form" ).dialog({
		autoOpen: false, modal: true,
		width: 900//height: 600,
	});
	
	//$("#search-icon").addClass("ui-icon ui-icon-search");
	$("#search-form-warp").hide();
    $("#search-icon").click(function(){
    	$("#search-form-warp").toggle();
    });
    
	$("#search-btn").button().click(function(ev){
		loadHtml("./search?page=1","search-form","result-list");
		ev.preventDefault();
	});
	
	$("#update-btn").button().click(function(ev){
		var url = "./update?"+$("#update-form").serialize()+"&date="+new Date().getMilliseconds();; 
		$.post(url, function(data){
			$("#dialog-form" ).dialog( "close" );
			refreshList(curren_page);
		});	
	});
    
	refreshList(0);
}


function edit(param){
    var url = "./edit?"+param+"&date="+new Date().getMilliseconds();; 
	$( "#update-form" ).load(url, function() {
		$("#dialog-form" ).dialog( "open" );
	});
}

function remove(param){
	if(confirm("Are you sure to delete the record?")){
        var url = "./remove?"+param;
        $.get(url,function(data){
        	refreshList(curren_page);
        });
    }
}