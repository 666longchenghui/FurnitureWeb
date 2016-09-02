$(document).ready(function () {
    $("#uploadify").uploadify({
        'uploader': 'js/jquery.uploadify-v2.1.0/uploadify.swf',
        'script': 'imageHandler.ashx',
        'cancelImg': 'js/jquery.uploadify-v2.1.0/cancel.png',
        'folder': 'UploadFile',
        'buttonText': 'SELECT Pic',
        'fileExt': '*.jpg;*.gif,*.png',                 //允许上传的文件格式为*.jpg,*.gif,*.png
        'fileDesc': 'Web Image Files(.JPG,.GIF,.PNG)',  //过滤掉除了*.jpg,*.gif,*.png的文件
        'queueID': 'fileQueue',
        'sizeLimit': '2048000',                         //最大允许的文件大小为2M
        'auto': false,                                  //需要手动的提交申请
        'multi': true,                                 //一次只允许上传一张图片
        'onCancel': funCancel,                          //当用户取消上传时
        'onComplete': funComplete,                      //完成上传任务
        'OnError': funError                             //上传发生错误时
    });
});

//用户取消函数
function funCancel(event, ID, fileObj, data) {
    alert('您取消了操作');
    return;
}

//图片上传发生的事件
function funComplete(event, ID, fileObj, response, data) {
    if (response == 0) {
        alert('图片' + fileObj.name + '操作失败');
        return;
    }
    $("#pic").attr("src", "UploadFile/" + response).height(150).width(300);
    alert('图片' + fileObj.name + '操作成功');
    return;
}

//上传发生错误时。
function funError(event, ID, fileObj, errorObj) {
    alert(errorObj.info);
    return;
}