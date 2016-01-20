$(document).ready(function () {
    $('#welcomeBody').fadeIn(1500);
    $('#globeIcon').on('click', function () {
        if (!$(this).hasClass('clicked-icon')) {
            $(this).addClass('clicked-icon'); 
            $(this).parents('body').fadeOut(2000);
            window.location = '/Home/Map';
        };
    });
});

