class ScrollableObjectInfo {
    constructor(url, scrollContainer) {
        this.page = 0;
        this.url = url;
        this.scrollContainer = scrollContainer;
    }
}

let page = 0;
const logMessage = "Add additional scrollable page elements.";

function loadMorePageOnly(scrollableInfo) {

    $.ajax({
        url: scrollableInfo.url,
        type: 'GET',
        data: { "page": scrollableInfo.page },
        dataType: "html",
        cache: false,
    }).done(function (result) {
        console.log("Success: " + logMessage);
        scrollableInfo.scrollContainer.append(result);
        scrollableInfo.page++;
        onScrollableChanged(scrollableInfo.scrollContainer);
    }).fail(function (error) {
        console.log("Failed: " + logMessage);
        console.log(error);
    });

}

function loadMoreWithSearch(scrollableInfo, search) {

    $.ajax({
        url: scrollableInfo.url,
        type: 'GET',
        data: { "page": scrollableInfo.page, "search": search },
        dataType: "html",
        cache: false,
    }).done(function (result) {
        console.log("Success: " + logMessage);
        scrollableInfo.scrollContainer.append(result);
        scrollableInfo.page++;
        onScrollableChanged(scrollableInfo.scrollContainer);
    }).fail(function (error) {
        console.log("Failed: " + logMessage);
        console.log(error);
    });

}