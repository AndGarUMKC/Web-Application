// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// To dynamically tab between Credit Card and Bank Account on Payment screen
const tab1 = $("#tab1").html();
const tab2 = $("#tab2").html();

$(document).ready(function () {
    $(".tabs-list li a").click(function (e) {
        e.preventDefault();
    });
    $("#tab2").html("");
    $(".tabs-list li").click(function () {
        var tabid = $(this).find("a").attr("href");
        $(".tabs-list li,.tabs div.tab").removeClass("active");   // removing active class from tab

        $(".tab").html("");
        $(".tab").hide();   // hiding open tab

        $(tabid).show();    // show tab
        if (tabid == "#tab1") {
            $(tabid).html(tab1);
        }
        if (tabid == "#tab2") {
            $(tabid).html(tab2);
        }
        $(this).addClass("active"); //  adding active class to clicked tab
    });
});

// To get the Organization Id on Payment Page when click DONATE on Organization Page
function GetReferrer() {
    let preUrl = document.referrer;
    let urlInt = parseInt(preUrl.split("/")[5]);
    if (urlInt != NaN || urlInt != "") {
        document.getElementById("org").value = urlInt;
    }
    else {
        window.location.href = "https://localhost:7108/Organization";
    }
}

// to switch between dark mode and light mode on check box
const toggleSwitch = document.querySelector('.theme-switch input[type="checkbox"]');

function switchTheme(e) {
    if (e.target.checked) {
        document.documentElement.setAttribute('data-theme', 'dark');
        localStorage.setItem('theme', 'dark');
    }
    else {
        document.documentElement.setAttribute('data-theme', 'light');
        localStorage.setItem('theme', 'light');
    }
}

toggleSwitch.addEventListener('change', switchTheme, false);

const currentTheme = localStorage.getItem('theme') ? localStorage.getItem('theme') : null;

if (currentTheme) {
    document.documentElement.setAttribute('data-theme', currentTheme);

    if (currentTheme === 'dark') {
        toggleSwitch.checked = true;
    }
}