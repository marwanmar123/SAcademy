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

//////////ENd Top/////



/////////////////////increase numbers

    let nums = document.querySelectorAll(".nums .num");
    let section = document.querySelector(".three");
    let started = false; // Function Started ? No

    window.onscroll = function () {
        if (window.scrollY >= 800) {
            if (!started) {
                nums.forEach((num) => startCount(num));
            }
            started = true;
        }
    };

    function startCount(el) {
      let goal = el.dataset.goal;
      let count = setInterval(() => {
        el.textContent++;
        if (el.textContent == goal) {
          clearInterval(count);
        }
      }, 1000 / goal);
    }

/////////////////////END increase numbers



/*$(".smrnote").summernote({
    placeholder: "Certificate ",
    lineHeights: ['0.2', '0.3', '0.4', '0.5', '0.6', '0.8', '1.0', '1.2', '1.4', '1.5', '2.0', '3.0'],
    fontNames: ['Arial', 'Arial Black', 'Comic Sans MS', 'Nunito Sans', 'Nunito', 'Nunito Black'],
    tabsize: 2,
    height: 120,
    toolbar: [
        ["style", ["style"]],
        ["font", ["bold", "underline", 'italic', "clear"]],
        ['font', ['strikethrough', 'superscript', 'subscript']],
        ['fontsize', ['fontsize']],
        ["color", ["color"]],
        ["para", ["ul", "ol", "paragraph"]],
        ['height', ['height']],
        ['fontname', ['fontname']],
        ["table", ["table"]],
        ["insert", ["link", "picture", "video"]],
        ["view", ["fullscreen", "codeview", "help"]],
    ],
});*/



//function storePagePosition() {
//  var page_y = window.pageYOffset;
//  localStorage.setItem("page_y", page_y);
//}
//window.addEventListener("scroll", storePagePosition);
//var currentPageY;
//try {
//  currentPageY = localStorage.getItem("page_y");
//  if (currentPageY === undefined) {
//      localStorage.setItem("page_y") = 0;
//  }
//  window.scrollTo(0, currentPageY);
//} catch (e) {
//  // no localStorage available
//}


function submitForm(form) {
    swal({
        title: "Merci!",
        text: "Votre message a été envoyé.",
        icon: "success",
        buttons: false,
        dangerMode: false,
    })
        .then(function (isOkay, e) {
            e.preventDefault();
            if (isOkay) {
                form.submit();

            }
        });
    return true;
}






