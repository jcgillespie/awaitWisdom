(function (window, document) {
var menu = document.getElementById('top-nav'),
    WINDOW_CHANGE_EVENT = ('onorientationchange' in window) ? 'orientationchange':'resize';

function toggleHorizontal() {
    [].forEach.call(
        document.getElementById('nav-link-list').querySelectorAll('.nav-link'),
        function(el){
            el.classList.toggle('horizontal');
        }
    );
};

function toggleMenu() {
    // set timeout so that the panel has a chance to roll up
    // before the menu switches states
    if (menu.classList.contains('open')) {
        setTimeout(toggleHorizontal, 500);
    }
    else {
        toggleHorizontal();
    }
    menu.classList.toggle('open');
    document.getElementById('nav-toggle').classList.toggle('x');
};

function closeMenu() {
    if (menu.classList.contains('open')) {
        toggleMenu();
    }
}

document.getElementById('nav-toggle').addEventListener('click', function (e) {
    toggleMenu();
});

window.addEventListener(WINDOW_CHANGE_EVENT, closeMenu);
})(this, this.document);