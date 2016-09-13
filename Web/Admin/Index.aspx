<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.Admin.Index" %>

<!DOCTYPE html>
<html lang="zh">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title> 进销存后台管理系统</title>
    <meta name="Keywords" content="美谷进销存系统" />
    <meta name="Description" content="美谷进销存系统" />
    <!-- bootstrap - css -->
    <script src="BJUI/js/jquery-1.7.2.min.js"></script>
    <link href="BJUI/themes/css/bootstrap.css" rel="stylesheet" />
    <!-- core - css -->
    <link href="BJUI/themes/css/style.css" rel="stylesheet">
    <link href="BJUI/themes/blue/core.css" id="bjui-link-theme" rel="stylesheet">
    <!-- plug - css -->
    <link href="../B-JUI/BJUI/plugins/kindeditor_4.1.10/themes/default/default.css" rel="stylesheet" />
    <link href="BJUI/plugins/colorpicker/css/bootstrap-colorpicker.min.css" rel="stylesheet">
    <link href="BJUI/plugins/niceValidator/jquery.validator.css" rel="stylesheet">
    <link href="BJUI/plugins/bootstrapSelect/bootstrap-select.css" rel="stylesheet">
    <link href="BJUI/themes/css/FA/css/font-awesome.min.css" rel="stylesheet">
    <!--[if lte IE 7]>
    <link href="BJUI/themes/css/ie7.css" rel="stylesheet">
    <![endif]-->
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lte IE 9]>
        <script src="BJUI/other/html5shiv.min.js"></script>
        <script src="BJUI/other/respond.min.js"></script>
    <![endif]-->
    <!-- jquery -->
    <script src="BJUI/js/jquery.cookie.js"></script>
    <!--[if lte IE 9]>
    <script src="BJUI/other/jquery.iframe-transport.js"></script>
    <![endif]-->
    <!-- BJUI.all 分模块压缩版 -->
    <script src="BJUI/js/bjui-all.js"></script>

    <!-- 以下是B-JUI的分模块未压缩版，建议开发调试阶段使用下面的版本 -->
    <!--
    <script src="BJUI/js/bjui-core.js"></script>
    <script src="BJUI/js/bjui-regional.zh-CN.js"></script>
    <script src="BJUI/js/bjui-frag.js"></script>
    <script src="BJUI/js/bjui-extends.js"></script>
    <script src="BJUI/js/bjui-basedrag.js"></script>
    <script src="BJUI/js/bjui-slidebar.js"></script>
    <script src="BJUI/js/bjui-contextmenu.js"></script>
    <script src="BJUI/js/bjui-navtab.js"></script>
    <script src="BJUI/js/bjui-dialog.js"></script>
    <script src="BJUI/js/bjui-taskbar.js"></script>
    <script src="BJUI/js/bjui-ajax.js"></script>
    <script src="BJUI/js/bjui-alertmsg.js"></script>
    <script src="BJUI/js/bjui-pagination.js"></script>
    <script src="BJUI/js/bjui-util.date.js"></script>
    <script src="BJUI/js/bjui-datepicker.js"></script>
    <script src="BJUI/js/bjui-ajaxtab.js"></script>
    <script src="BJUI/js/bjui-datagrid.js"></script>
    <script src="BJUI/js/bjui-tablefixed.js"></script>
    <script src="BJUI/js/bjui-tabledit.js"></script>
    <script src="BJUI/js/bjui-spinner.js"></script>
    <script src="BJUI/js/bjui-lookup.js"></script>
    <script src="BJUI/js/bjui-tags.js"></script>
    <script src="BJUI/js/bjui-upload.js"></script>
    <script src="BJUI/js/bjui-theme.js"></script>
    <script src="BJUI/js/bjui-initui.js"></script>
    <script src="BJUI/js/bjui-plugins.js"></script>
    -->
    <!-- plugins -->
    <link href="../B-JUI/BJUI/plugins/kindeditor_4.1.10/plugins/code/prettify.css" rel="stylesheet" />
    <!-- swfupload for uploadify && kindeditor -->
    <script src="BJUI/plugins/swfupload/swfupload.js"></script>
    <!-- kindeditor -->

    <script src="BJUI/plugins/kindeditor_4.1.10/kindeditor-all.min.js"></script>
    <script src="BJUI/plugins/kindeditor_4.1.10/lang/zh_CN.js"></script>
    <!-- colorpicker -->
    <script src="BJUI/plugins/colorpicker/js/bootstrap-colorpicker.min.js"></script>
    <!-- ztree -->
    <script src="BJUI/plugins/ztree/jquery.ztree.all-3.5.js"></script>
    <!-- nice validate -->
    <script src="BJUI/plugins/niceValidator/jquery.validator.js"></script>
    <script src="BJUI/plugins/niceValidator/jquery.validator.themes.js"></script>
    <!-- bootstrap plugins -->
    <script src="BJUI/plugins/bootstrap.min.js"></script>
    <script src="BJUI/plugins/bootstrapSelect/bootstrap-select.min.js"></script>
    <script src="BJUI/plugins/bootstrapSelect/defaults-zh_CN.min.js"></script>
    <!-- icheck -->
    <script src="BJUI/plugins/icheck/icheck.min.js"></script>
    <!-- dragsort -->
    <script src="BJUI/plugins/dragsort/jquery.dragsort-0.5.1.min.js"></script>
    <!-- HighCharts -->
    <script src="BJUI/plugins/highcharts/highcharts.js"></script>
    <script src="BJUI/plugins/highcharts/highcharts-3d.js"></script>
    <script src="BJUI/plugins/highcharts/themes/gray.js"></script>
    <!-- ECharts -->
    <script src="BJUI/plugins/echarts/echarts.js"></script>
    <!-- other plugins -->
    <script src="BJUI/plugins/other/jquery.autosize.js"></script>
    <link href="BJUI/plugins/uploadify/css/uploadify.css" rel="stylesheet">
    <script src="BJUI/plugins/uploadify/scripts/jquery.uploadify.min.js"></script>
    <script src="BJUI/plugins/download/jquery.fileDownload.js"></script>
    <!-- init -->
    <script type="text/javascript">
        $(function () {
            BJUI.init({
                JSPATH: 'BJUI/',         //[可选]框架路径
                PLUGINPATH: 'BJUI/plugins/', //[可选]插件路径
                loginInfo: { url: 'login_timeout.html', title: '登录', width: 400, height: 200 }, // 会话超时后弹出登录对话框
                statusCode: { ok: 200, error: 300, timeout: 301 }, //[可选]
                ajaxTimeout: 50000, //[可选]全局Ajax请求超时时间(毫秒)
                pageInfo: { total: 'total', pageCurrent: 'pageCurrent', pageSize: 'pageSize', orderField: 'orderField', orderDirection: 'orderDirection' }, //[可选]分页参数
                alertMsg: { displayPosition: 'topcenter', displayMode: 'slide', alertTimeout: 3000 }, //[可选]信息提示的显示位置，显隐方式，及[info/correct]方式时自动关闭延时(毫秒)
                keys: { statusCode: 'statusCode', message: 'message' }, //[可选]
                ui: {
                    windowWidth: 0,    //框架可视宽度，0=100%宽，> 600为则居中显示
                    showSlidebar: true, //[可选]左侧导航栏锁定/隐藏
                    clientPaging: true, //[可选]是否在客户端响应分页及排序参数
                    overwriteHomeTab: true //[可选]当打开一个未定义id的navtab时，是否可以覆盖主navtab(我的主页)
                },
                debug: true,    // [可选]调试模式 [true|false，默认false]
                theme: 'sky' // 若有Cookie['bjui_theme'],优先选择Cookie['bjui_theme']。皮肤[五种皮肤:default, orange, purple, blue, red, green]
            })

            // main - menu
            $('#bjui-accordionmenu')
                .collapse()
                .on('hidden.bs.collapse', function (e) {
                    alert("aaa11");
                    $(this).find('> .panel > .panel-heading').each(function () {
                        var $heading = $(this), $a = $heading.find('> h4 > a')

                        if ($a.hasClass('collapsed')) $heading.removeClass('active')
                    })
                })
                .on('shown.bs.collapse', function (e) {
                    $(this).find('> .panel > .panel-heading').each(function () {
                        var $heading = $(this), $a = $heading.find('> h4 > a')

                        if (!$a.hasClass('collapsed')) $heading.addClass('active')
                    })
                })

            $(document).on('click', 'ul.menu-items > li > a', function (e) {
                alert("aaa");
                var $a = $(this), $li = $a.parent(), options = $a.data('options').toObj()
                var onClose = function () {
                    $li.removeClass('active')
                }
                var onSwitch = function () {
                    $('#bjui-accordionmenu').find('ul.menu-items > li').removeClass('switch')
                    $li.addClass('switch')
                }

                $li.addClass('active')
                if (options) {
                    options.url = $a.attr('href')
                    options.onClose = onClose
                    options.onSwitch = onSwitch
                    if (!options.title) options.title = $a.text()
                    if (!options.target)
                        $a.navtab(options)
                    else
                        $a.dialog(options)
                }
                e.preventDefault()
            })

            //时钟
            var today = new Date(), time = today.getTime()
            $('#bjui-date').html(today.formatDate('yyyy/MM/dd'))
            setInterval(function () {
                today = new Date(today.setSeconds(today.getSeconds() + 1))
                $('#bjui-clock').html(today.formatDate('HH:mm:ss'))
            }, 1000)
        })

        //菜单-事件
        function MainMenuClick(event, treeId, treeNode) {
            event.preventDefault()

            if (treeNode.isParent) {
                var zTree = $.fn.zTree.getZTreeObj(treeId)

                zTree.expandNode(treeNode, !treeNode.open, false, true, true)
                return
            }
            if (treeNode.target && treeNode.target == 'dialog')
                $(event.target).dialog({ id: treeNode.tabid, url: treeNode.url, title: treeNode.name })
            else
                $(event.target).navtab({ id: treeNode.tabid, url: treeNode.url, title: treeNode.name, fresh: treeNode.fresh, external: treeNode.external })
        }

    </script>
    <!-- for doc begin -->
    <link href="BJUI/CSS/js/syntaxhighlighter-2.1.382/styles/shCore.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="BJUI/CSS/js/syntaxhighlighter-2.1.382/styles/shThemeEclipse.css" />
    <script type="text/javascript" src="BJUI/CSS/js/syntaxhighlighter-2.1.382/scripts/brush.js"></script>
   
    <link href="../B-JUI/doc/doc.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function(){
            SyntaxHighlighter.config.clipboardSwf = 'BJUI/CSS/js/syntaxhighlighter-2.1.382/scripts/clipboard.swf'
              $(document).on(BJUI.eventType.initUI, function(e) {
         SyntaxHighlighter.highlight();
    })
})
    </script>
    <!-- for doc end -->
</head>
<body>
    <!--[if lte IE 7]>
        <div id="errorie"><div>您还在使用老掉牙的IE，正常使用系统前请升级您的浏览器到 IE8以上版本 <a target="_blank" href="http://windows.microsoft.com/zh-cn/internet-explorer/ie-8-worldwide-languages">点击升级</a>&nbsp;&nbsp;强烈建议您更改换浏览器：<a href="http://down.tech.sina.com.cn/content/40975.html" target="_blank">谷歌 Chrome</a></div></div>
    <![endif]-->
    <div id="bjui-window">
        <header id="bjui-header">
            <div class="bjui-navbar-header"  style="margin-bottom:auto ">
                <button type="button" class="bjui-navbar-toggle btn-default" data-toggle="collapse" data-target="#bjui-navbar-collapse">
                    <i class="fa fa-bars"></i>
                </button>
                <a class="bjui-navbar-logo" href="http://www.mgoo.net/"><img src="BJUI/Img/mgoo.png"></a>
            </div>

            <nav id="bjui-navbar-collapse">
                <ul class="bjui-navbar-right">
                    <li class="datetime"><div><span id="bjui-date"></span> <span id="bjui-clock"></span></div></li>
                 <%--   <li><a href="#">消息 <span class="badge">4</span></a></li>--%>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                            <%= (Session["LoginUserInfo"] as Model.Users).U_UserName %> <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
                            <li><a href="changepwd.html" data-toggle="dialog" data-id="changepwd_page" data-mask="true" data-width="400" data-height="260">&nbsp;<span class="glyphicon glyphicon-lock"></span> 修改密码&nbsp;</a></li>
                            <li><a href="DataBase/Users/UserInfo.html" data-toggle="dialog" data-id="dialog_id"data-mask="true" data-width="700" data-height="400" data-reload-warn="已打开业务页面，确认将重新载入?" data-title="我的资料">&nbsp;<span class="glyphicon glyphicon-user"></span> 我的资料</a></li>
                            <li class="divider"></li>                   
                            <li><a href="AdminLogin.aspx?action=Logout" class="red">&nbsp;<span class="glyphicon glyphicon-off"></span> 注销登陆</a></li>
                        </ul>
                    </li>
                <%--    <li><a href="AdminIndex.html" title="切换为列表导航(窄版)" style="background-color:#ff7b61;">列表导航栏(窄版)</a></li>--%>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle theme blue" data-toggle="dropdown" title="切换皮肤"><i class="fa fa-tree"></i></a>
                        <ul class="dropdown-menu" role="menu" id="bjui-themes">
                            <li><a href="javascript:;" class="theme_default" data-toggle="theme" data-theme="default">&nbsp;<i class="fa fa-tree"></i> 黑白分明&nbsp;&nbsp;</a></li>
                            <li><a href="javascript:;" class="theme_orange" data-toggle="theme" data-theme="orange">&nbsp;<i class="fa fa-tree"></i> 橘子红了</a></li>
                            <li><a href="javascript:;" class="theme_purple" data-toggle="theme" data-theme="purple">&nbsp;<i class="fa fa-tree"></i> 紫罗兰</a></li>
                            <li class="active"><a href="javascript:;" class="theme_blue" data-toggle="theme" data-theme="blue">&nbsp;<i class="fa fa-tree"></i> 天空蓝</a></li>
                            <li><a href="javascript:;" class="theme_green" data-toggle="theme" data-theme="green">&nbsp;<i class="fa fa-tree"></i> 绿草如茵</a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
            <div id="bjui-hnav">
                <button type="button" class="btn-default bjui-hnav-more-left" title="导航菜单左移"><i class="fa fa-angle-double-left"></i></button>
                <div id="bjui-hnav-navbar-box">
                    <ul id="bjui-hnav-navbar">
                        <li class="active">
                            <a href="javascript:;" data-toggle="slidebar"><i class="fa fa-check-square-o"></i> 基础数据</a>
                            <div class="items hide" data-noinit="true">
                                <ul id="bjui-hnav-data" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="check-square-o">
                                    <li data-id="1" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">基础数据</li>
                          
                                  <!--//  <li data-id="7" data-pid="1" data-url="" data-tabid="supplier" data-faicon="hand-o-up">供应商</li>-->
                                    <li data-id="5" data-pid="1" data-url="DataBase/supplier/SupplierList.html" data-tabid="supplier" data-faicon="table">供应商管理</li>
                                    <li data-id="6" data-pid="1" data-url="DataBase/Goods/GoodsList.html"data-faicon="table" data-tabid="table">商品管理</li>
                                    <li data-id="7" data-pid="1" data-url="DataBase/Goods/UnitList.html" data-faicon="table" data-tabid="unit">单位管理</li>
                                    <li data-id="8" data-pid="1" data-url="DataBase/Goods/ModelList.html" data-faicon="table" data-tabid="model">型号管理</li>
                                    <li data-id="9" data-pid="1" data-url="DataBase/DepartMent/DepartMentList.html" data-faicon="table" data-tabid="depart">部门管理</li>                                                 
                                    <!--<li data-id="10" data-pid="1" data-url="DepartMent/AddUsers.html" data-faicon="table" data-tabid="Depart">用户管理</li>-->
                                    <li data-id="10" data-pid="1" data-url="DataBase/Users/PersonnelManagement.html" data-faicon="table" data-tabid="user">用户管理</li>
                                    <!--<li data-id="12" data-pid="1" data-url="DepartMent/AddDepart.html" data-faicon="list" data-tabid="import">创建部门</li>-->
                                    <li data-id="Mian_AccountId" data-pid="1" data-url="DataBase/Account/AccountList.html" data-faicon="table" data-tabid="WareHouse">客户管理</li>     
                                    <li data-id="12" data-pid="1" data-url="DataBase/WareHouse/WareHouseList.html" data-faicon="table" data-tabid="WareHouse">仓库管理</li>     
                                </ul>                          
                                
                            </div>
                        </li>
                        <li>
                            <a href="javascript:;" data-toggle="slidebar"><i class="fa fa-table"></i> 库存管理</a>
                            <div class="items hide" data-noinit="true">
                                <ul id="bjui-hnav-tree2" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="table">
                                      <li data-id="2" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">库存管理</li>               
                                      <li data-pid="2" data-toggle="navtab" data-id="tabInHostory"  data-tabid="table-edit" data-faicon="indent" data-title="进货历史">库存盘点</li>
                                      <li data-pid="2" data-id="tabStoreManager"  data-url="InHostory/StoreManager.html" data-tabid="table-manager" data-faicon="indent">库存查询</li>                 
                                      <li data-pid="1" data-id="3"  data-url="InHostory/StoreManager.html" data-tabid="table-manager" data-faicon="indent">入库管理</li>                
                                      <li data-pid="3" data-toggle="navtab" data-id="tabInHostory"  data-url="InHostory/InHostory.html"  data-tabid="table-edit" data-faicon="indent" data-title="进货历史">进货历史</li>       
                                      <li data-pid="3" data-toggle="navtab" data-id=""  data-url="InHostory/StockReturnHistory.html"  data-tabid="table-edit" data-faicon="indent" data-title="进货历史">进货退货历史</li>   
                                      <li data-pid="3" data-toggle="navtab" data-id=""   data-tabid="table-edit" data-faicon="indent" data-title="进货历史">进货订单历史</li>   
                                     <li data-pid="1" data-id="4"  data-url="" data-tabid="table-manager" data-faicon="indent">出库管理</li>                
                                      <li data-pid="4" data-toggle="navtab" data-id="tabInHostory"   data-url="Output/OutputInHostory.html"   data-tabid="table-edit" data-faicon="indent" data-title="进货历史"> 出库历史</li>     
                                       <li data-pid="4" data-toggle="navtab" data-id="tabInHostory"   data-tabid="table-edit" data-faicon="indent" data-title="进货历史">出库退货历史</li>     
                                </ul>
                            </div>   
                        </li>
                                             
                        <li>
                            <a href="javascript:;" data-toggle="slidebar"><i class="fa fa-image"></i> 统计报表</a>
                            <div class="items hide" data-noinit="true">
                                <ul id="bjui-hnav-tree5" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="image">
                                    <li data-id="5" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">图形报表</li>
                                    <li data-id="51" data-pid="5" data-url="highcharts.html" data-tabid="chart" data-faicon="image">Highcharts图表</li>
                                    <li data-id="52" data-pid="5" data-url="echarts.html" data-tabid="echarts" data-faicon="image">ECharts图表</li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <a href="javascript:;" data-toggle="slidebar"><i class="fa fa-coffee"></i> 权限管理</a>
                            <div class="items hide" data-noinit="true">
                                <ul id="bjui-hnav-tree6" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="coffee">
                                    <li data-id="6" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">框架组件</li>
                                    <li data-id="61" data-pid="6" data-url="tabs.html" data-tabid="tabs" data-faicon="columns">选项卡</li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <a href="javascript:;" data-toggle="slidebar"><i class="fa fa-bug"></i> 数据备份</a>
                            <div class="items hide" data-noinit="true">
                                <ul id="bjui-hnav-tree7" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="bug">
                                    <li data-id="7" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">其他插件</li>
                                    <li data-id="71" data-pid="7" data-url="ztree.html" data-tabid="ztree" data-faicon="tree">zTree</li>
                                    <li data-id="72" data-pid="7" data-url="ztree-select.html" data-tabid="ztree-select" data-faicon="caret-square-o-down">zTree下拉选择</li>
                                </ul>
                            </div>
                        </li>   
                        <li>
                            <a href="javascript:;" data-toggle="slidebar"><i class="fa fa-table"></i>用户管理</a>
                            <div class="items hide" data-noinit="true">
                         
                                <ul id="bjui-hnav-tree-datagrid" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="table">
                                    <li data-id="3" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o"> 用户管理</li>
                                    <li data-id="31" data-pid="3" data-url="Users/UserList.html" data-tabid="datagrid-convertable" data-faicon="table" >用户列表</li>
                                    <li data-id="32" data-pid="3" data-url="Users/AddList.html" data-tabid="datagrid-demo" data-faicon="table">添加用户</li>
                                </ul>
                            </div>
                       
                        </li>              
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-cog"></i> 系统设置 <span class="caret"></span></a>
                            <ul class="dropdown-menu" role="menu">
                               <li><a href="#">角色权限</a></li>                                                    
                                <li>
                                <li class="divider"></li>
                                <li><a href="#">关于我们</a></li>
                                <li class="divider"></li>
                                <li><a href="#">友情链接</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <button type="button" class="btn-default bjui-hnav-more-right" title="导航菜单右移"><i class="fa fa-angle-double-right"></i></button>
            </div>
        </header>
        <div id="bjui-container">
            <div id="bjui-leftside">
                <div id="bjui-sidebar-s">
                    <div class="collapse"></div>
                </div>
                <div id="bjui-sidebar">
                    <div class="toggleCollapse"><h2><i class="fa fa-bars"></i> 导航栏 <i class="fa fa-bars"></i></h2><a href="javascript:;" class="lock"><i class="fa fa-lock"></i></a></div>
                    <div class="panel-group panel-main" data-toggle="accordion" id="bjui-accordionmenu" data-heightbox="#bjui-sidebar" data-offsety="26">
                    </div>
                </div>
            </div>
            <div id="bjui-navtab" class="tabsPage">
                <div class="tabsPageHeader">
                    <div class="tabsPageHeaderContent">
                        <ul class="navtab-tab nav nav-tabs">
                            <li data-url="index_layout.html"><a href="javascript:;"><span><i class="fa fa-home"></i> #maintab#</span></a></li>
                        </ul>
                    </div>
                    <div class="tabsLeft"><i class="fa fa-angle-double-left"></i></div>
                    <div class="tabsRight"><i class="fa fa-angle-double-right"></i></div>
                    <div class="tabsMore"><i class="fa fa-angle-double-down"></i></div>
                </div>
                <ul class="tabsMoreList">
                    <li><a href="javascript:;">#maintab#</a></li>
                </ul>
                <div class="navtab-panel tabsPageContent">
                    <div class="navtabPage unitBox">
                        <div class="bjui-pageContent" style="background:#FFF;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <footer id="bjui-footer">
            Copyright &copy;<a href="http://www.mgoo.net/" target="_blank">美谷进销存系统</a>
            <!--
            <script type="text/javascript">var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://");document.write(unescape("%3Cspan id='cnzz_stat_icon_1252983288'%3E%3C/span%3E%3Cscript src='" + cnzz_protocol + "s23.cnzz.com/stat.php%3Fid%3D1252983288%26show%3Dpic' type='text/javascript'%3E%3C/script%3E"));</script>
            -->
        </footer>
    </div>
</body>
</html>