<%@ page language="java" contentType="text/html; charset=UTF-8"  pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<link rel="stylesheet" href="../resources/css/pagination.css" />
<script type="text/javascript" src="../resources/js/jquery.min.js"></script>
<script type="text/javascript" src="../resources/js/jquery.pagination.js"></script>
<script type="text/javascript">
            /** 
             * Initialisation function for pagination
             */
            function initPagination() {
                // count entries inside the hidden content
                var num_entries =${page.count}; // 总的记录数
                // Create content inside pagination element
                $("#Pagination").pagination(num_entries, {
                    callback: pageselectCallback,
                    prev_text:"上一页",
                    next_text:"下一页",
                    num_display_entries: 4,//连续分页主体部分显示的分页条目数.默认是11
                    num_edge_entries: 2,//两侧显示的首尾分页的条目数.默认是0
                    current_page: ${page.currentPage},
                    items_per_page: ${page.sizePerPage}// 每页显示的记录数
                });
            }
            
            // When document is ready, initialize pagination
            $(document).ready(function(){      
                initPagination();
            });
</script>