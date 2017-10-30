$(function(){

	//原始密码验证
	$(".oldPassword").blur(function(){
		var oldPassword = $(".oldPassword").val();
		var reg = /^[0-9|a-z|A-Z]{6,16}$/ig;
		if(!reg.test(oldPassword)){
			$(".old_msg").html("密码格式有误!").css("color","red");
		}
	});
	//重置密码验证
	$(".newPassword").blur(function(){
		var newPassword = $(".newPassword").val();
		var oldPassword = $(".originPw").val();
		var reg = /^[0-9|a-z|A-Z]{6,16}$/ig;
		if(reg.test(newPassword)){
			if(newPassword == oldPassword){
				$(".new_msg").html("新改密码与原始密码相同!").css("color","red");
			}
		}else{
			$(".new_msg").html("密码格式有误!").css("color","red");
		};
	});
	//重复密码验证
	$(".repeat").blur(function(){
		var newPassword = $(".newPassword").val();
		var repeat = $(".repeat").val();
		if(repeat != newPassword){
			$(".repeat_msg").html("前后密码输入不一致!").css("color","red");
		}
	});

	// 修改密码
	$(".save_pw").click(function(){
		var oldPassword = $("input[name=oldPassword]").val();
		var newPassword = $("input[name=newPassword]").val();
		var repeat = $("input[name=repeat]").val();
		$.ajax({
			type : "post",
			url : "",
			dataType : "json",
			data : {
				"oldPassword" : oldPassword,
				"newPassword" : newPassword,
				"repeat" : repeat
			},
			success:function(ret){
				if(ret.code == 0){
					alert("修改成功！");
					$("input[name=oldPassword]").val("");
					$("input[name=newPassword]").val("");
					$("input[name=repeat]").val("");
				}else{
					alert(ret.msg);
				}
			},
			error:function(){
				alert("网络访问出错！");
			}
		});
	});

	//取消修改
	$(".quit").click(function(){
		$("input[name=oldPassword]").val("");
		$("input[name=newPassword]").val("");
		$("input[name=repeat]").val("");
		$(".old_msg").html("");
		$(".new_msg").html("");
		$(".repeat_msg").html("");
	});

	//退出登录
	$(".logout").click(function(){
		$.ajax({
			type : "get",
			url : "",
			dataType : "json",
			success : function(ret){
				if(ret.code == 0){
					window.location.href = 'login.html';
				}else{
					alert(ret.msg);
				}
			},
			error : function(){
				alert("网络访问出错！");
			}
		})
	})
});