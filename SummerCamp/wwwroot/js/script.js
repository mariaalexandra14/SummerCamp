function openMenu(id) {
    var menuOptions = document.getElementById("menu-options-" + id);

    menuOptions.style.display = (menuOptions.style.display === 'block') ? 'none' : 'block';
}

document.addEventListener('DOMContentLoaded', function () {
    document.addEventListener('click', function (event) {
        var menuOptionsList = document.querySelectorAll('.menu-options');

        menuOptionsList.forEach(menuOptions => {
            var id = menuOptions.id.replace('menu-options-', '');

            var moreIcon = document.getElementById("more-icon-" + id);


            if (!moreIcon.contains(event.target) && !menuOptions.contains(event.target)) {
                menuOptions.style.display = 'none';
            }
        });
    });
});