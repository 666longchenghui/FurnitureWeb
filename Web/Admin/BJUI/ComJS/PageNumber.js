function PageNum(max,index) {
    htmls = [];
    htmls.push("<ul class='pagination'>");
    htmls.push("<li class='c-first'><a href='javascript:'onclick='first()'><span class='first'><i class='fa fa-step-backward'></i>首页</span></a></li>");
    htmls.push("<li class='c-prev'><a href='javascript:' onclick='prev()'><span class='previous'><i class='fa fa-backward'></i>上一页</span></li>");
    //循环出页码
    for (var i = 1; i <= max; i++) {
        var cs = "j-sum";
        if (i == index) {
            cs = "selected " + cs;
        }
        htmls.push("<li class='" + cs + "'><a href='javascript:'> " + i + "</a></li>")//显示页码
    }
    htmls.push("<li class='c-next' ><a href='javascript:' id='n' onclick='next()'><span class='c_next' ><i class='fa fa-forward'></i>下一页</span></li>");
    htmls.push("<li class='c-last'><a href='javascript:' onclick='last()' ><i class='fa fa-step-forward'></i>末页</a></li>")
    htmls.push('<li class="jumpto"><span class="p-input"><input class="form-control input-sm-pages" type="text" size="2.6" value="" title="输入跳转页码，回车确认" id="go"></span><a class="goto" href="javascript:;" title="跳转" onclick="Jump()" ><i class="fa fa-chevron-right"></i></a></li>')
    htmls.push("<ul>");

    //$("PageNum").empty().append(htmls.join(''));
 
}

 