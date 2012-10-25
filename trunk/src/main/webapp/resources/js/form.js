var lastLineId = "";  
var curren_page = 0;
var dbfiled="";
var desc = false;
var options = {};
var spinner = "<div style='width:100%;text-align:center;position:absolute;margin-top:-25px;'><img src='../resources/images/spinner_18_18.gif'/></div>";

/**列表选中颜色**/
function rowclick(obj){
    if (lastLineId != "") {  
        $("#" + lastLineId).removeClass("l-selected");  
    }  
    $(obj).addClass("l-selected");  
    lastLineId = $(obj).attr("id");      	
}

/**列表选中颜色**/
function  queryString(){
	var order ="";
	if(dbfiled && desc) order = dbfiled+ "%20desc"; else order = dbfiled;
	return $("#search-form").serialize()+"&order="+order+"&date="+new Date().getMilliseconds();
}

function doSearch(page){
	var url = "./search?page="+(page+1) +"&"+queryString();
	$("#result-list-warp").html(spinner);
	$("#result-list-warp").load(url, function(response, status, xhr){
		if(xhr.status == "200"){
		    $(".table-list th").dblclick(function(ev){
		    	ev.preventDefault();
		    	if($(this).attr("filed")){
		    		if(dbfiled == $(this).attr("filed")){
		    			desc  = !desc;
		    		}else{
		    			dbfiled = $(this).attr("filed");
		    			desc  =0;
		    		}
		    		doSearch(0);
		    	}			
		    });
			$(".table-list th").each(function(){
				if(dbfiled == $(this).attr("filed")){
					$(this).css("color","yellow");
					$(this).append(desc?"▼":"▲");
				}
			});
		}else{
			alert(xhr.status);
			$("#result-list-warp").html("error");
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
	
	$("#search-form-warp").show();
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
    
	
    $("#back-button").button({icons: {primary: "ui-icon-arrowthick-1-w"}}).click(function(){
		$("#update-form-warp" ).hide();			
		$("#upper-warp" ).show("slide");
    	$("#edit-button").show();
    	$("#save-button").hide();
    });
    
    $("#edit-button").button({icons: {primary: "ui-icon-unlocked"}}).click(function(){
    	readonlyform("#update-form", false);
    	$("#edit-button").hide();
    	$("#save-button").css("display","inline-block"); //$("#save-button").show(); jquery bug?
    	startEdit();
    	initValidate();
    });
    
	$("#save-button").button({icons: {primary: "ui-icon-disk"}}).click(function(ev){
		$("#update-form").submit();
	});
    
	$("#search-button").button({icons: {primary: "ui-icon-search"}}).click(function(ev){
		doSearch(0);
		//ev.preventDefault();
	});
	
	$("#create-button").button({icons: {primary: "ui-icon-document"}}).click(function(ev){
		edit("create");
		//ev.preventDefault();
	});
	
	$("#excel-button").button({icons: {primary: "ui-icon-note"}}).click(function(ev){
		var url = "./excel?"+queryString();
		download(url);
	});
	
	$("#pdf-button").button({icons: {primary: "ui-icon-script"}}).click(function(ev){
		var url = "./pdf?"+queryString();
		download(url);
	});
}

function download(url){
	var elemIF = document.createElement("iframe"); 
	elemIF.src = url;
	elemIF.style.display = "none";
	document.body.appendChild(elemIF);  
}


function initValidate() {
	$.metadata.setType("attr", "validate");
	$("#update-form").validate(
	{
		errorPlacement : function(lable, element) 
		{
			var html ="<span class='tip'><span class='content'></span><s></s><i></i></span>";
			var span = $(html);						
			lable.appendTo(span.find(".content"));
			element.parent().append(span);				   						
		},
		success : function(lable) 
		{
			var element = $("#" + lable.attr("for"));
			lable.parent().parent().remove();
		},
		submitHandler : function() 
		{
			save();
		}
	});
}

function save(){
	var url = "./save"; 
	$.post(url, $("#update-form").serialize()).success(function() { 
		$("#update-form-warp" ).hide();			
		$("#upper-warp" ).show("slide");
    	$("#save-button").hide();
		doSearch(curren_page);
	}).error(function() { 
		alert("error"); 
	});
}

function edit(param){
	if(param=="create"){
	    var url = "./edit?date="+new Date().getMilliseconds();
		$( "#update-form" ).load(url, function(response, status, xhr) {
			if(xhr.status == "200"){
				$("#upper-warp" ).hide("slide", options, 500, function(){
					$("#update-form-warp" ).show();
			    	readonlyform("#update-form", false);
			    	$("#edit-button").hide();
			    	$("#save-button").css("display","inline-block"); //$("#save-button").show();
			    	startEdit();
			    	initValidate();
				});
			}else{
				alert(xhr.status);
			}
		});
	}else{
	    var url = "./edit?"+param+"&date="+new Date().getMilliseconds();
		$( "#update-form" ).load(url, function(response, status, xhr) {
			if(xhr.status == "200"){
				$("#upper-warp" ).hide("slide", options, 500, function(){
					$("#update-form-warp" ).show();
			    	$("#edit-button").show();
			    	$("#save-button").hide();
					readonlyform("#update-form", true);
				});
			}else{
				alert(xhr.status);
			}
		});
	}
}

function remove(param){
	if(confirm("Are you sure to delete the record?")){
        var url = "./remove?"+param;
        $.get(url, function(data){
        }).success(function() {
        	doSearch(curren_page);
        }).error(function() {
        	alert("error"); 
        });
    }
}

function initDatepicker(){
	$.datepicker.regional['zh-CN'] = {
		closeText: '关闭',
		prevText: '&#x3c;上月',
		nextText: '下月&#x3e;',
		currentText: '今天',
		monthNames: ['一月','二月','三月','四月','五月','六月',
		'七月','八月','九月','十月','十一月','十二月'],
		monthNamesShort: ['一月','二月','三月','四月','五月','六月',
		'七月','八月','九月','十月','十一月','十二月'],
		dayNames: ['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],
		dayNamesShort: ['周日','周一','周二','周三','周四','周五','周六'],
		dayNamesMin: ['日','一','二','三','四','五','六'],
		weekHeader: '周',
		dateFormat: 'yy/mm/dd',
		firstDay: 1,
		isRTL: false,
		showMonthAfterYear: true,
		yearSuffix: '年'};
	$.datepicker.setDefaults($.datepicker.regional['zh-CN']);
}