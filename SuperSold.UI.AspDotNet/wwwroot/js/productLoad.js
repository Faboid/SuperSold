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
    var data = { "page": scrollableInfo.page };
    loadMore(scrollableInfo, data);
}

function loadMoreWithSearch(scrollableInfo, search) {
    var data = { "page": scrollableInfo.page, "search": search };
    loadMore(scrollableInfo, data);
}

function loadMore(scrollableInfo, data) {

    $.ajax({
        url: scrollableInfo.url,
        type: 'GET',
        data: data,
        dataType: "html",
        cache: false,
    }).done(function (result) {
        console.log("Success: " + logMessage);
        addResultToScrollable(scrollableInfo.scrollContainer, result);
        scrollableInfo.page++;
        onScrollableChanged(scrollableInfo.scrollContainer);
    }).fail(function (error) {
        console.log("Failed: " + logMessage);
        console.log(error);
    });

}

function addResultToScrollable(scrollable, result) {

    $(result).map(function () {
        return $('<div>').append(this).html();
    }).each(function (index, element) {
        addItemToScrollable(scrollable, index, element);
    });

}

function addItemToScrollable(scrollable, index, item) {

    var adjustedIndex = index + 1;
    var data = $(item);
    data.hide();
    scrollable.append(data);
    data.slideDown(adjustedIndex * 200 + (200 / adjustedIndex));

}