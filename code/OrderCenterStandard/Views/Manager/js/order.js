$(function(){

	// 日期选择器
	$(".datepicker").datepicker({
		language:'zh-CN',
		autoclose:true,
		todayHighlight:true
	});

	loading(1);

	// 筛选状态查询
	$(".chooseItem").click(function(){
		$(this).addClass("active").siblings().removeClass("active");
		var key_choice = $(".form-group .active").text();
		$.ajax({
			type:"post",
			url:"",
			dataType:"json",
			data:{
				"OrState":key_choice
			},
			success:function(ret){
				if(ret.code == 0){
					var page = $("input[name=page]").val();
					var key_time_up = $("input[name=time_up]").val();
					var key_time_down = $("input[name=time_down]").val();
					loading(page, key_time_up, key_time_down, key_choice);
				}
			},
			error:function(){
				alert("网络访问出错！");
			}
		});
	});

	// 审核通过
	$(".confirm").click(function(){
		var id = $("input[name=id]").val();
		$.ajax({
			type:"post",
			url:"",
			dataType:"json",
			data:{
				"MainID":id,
				"":'通过'
			},
			success:function(ret){
				if(ret.code == 0){
					alert("已通过审核！");
					$("#examine").modal('hide');
					$(".order_" + id).children(".status").html("待发货").addClass("colorOrange");
				}else{
					alert(ret.msg);
				}
			},
			error:function(){
				alert("网络访问出错！");
			}
		});
	});

	// 审核拒绝
	$(".refuse").click(function(){
		var id = $("input[name=id]").val();
		$.ajax({
			type:"post",
			url:"",
			dataType:"json",
			data:{
				"MainID":id,
				"":'拒绝'
			},
			success:function(ret){
				if(ret.code == 0){
					alert("未通过审核！");
					$("#examine").modal('hide');
					$(".order_" + id).children(".status").html("未通过审核").addClass("colorOrange");
				}else{
					alert(ret.msg);
				}
			},
			error:function(){
				alert("网络访问出错！");
			}
		});
	});

	// 发货
	$(".colorGreen").click(function(){
		$.ajax({
			type:"post",
			url:"",
			dataType:"json",
			data:{
				"MainID":id,
				"":'发货'
			},
			success:function(ret){
				if(ret.code == 0){
					$("order_" + id).children(".status").html("已发货").addClass("colorOrange");
				}else{
					alert(ret.msg);
				}
			},
			error:function(){
				alert("网络访问出错！");
			}
		});
	});
});

//页面加载
function loading(Page = 1, key_time_up = "", key_time_down = "", key_choice = "全部"){
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"PageIndex":page,
			"":key_time_up,
			"":key_time_down,
			"":key_choice
		},
		success:function(ret){
			if(ret.code == 0){
				var data = ret.data;
				var html = "";
				var htmlp = "";
				for(var i = 0; i < data.length; i++){
					var btn = "";
					if(data[i].OrState == '待发货'){
						btn = "<span class='colorGreen'>发货</span>";
					}else{
						btn = "";
					}

					html +="<tr class='order_" + data[i].MainID + "'><td><div>" + data[i].OrderNum  
							+ "</div><div>" + data[i].
							+"</div></td><td>" + data[i].UsePersonName
							+"</td><td>" + data[i].Phone
							+"</td><td>" + data[i].Address
							+"</td><td class='" + (data[i].OrState == "待审核" ? colorRed : colorOrange) + " status'>" + data[i].OrState
							+"</td><td><div class='fontGreen'><a href='orderDetail.html?id='" + data[i].MainID 
							+"><span>订单详情</span></a>&nbsp;&nbsp;&nbsp;<span data-toggle='modal' data-target='#examine'>审核</span></div>"
							+ btn +"</td></tr>"
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

// 时间查询
function search(page){
	var key_time_up = $("input[name=time_up]").val();
	var key_time_down = $("input[name=time_down]").val();
	var key_choice = $(".form-group .active").text();

	if(key_time_up == "" || key_time_down == ""){
		alert("请选择时间！");
	}else{
		if(key_time_up.replace(/-/g,"") > key_time_down.replace(/-/g,"")){
			alert("起始时间不能晚于结束时间！请重新选择时间！");
		}
	}

	$("input[name=page]").val(page);
	search(page, key_time_up, key_time_down, key_choice);
}
