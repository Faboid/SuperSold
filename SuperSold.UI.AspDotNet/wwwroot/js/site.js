// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const notifyContainer = document.getElementsByClassName("notify-user-container")[0];
const textPresenter = document.getElementsByClassName("notify-user-text-presenter")[0];

/**
 * Sends a message to the user by using a partial view's container object defined by its class.
 * 
 * @param {string} message
 */
function notifyUser(message) {

    console.log("User message: " + message);

    textPresenter.textContent = message;
    notifyContainer.setAttribute("data-visibility", "visible");

}

function redirectTo(url) {
    window.location.href = url;
}