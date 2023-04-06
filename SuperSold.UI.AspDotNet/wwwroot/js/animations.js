
/**
 * Pulls up the given element, then removes it from the document, essentially deleting it.
 * 
 * @param {any} element
 */
function pullUp(element) {
    element.animate({ height: 'toggle', opacity: '0%' }, function () {
        //delete element afterward
        //todo - rename pullUp to better reflect its behavior
        element.remove();
    });
}
