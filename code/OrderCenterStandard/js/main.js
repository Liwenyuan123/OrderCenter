$(function () {
    
    $("#bd").height($(window).height() - $("#hd").outerHeight());
    $("#mainIframe").height($(window).height() - $("#hd").outerHeight());
   
    $('.munLi').click(function () {
        var munUrl = $(this).attr('munUrl');        
        $(this).parent().find('.changeColor:eq(0)').children('span:eq(0)').find('img:eq(1)').remove();
        
        $(this).siblings().removeClass('changeColor');
        $(this).addClass('changeColor');
        $(this).find('span:eq(0)').append('<img class="current" src="../image/current.png" />');
        
        $("#mainIframe").attr("src", munUrl);
    })
})