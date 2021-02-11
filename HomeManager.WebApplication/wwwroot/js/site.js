$(document).ready(function () {
    profileDropDown();
});

function profileDropDown() {
    $("#dropdownMenuProfile").hover(function () {
        var dropdownMenu = $(this).children(".dropdown-menu");
        if (dropdownMenu.is(":visible")) {
            dropdownMenu.parent().toggleClass("open");
        }
    });
};
