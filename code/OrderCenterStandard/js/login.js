$(function () {
    
    $("#btn_ok").click(function () {
        
        var url = postURL + "login/loginIn";
        var loginData = { LoginId: $("#txt_userName").val(), PassWord: $("#txt_pwd").val(), Code: $("#img_code").val() };
        $.ajax({
            type: 'post',
            url: url,
            async: true,
            headers: {
                staffid: 12,
                timestamp: Date.parse(new Date()),
                nonce: CreateRandom()
            },
            contentType: "application/json",
            data: JSON.stringify(loginData),
            success: function (rev) {                
                if (rev.error_code == 1) {
                    var storage = window.localStorage;
                    var returnData =rev.data;   
                    storage.setItem('LoginID', returnData[0].LoginId);
                    storage.setItem('UserName', returnData[0].UserName);
                    storage.setItem('Token', returnData[0].Token);
                    window.location = "../manager/main.html";
                } else {
                    alert('操作失败，请稍后再试！');
                }
            },
            error: function () {
                alert('网络连接错误');
            }
        })
    });

   
})
//创建0-12之间的随机数
function CreateRandom() {
    var x = 12;
    var y = 0;
    var rand = parseInt(Math.random() * (x - y + 1) + y);
    return rand;
}

function getCode() {

}