// 页面加载
function loading(page = 1, id){
	var id = $("input[name=orderDetail_id]").val();
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"UID":id,
			"pageIndex":page
		},
		success:function(ret){
			if(ret.code == 0){
				var list = ret.data.OrderDetail;
				var html = "";
				var htmlp = "";
				for(var i = 0; i < list.length; i++){
					html +="<tr><td>" + (i + 1) + "<input type='hidden' name='id' /></td><td>" + list[i].ComName
							+"</td><td>" + list[i].Standard
							+"</td><td>" + list[i].PricePlan + "/" + list[i].Unit
							+"</td><td><input type='text' name='number' readonly='readonly' class='hideBorder' value='" + list[i].NumPlan 
							+ "' /></td><td></td><td><input type='text' name='totalPrice' readonly='readonly' class='hideBorder' value='" + list[i].PriceSumPlan
							+ "' /></td></tr>"
				}

				//分页
				htmlp +="<span>共计：" + list.PageCount
						+"条</span><span>第<p class='page'>" + list.PageIndex
						+"页 共 " + list.PageTotal
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

// 修改
function updateData(){
	$("input[name=number]").removeClass("hideBorder").removeAttr("readonly");
	$("input[name=totalPrice]").removeClass("hideBorder").removeAttr("readonly");
}

function saveData(id){
	var id = $("input[name=id]").val();
	var number = $("input[name=number]").val();
	var totalPrice = $("input[name=totalPrice]").val();
	$.ajax({
		type:"post",
		url:"",
		dataType:"json",
		data:{
			"UID":id
			"NumReal":number,
			"PriceSumReal":totalPrice
		},
		success:function(ret){
			if(ret.code == 0){
				var list = ret.data.OrderDetail;
				var html = "";
				var htmlp = "";
				for(var i = 0; i < list.length; i++){
					html +="<tr><td>" + (i + 1) + "<input type='hidden' name='id' /></td><td>" + list[i].ComName
							+"</td><td>" + list[i].Standard
							+"</td><td>" + list[i].PricePlan + "/" + list[i].Unit
							+"</td><td><input type='text' name='number' readonly='readonly' class='hideBorder' value='" + list[i].NumReal 
							+ "' /></td><td></td><td><input type='text' name='totalPrice' readonly='readonly' class='hideBorder' value='" + list[i].PriceSumreal
							+ "' /></td></tr>"
				}

				//分页
				htmlp +="<span>共计：" + list.PageCount
						+"条</span><span>第<p class='page'>" + list.PageIndex
						+"页 共 " + list.PageTotal
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

// 打印
function print(){
	var id = $("input[name=id]").val();
	window.location.href = '?' + id;
}