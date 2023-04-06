// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/**
 * Sends a message to the user by using a partial view's container object defined by its class.
 * 
 * @param {string} message
 */
function notifyUser(message) {

    console.log("User message: " + message);
    $(".notifyUserTextPresenter").text(message);
    $(".notifyUserContainer").slideDown("swing");

}