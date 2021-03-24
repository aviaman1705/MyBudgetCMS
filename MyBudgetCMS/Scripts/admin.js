$(document).ready(function () {
    $(".dropdown-toggle").dropdown();

    var loc = window.location.pathname;

    $('#sidebar-nav').find('a').each(function () {
        if ($(this).attr('href') === loc)
            $(this).parent('li').addClass('active');
    });

    $('#topBtn').on("click", function () {
        window.scrollTo(0, 0);
    });

    $.ajax({
        url: "/MyBudgetCMS/Admin/Search/Index",
        type: "get", //send it through get method
        data: {
            searchStr: $('#main-search').val()
        },
        success: function (response) {
            console.log(response);
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
});