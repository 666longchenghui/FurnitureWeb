function OnloadSelect() {

    tempAjax = "";
    $.ajax({
        url: "../../Ajax/AjaxDepartMent.ashx?action=GetDepartMent",
        data: "",
        type: "post",
        async: false,
        success: function (msg) {
            // console.log(msg);
            var json = JSON.parse(msg);
            var Data = json[""];
            tempAjax += "<option >请选择</option>"
            $.each(Data, function (k, v) {
                tempAjax += "<option value='" + v.DepartId + "'>" + v.Department + "</option>";
            });
            $("#Depart").empty();
            $("#Depart").append(tempAjax);
            //更新内容刷新到相应的位置
            $('#Depart').selectpicker('render');
            $('#Depart').selectpicker('refresh');

        }
    });
}
//默认加载单位信息
function OnloadUnit() {
    var UnitName = "";
    $.ajax({
        url: "/Ajax/AjaxGoods.ashx?action=GetUnitName",
        data: "",
        type: "post",
        async: false,
        success: function (msg) {
            console.log(msg);
            var json = JSON.parse(msg);
            var Data = json[""];
            UnitName += "<option>请选择</option>"
            $.each(Data, function (k, v) {
                UnitName += "<option value='" + v.Unitid + "'>" + v.UnitName + "</option>";
            });
            $("#M_Unit").empty();
            $("#M_Unit").append(UnitName);
            //更新内容刷新到相应的位置
            $('#M_Unit').selectpicker('render');
            $('#M_Unit').selectpicker('refresh');
        }
    });
}

function OnloadModel() {
    var ModelName = "";
    $.ajax({
        url: "/Ajax/AjaxGoods.ashx?action=GetModelName",
        data: "",
        type: "post",
        async: false,
        success: function (msg) {
            var json = JSON.parse(msg);

            var Data = json[""];
            ModelName += "<option >请选择</option>"
            $.each(Data, function (k, v) {
                ModelName += "<option value='" + v.DictionaryID + "'>" + v.DataText + "</option>";
            });
            $("#M_specification").empty();
            $("#M_specification").append(ModelName);
            //更新内容刷新到相应的位置
            $('#M_specification').selectpicker('render');
            $('#M_specification').selectpicker('refresh');
        }
    });

}