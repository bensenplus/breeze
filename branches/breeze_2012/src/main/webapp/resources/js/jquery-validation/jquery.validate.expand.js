$.validator.setDefaults({
	success:function(element){element.parent().parent().remove()},
   errorPlacement:function(error,element){					   
		var html ="<span class='tip'><span class='content'></span><s></s><i></i></span>"
		var span = $(html);						
   		error.appendTo(span.find(".content"));
   		element.parent().append(span);				   		
   		span.hide();
   		element.hover(function(){span.show();},function(){span.hide();});
   		
   		/* var content = div.parent();
		content.find(".formError").css("top", element.position().top + element.height() / 2 - div.find(".formErrorContent").height() / 2);
		content.find(".formError").css("left",element.position().left+ element.width() + 25); */
    } 
});
