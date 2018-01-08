$(function () {
    
    $("#bd").height($(window).height() - $("#hd").outerHeight());
    $("#mainIframe").height($(window).height() - $("#hd").outerHeight());
   
    $('.munLi').click(function () {
        var munUrl = $(this).attr('munUrl');
        //old
        var oldElement = $(this).siblings().find('.changeColor:eq(0)');
        console.log(typeof oldElement);
        oldElement.children('span:eq(0)').children().find('img:eq(1)').remove();
        $(this).siblings().removeClass('changeColor');
        $(this).addClass('changeColor');
        $(this).find('span:eq(0)').append('<img class="current" src="../image/current.png" />');
        $(this).siblings().children().find('span:eq(0)').children().find('.current').remove();
        $("#mainIframe").attr("src", munUrl);
    })
})