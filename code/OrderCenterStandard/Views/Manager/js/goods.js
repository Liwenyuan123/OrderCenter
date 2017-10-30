$(function(){

	// 新增商品
	$(".add").click(function(){
		$("editGoods").modal('show');
	})
	$(".reset").click(function(){
		$("input[name=goodsName]").val("");
		$("input[name=goodsFormat]").val("");
		$("input[name=goodsUnit]").val("");
		$("input[name=goodsPrice]").val("");
		$("input[name=goodsFCLPrice]").val("");
		$("select[name=goodsType]").val(0);
	});

	loading(1);
	getType();

	// 添加/修改-保存
	$(".save").click(function(){
		var id = $("input[name=id]").val();
		var goodsName = $("input[name=goodsName]").val();
		var goodsFormat = $("input[name=goodsFormat]").val();
		var goodsUnit = $("input[name=goodsUnit]").val();
		var goodsPrice = $("input[name=goodsPrice]").val();
		var goodsFCLPrice = $("input[name=goodsFCLPrice]").val();
		var goodsType = $("input[name=goodsType]").val();
		
		if(id == 0){
			var url = ""; //添加
			$("#editGoodsLabel").html("新增商品信息");
			$(".reset").css("display","block");
		}else{
			var url = ""; //修改
			$("#editGoodsLabel").html("修改商品信息");
			$(".reset").css("display","none");
		}
		$.ajax({
			type:"post",
			url:url,
			dataType:"json",
			data:{
				"Uid":id,
				"ComName":goodsName,
				"Standard":goodsFormat,
				"Unit":goodsUnit,
				"Price":goodsPrice,
				"PriceSum":goodsFCLPrice,
				"TypeName":goodsType
			},
			success:function(ret){
				if(ret.code == 0){
					var page = $("input[name=page]").val();
					search(page);
					$("editGoods").modal('hide');
				}else{
					alert(ret.msg);
				}
			},
			error:function(){
				alert((id == 0 ? "添加" : "修改") + "出错！");
			}
		});
	});
});

// 页面加载
function loading(page = 1, key_goodsName = "", key_goodsType = ""){
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"pageIndex":page,
			"ComName":key_goodsName,
			"TypeID":key_goodsType
		},
		success:function(ret){
			if(ret.code == 0){
				var data = ret.data;
				var html = "";
				var htmlp = "";
				for(var i = 0; i < data.length; i++){
					html +="<tr class='goods_" + data[i].UID + "'><td>" + (i + 1)
							+"</td><td>" + data[i].ComName
							+"</td><td>" + data[i].Standard 
							+"</td><td>" + data[i].Unit 
							+"</td><td>" + data[i].Price 
							+"</td><td>" + data[i].PriceSum 
							+"</td><td>" + data[i].TypeName 
							+"</td><td><span data-toggle='modal' data-target='#edit' onclick='updateData(" + data[i].UID
							+")'>修改</span>&nbsp;&nbsp;&nbsp;<span data-toggle='modal' data-target='#delete' onclick='deleteDataWarn(" + data[i].UID 
							+")'>删除</span></td></tr>"
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
	})
}

// 获取类型
function getType(){
	$.ajax({
		type:"get",
		url:"",
		dataType:"json",
		success:function(ret){
			if(ret.code == 0){
				var data = ret.data;
				var html = "";
				for(var i = 0; i < data.length; i++){
					html +="<option value='" + data[i].TypeID 
							+"'>" + data[i].TypeName +"</option>"
				}
				$("#goodsType").html("<option value='0'>--请选择--</option>" + html);
			}
		}
	})
}

//查询
function search(page){
	var key_goodsName = $("#goodsName").val();
	var key_goodsType = $("#goodsType").val();
	if(key_goodsName == "" && key_goodsType == 0){
		alert("请选择要查询的条件！");
	}
	$("input[name=page]").val(page);
	loading(page, key_goodsName, key_goodsType);
}

// 添加
function addData(){
	$("input[name=goodsName]").val("");
	$("input[name=goodsFormat]").val("");
	$("input[name=goodsUnit]").val("");
	$("input[name=goodsPrice]").val("");
	$("input[name=goodsFCLPrice]").val("");
	$("input[name=goodsType]").val("");
}

//修改
function updateData(id){
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"Uid":id
		},
		success:function(ret){
			if(ret.code == 0){
				$("input[name=goodsName]").val(ret.data.ComName);
				$("input[name=goodsFormat]").val(ret.data.Standard);
				$("input[name=goodsUnit]").val(ret.data.Unit);
				$("input[name=goodsPrice]").val(ret.data.Price);
				$("input[name=goodsFCLPrice]").val(ret.data.PriceSum);
				$("input[name=goodsType]").val(ret.data.TypeName);
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
	$("input[name=delete_goods_id]").val(id);
	$("#delete").modal('show');
}

function deleteData(){
	var id = $("input[name=delete_goods_id]").val();
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
