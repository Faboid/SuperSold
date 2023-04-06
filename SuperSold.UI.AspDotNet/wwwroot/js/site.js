// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//sends a message to the user. Currently uses alert, but will implement a partial view in the future
function notifyUser(message) {

    console.log("User message: " + message);
    $(".notifyUserTextPresenter").text(message);
    $(".notifyUserContainer").slideDown("swing");

}