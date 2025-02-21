
$(document).ready(function () {

	$(".currency").each(function () {
		var value = $(this).text().replace('.', '').replace(',', '.');
		value = parseFloat(value);

		if (!isNaN(value)) {
			$(this).text(value.toLocaleString('id-ID', { style: 'currency', currency: 'IDR' }));
		}
	});

	$(".percentage").each(function () {
		var value = parseFloat($(this).text());
		$(this).text((value * 100).toFixed(2) + "%");
	});

	setTimeout(function () {
		$('body').addClass('sidebar-folded');
	}, 500);

});


var UrlOrigin = window.location.origin;
var UrlPath = window.location.pathname.split('/');
var NewUrl = new URL(window.location);

function showLoading() {
	$("div.spanner").addClass("show");
	$("div.overlay").addClass("show");
}

function stopLoading() {
	$("div.spanner").removeClass("show");
	$("div.overlay").removeClass("show");
}
