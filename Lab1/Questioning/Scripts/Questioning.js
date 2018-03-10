$(document).ready(function () {
    $("input[type='radio']").change(function () {
        var newId = $(this).attr("value");
        //update all pagination links
        $(".pagination > li > a").each(function (index) {
            var oldHrefSplit = $(this).attr("href").split("=");
            oldHrefSplit.pop();
            oldHrefSplit.join("=") + newId
            $(this).attr("href", oldHrefSplit.join("=") + "=" + newId);
        });
        //update finish link
        $("a#finish-test").each(function (index) {
            var oldHrefSplit = $(this).attr("href").split("=");
            oldHrefSplit.pop();
            oldHrefSplit.join("=") + newId
            $(this).attr("href", oldHrefSplit.join("=") + "=" + newId);
        });
        //update next link
        $("a#next-question").each(function (index) {
            var oldHrefSplit = $(this).attr("href").split("=");
            oldHrefSplit.pop();
            oldHrefSplit.join("=") + newId
            $(this).attr("href", oldHrefSplit.join("=") + "=" + newId);
        });
    });
});