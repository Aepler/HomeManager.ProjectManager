const darkmodeSwitch = document.getElementById('darkmodeSwitch');
const manage = document.getElementById('manage');


$(document).ready(function () {
    if (!manage) {
        initTheme();
    }
    $('#darkmodeSwitch').change(function () {
        switchTheme()
        var bool = darkmodeSwitch.checked;
        if (manage) {
            UpdateUserDarkmode(bool);
        }
    });
});

function UpdateUserDarkmode(bool) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "Home/UpdateUserDarkmode",
        data: { darkmodeBool: bool },
    });
};

function initTheme() {
    const darkThemeSelected =
        localStorage.getItem('darkmodeSwitch') !== null &&
        localStorage.getItem('darkmodeSwitch') === 'dark';
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
        localStorage.removeItem('darkmodeSwitch');
    }
}