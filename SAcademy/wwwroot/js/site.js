$(function () {
  var n = "#nav";
  var no = ".nav-items";
  $(n).click(function () {
    if ($(no).hasClass("nav-open")) {
      $(no).animate({ height: 0 }, 300);
      setTimeout(function () {
        $(no).removeAttr("style").removeClass("nav-open");
      }, 320);
    } else {
      var h = $(no).css("height", "auto").height();
      $(no).height(0).animate({ height: h }, 300);
      setTimeout(function () {
        $(no).removeAttr("style").addClass("nav-open");
      }, 320);
    }
  });
});


//////////Top/////
var btn = $('#top');

$(window).scroll(function () {
    if ($(window).scrollTop() > 300) {
        btn.addClass('show');
    } else {
        btn.removeClass('show');
    }
});

btn.on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({ scrollTop: 0 }, '300');
});