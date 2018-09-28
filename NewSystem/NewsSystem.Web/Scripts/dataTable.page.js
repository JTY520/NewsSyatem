 $(document).ready(function () {
            $('#example').DataTable({
                "pagingType": "full_numbers",
                "language": {
                    "processing": "加载中...",
                    "lengthMenu": "每页显示 _MENU_ 条数据",
                    "zeroRecords": "没有匹配结果",
                    "info": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                    "search": "搜索:",
                    "emptyTable": "没有匹配结果",
                    "loadingRecords": "载入中...",
                    "thousands": ",",
                    "paginate": {
                        "first": "首页",
                        "previous": "上一页",
                        "next": "下一页",
                        "last": "末页"
                    }
                }
            });
});