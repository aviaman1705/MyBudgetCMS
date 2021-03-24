$(document).ready(function () {
    // Header Scroll
    $(window).on('scroll', function () {
        var scroll = $(window).scrollTop();

        if (scroll >= 50) {
            $('#header').addClass('fixed');
        } else {
            $('#header').removeClass('fixed');
        }
    });


    // Page Scroll
    var sections = $('section')
    nav = $('nav[role="navigation"]');

    $(window).on('scroll', function () {
        var cur_pos = $(this).scrollTop();
        sections.each(function () {
            var top = $(this).offset().top - 76
            bottom = top + $(this).outerHeight();
            if (cur_pos >= top && cur_pos <= bottom) {
                nav.find('a').removeClass('active');
                nav.find('a[href="#' + $(this).attr('id') + '"]').addClass('active');
            }
        });
    });
    nav.find('a').on('click', function () {
        var $el = $(this)
        id = $el.attr('href');
        $('html, body').animate({
            scrollTop: $(id).offset().top - 75
        }, 500);
        return false;
    });

    // Mobile Navigation
    $('.nav-toggle').on('click', function () {
        $(this).toggleClass('close-nav');
        nav.toggleClass('open');
        return false;
    });
    nav.find('a').on('click', function () {
        $('.nav-toggle').toggleClass('close-nav');
        nav.toggleClass('open');
    });


    $('#btnGetSentence').click(function (e) {
        // prevent the default event behaviour
        e.preventDefault();
               
        $('#sentenceLbl').text('אנא המתן...');
        
        $.ajax({
            url: "/Home/GetSentence",
            type: "POST",
            data: {},
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                setTimeout(function () {
                    delaySuccess(data);
                }, 2000);
            },
            error: function () {
                alert("An error has occured!!!");
            }
        });
    });
});

function delaySuccess(data) {    
    $('#sentenceLbl').text(data);
}