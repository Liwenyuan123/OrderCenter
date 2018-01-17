$(function () {
    
    $("#bd").height($(window).height() - $("#hd").outerHeight());
    $("#mainIframe").height($(window).height() - $("#hd").outerHeight());
    console.log(localStorage.UserName);
    $('#sys_name').html(localStorage.UserName);
    $('.menu_childen').hide();
    $('.munLi').click(function () { 
        $(this).parent().parent().find('.changeColor:eq(0)').find('img:eq(1)').remove();

        $(this).parent().siblings().removeClass('changeColor');
        $(this).parent().addClass('changeColor');
        $(this).parent().append('<img class="current" src="../image/current.png" />');
        //显示二级菜单
        $(this).parent().parent().children().find('ul').hide();
        $(this).parent().find('ul').show(500);
        
    });
    $('.menu_childen li').click(function () {
        var munUrl = $(this).attr('munUrl');
        $("#mainIframe").attr("src", munUrl);
    });
    $('.logout').click(function () {
        
        window.localStorage.clear();
        window.location = '../login.html';
    });
})