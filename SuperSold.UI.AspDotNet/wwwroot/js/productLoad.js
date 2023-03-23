class ScrollableObjectInfo {
    constructor(url, scrollContainer) {
        this.page = 0;
        this.url = url;
        this.scrollContainer = scrollContainer;
    }
}

let page = 0;
const logMessage = "Add additional scrollable page elements.";

function loadProductInScrollable(productId, scrollable, itemRowFormat) {

    $.ajax({
        url: "/Products/Get",
        type: 'GET',
        data: { "id": productId, "itemRowFormat": itemRowFormat },
        dataType: "html",
        cache: true,
    }).done(function (result) {

        console.log("Successfully retrieved product id " + productId);
        scrollable.append(result);
        onScrollableChanged(scrollable);

    }).fail(function (error) {

        if (error.status == 404) {
            console.log("The item " + productId + " is missing!");
            return;
        }

        console.warn("Failed retrieving item id: " + productId + "\n" + error);
    });

}

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