

/// <reference path="~\Scripts/jquery-1.10.2.min.js" />



$(function () {
   
    $("#AddButoon").click(function () {

        $.ajax({
            url:'VeLogData/Index',
            datatype: 'html',
            success: function (data) {
                $("#addViews").append( data);
            }
        })

    });
});