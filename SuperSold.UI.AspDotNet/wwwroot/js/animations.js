

function pullUp(element) {
    element.animate({ height: 'toggle', opacity: '0%' }, function () {
        //delete element afterward
        //todo - rename pullUp to better reflect its behavior
        element.remove();
    });
}
