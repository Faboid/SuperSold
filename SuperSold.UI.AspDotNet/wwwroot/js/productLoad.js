
let page = 0;
const logMessage = "Add additional scrollable page elements.";

function loadMore(url, scrollContainer) {

    $.ajax({
        url: url,
        type: 'GET',
        data: { "page": page },
        dataType: "html",
        cache: false,
    }).done(function (result) {
        console.log("Success: " + logMessage);
        scrollContainer.innerHTML += result;
        page++;
    }).fail(function (error) {
        console.log("Failed: " + logMessage);
        console.log(error);
    });

}