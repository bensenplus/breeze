//上此选中行的id  
var lastLineId = "";  	
function rowclick(obj){
    if (lastLineId != "") {  
    	 if(lastLineId % 2 == 1)
    		 $("#" + lastLineId).addClass("l-odd");  
    	  else
    		  $("#" + lastLineId).addClass("l-even");  
   	
        $("#" + lastLineId).removeClass("l-selected");  
    }  
    $(obj).removeClass("l-odd");  
    $(obj).removeClass("l-even");  
    $(obj).addClass("l-selected");  
    lastLineId = $(obj).attr("id");      	
}

function dbClick(param) {
	edit(param);
}

function edit(param){
    var url = "./edit?"+param+"&date="+new Date().getMilliseconds();; 
	$( "#dialog-form" ).load(url, function() {
		//alert(url);
	});
	$("#dialog-form" ).dialog( "open" );
}

function remove(param){
	if(confirm("Are you sure to delete the record?")){
        var url = "./remove?"+param;
        $.get(url,function(result){
            select_page(1, 0);
        });
    }
}

function initForm(count, page, size){
	
	initPage(count, page, size);
    $('table.table-list tr:even').not(".pagefoot").addClass("l-even");	     
    //$( "#dialog:ui-dialog" ).dialog( "destroy" );
	$( "#dialog-form" ).dialog({
		autoOpen: false,
		width: 800,
		//height: 600,
		modal: false
	});
}