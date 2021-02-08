const darkmodeSwitch = document.getElementById('darkModeSwitch');


$(document).ready(function () {
    if (darkmodeSwitch) {
        if ($('#gloableUserId').val() == "") {
            initTheme();
        }
    }
    $('#darkModeSwitch').change(function () {
        switchTheme()
        var bool = document.getElementById('darkModeSwitch').checked;
        var userId = $('#gloableUserId').val();
        if (userId != "") {
            UpdateUserDarkMode(bool, userId);
        }
    });
});

function UpdateUserDarkMode(bool, userId) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "Home/UpdateUserDarkMode",
        data: { darkModeBool: bool, userId: userId },
    });
};

function initTheme() {
    const darkThemeSelected =
        localStorage.getItem('darkModeSwitch') !== null &&
        localStorage.getItem('darkModeSwitch') === 'dark';
    darkmodeSwitch.checked = darkThemeSelected;
    darkThemeSelected ? document.body.setAttribute('data-theme', 'dark') :
        document.body.removeAttribute('data-theme');
}

function switchTheme() {
    if (darkmodeSwitch.checked) {
        document.body.setAttribute('data-theme', 'dark');
        localStorage.setItem('darkModeSwitch', 'dark');
    } else {
        document.body.removeAttribute('data-theme');
        localStorage.removeItem('darkModeSwitch');
    }
}