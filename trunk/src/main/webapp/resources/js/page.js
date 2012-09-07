/** 
 * Initialisation function for pagination
 */
function initPage(count, page, size) {
    // Create content inside pagination element
    if(page >0){
    	page = page -1;
    }
    $("#Pagination").pagination(count, {
        callback: select_page,
        prev_text:"上一页",
        next_text:"下一页",
        num_display_entries: 4,//连续分页主体部分显示的分页条目数.默认是11
        num_edge_entries: 2,//两侧显示的首尾分页的条目数.默认是0
        current_page: page,
        items_per_page: size// 每页显示的记录数
    });
}

function select_page(page, jq) {
	location.href= "./list?page="+(page+1);
	return false;
}