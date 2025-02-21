$('.search-toggle').on('click', function (e) {
    $('.search-box, .search-input').toggleClass('active');
    $('.search-input input').focus();
    e.preventDefault();
});