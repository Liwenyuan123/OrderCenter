//页面加载
function loading(page = 1, key_account = "", key_status = ""){
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"PageIndex":page,
			"":key_account,
			"":key_status
		},
		success:function(ret){
			if(ret.code == 0){
				var data = ret.data;
				var html = "";
				var htmlp = "";
				for(var i = 0; i < data.length; i++){
					html +="<tr class='user_" + data[i].UID + "'><td>" + (i + 1)
							+"</td><td>" + data[i].
							+"</td><td>" + data[i].
							+"</td><td>" + data[i].
							+"</td><td>" + data[i].
							+"</td><td><span data-toggle='modal' data-target='#edit' onclick='updateData(" + data[i].UID 
							+ ")'>修改</span>&nbsp;&nbsp;&nbsp;<span data-toggle='modal' data-target='#delete' onclick='deleteDataWarn(" + data[i].UID 
							+ ")'>删除</span></td></tr>"
				}

				//分页
				htmlp +="<span>共计：" + data.PageCount
						+"条</span><span>第<p class='page'>" + data.PageIndex
						+"页 共 " + data.PageTotal
						+" 页</span><span class='glyphicon glyphicon-triangle-left' aria-hidden='true' onclick='search(" + (parseInt(page) - 1) 
						+ "></span><span class='glyphicon glyphicon-triangle-right' aria-hidden='true' onclick='search(" +  (parseInt(page) + 1)
						+")'></span>" 
				$("tbody").html(html);
				$(".pageInfo").html(htmlp);
			}else{
				alert(ret.msg);
			}
		},
		error:function(){
			alert("网络访问出错！");
		}
	});
}

$(function(){
	loading(1);

	//修改保存
	$(".update").click(function(){
		var status = $("select[name=status]").val();
		var id = $("input[name=id]").val();
		$.ajax({
			type:"post",
			url:"",
			dataType:"json",
			data:{
				"UID":id,
				"":status
			},
			success:function(ret){
				var page = $("input[name=page]").val();
				search(page);
				$("edit").modal('hide');
			},
			error:function(){
				alert("修改信息出错！");
			}
		});
	});
});

//查询
function search(page){
	var key_account = $("#account").val();
	var key_status = $("#status").val();
	if(key_account == "" && key_status == 0){
		alert("请选择要查询的条件！");
	}
	$("input[name=page]").val(page);
	loading(page, key_account, key_status);
}

// 修改
function updateData(id){
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"UID":id
		},
		success:function(ret){
			if(ret.code == 0){
				$("input[name=account]").val(ret.data.);
				$("input[name=userName").val(ret.data.);
				$("select[name=status]").val(ret.data.);
			}else{
				alert(ret.msg);
			}
		},
		error:function(){
			alert("网络访问出错！");
		}
	});
}

//删除
function deleteDataWarn(id){
	$("input[name=delete_user_id]").val(id);
	$("#delete").modal('show');
}

function deleteData(){
	var id = $("input[name=delete_user_id]").val();
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"Uid":id
		},
		success:function(ret){
			if(ret.code == 0){
				$("#delete").modal('hide');
				var page = $("input[name=page]").val();
				search(page);
			}else{
				alert(ret.msg);
			}
		},
		error:function(){
			alert("网络访问出错!");
		}
	});
}