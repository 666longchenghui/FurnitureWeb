<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Web.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <link href="Admin/BJUI/UPloadfiy/uploadify.css" rel="stylesheet" />
 
   <script src="Admin/BJUI/js/jquery-1.7.2.min.js"></script>
    <script src="Admin/BJUI/UPloadfiy/swfobject.js"></script>
       <script src="Admin/BJUI/UPloadfiy/jquery.uploadify.min.js" ></script>
    <title></title>
    <script type="text/javascript">
        $(function () {
            $("#file_upload").uploadify({
                //开启调试    
                'debug': false,
                //是否自动上传    
                'auto': false,
                'buttonText': '选择照片',
                //flash   
                'swf':"/Admin/BJUI/UPloadfiy/uploadify.swf",
                'queueSizeLimit': 5,
                //文件选择后的容器ID    
                'queueID': 'uploadfileQueue',
                //后台运行上传的程序  
                'uploader': '/Ajax/Uploadfiy.ashx',
                'width': '75',
                'height': '24',
                //是否支持多文件上传，这里为true，你在选择文件的时候，就可以选择多个文件  
                'multi': true,
                'fileTypeDesc': '支持的格式：',
                'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png',
                'fileSizeLimit': '1MB',
                //上传完成后多久删除进度条  
                'removeTimeout': 1,

                //返回一个错误，选择文件的时候触发    
                'onSelectError': function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                            break;
                        case -110:
                            alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                            break;
                        case -120:
                            alert("文件 [" + file.name + "] 大小异常！");
                            break;
                        case -130:
                            alert("文件 [" + file.name + "] 类型不正确！");
                            break;
                    }
                },
                //检测FLASH失败调用    
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                //上传到服务器，服务器返回相应信息到data里    
                'onUploadSuccess': function (file, data, response) {
                    alert(file.name);
                    $("#mypic").attr("src", data);
                },
                //多文件上传，服务器返回相应的信息  
                'onQueueComplete': function (queueData) {
                    alert(queueData.uploadsSuccessful);
                }
            });
        });

        //开始上传  
        function doUpload() {
            $('#file_upload').uploadify('upload', '*');
        }
        //停止上传  
        function closeLoad() {
            $('#file_upload').uploadify('cancel', '*');
        }

    </script>
</head>
<body>
      <table width="704" border="0" align="center" cellpadding="0" cellspacing="0" id="__01">    
        <tr>    
            <td align="center" valign="middle">    
                <div id="uploadfileQueue" style="padding: 3px;">    
                </div>    
                <div id="file_upload">    
                </div>    
            </td>    
        </tr>    
        <tr>    
            <td height="50" align="center" valign="middle">    
                <input type="button" value="上传" onclick="doUpload()" />  
                <input type="button" value="取消" onclick="closeLoad()" />   
            </td>    
        </tr>    
        <tr>  
            <td>  
                <img id="mypic" width="100" height="120" />  
            </td>  
        </tr>  
    </table>    
</body>
</html>
