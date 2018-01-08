$(function () {
    $("#btn_Login").click(function () {
        console.log('开始请求control........');
        var url = "http://localhost:26888/api/Service/GetToken?staffId=12";
        $.ajax({
            type: 'get',
            url: url,
            async: true,
            headers: {
                staffid: 12,
                timestamp: Date.parse(new Date()),
                nonce: CreateRandom()
            },
            contentType: 'application/json',
            success: function (data) {
                console.log(JSON.stringify(data));
                var storage = window.localStorage;
                var returnData = data.Data;
               
                storage.setItem('StaffId', returnData.StaffId);
                storage.setItem('SignToken', returnData.SignToken);
                storage.setItem('ExpireTime', returnData.ExpireTime);

            }
        })
    });

    $("#btnGetType").click(function () {
        $.ajax({
            type: 'get',
            url: 'http://localhost:26888/api/FoodType',
            async: true,//异步
            dataType: 'json',

            data: null,
            beforeSend: function (xhr) {
                //console.log("222");
                var storage = window.localStorage;
                var StaffId = storage.getItem('StaffId');
                var ExpireTime = storage.getItem('ExpireTime');
                var SignToken = storage.getItem('SignToken');
                var random = CreateRandom();
                var timestamp = Date.parse(new Date());
                var SignNature = hex_md5(timestamp + random + StaffId + SignToken);

                xhr.setRequestHeader("staffid", StaffId);
                xhr.setRequestHeader("timestamp", Date.parse(new Date()));
                xhr.setRequestHeader("nonce", random);
                xhr.setRequestHeader("signToken", SignToken);
                xhr.setRequestHeader("signature", SignNature)
                console.log(SignNature)
            },
            success: function (data) {

                alert("111");
                // console.log(JSON.stringify(data));
                $.each(data, function (i, item) {
                    $("#typediv").append(
                        "<div>类别ID:" + item.ID + "</div>" +
                        "<div>类别PID:" + item.PID + "</div>" +
                        "<div>类别名称：" + item.TypeName + "</div><hr/>");
                });

            },
            error: function (data) {
                //console.log(JSON.stringify(data));
            }
        });

    })
})
//创建0-12之间的随机数
function CreateRandom() {
    var x = 12;
    var y = 0;
    var rand = parseInt(Math.random() * (x - y + 1) + y);
    return rand;
}