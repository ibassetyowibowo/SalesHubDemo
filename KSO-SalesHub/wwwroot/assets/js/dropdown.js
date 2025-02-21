// Handling click event on the element with class 'sidebar-menu'
$('.sidebar .sidebar-menu li a').on('click', function () {
    const $this = $(this);

    if ($this.parent().hasClass('open')) {
        $this.parent().children('.dropdown-menu').slideUp(200, function () {
            $this.parent().removeClass('open');
        });
    } else {
        $this.parent().parent().children('li.open').children('.dropdown-menu').slideUp(200);
        $this.parent().parent().children('li.open').children('a').removeClass('open');
        $this.parent().parent().children('li.open').removeClass('open');
        $this.parent().children('.dropdown-menu').slideDown(200, function () {
            $this.parent().addClass('open');
        });
    }
});

// Finding and handling active links in the sidebar
const sidebarLinks = $('.sidebar').find('.sidebar-link');
sidebarLinks.each(function (index, el) {
    $(el).removeClass('active');
}).filter(function () {
    const href = $(this).attr('href');
    const pattern = href[0] === '/' ? href.substr(1) : href;
    return pattern === window.location.pathname.substr(1);
}).addClass('active');

// Handling click event on the element with class 'sidebar-toggle'
$('.sidebar-toggle').on('click', function (e) {
    $('.app').toggleClass('is-collapsed');
    e.preventDefault();
});

// Handling click event on the element with ID 'sidebar-toggle'
$('#sidebar-toggle').click(function (e) {
    e.preventDefault();
    setTimeout(function () {
        window.dispatchEvent(new Event('resize'));
    }, 300);
});