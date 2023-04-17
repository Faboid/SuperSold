class ScrollableObjectInfo {
    constructor(url, scrollContainer) {
        this.page = 0;
        this.url = url;
        this.scrollContainer = scrollContainer;
    }
}

let page = 0;
const logMessage = "Add additional scrollable page elements.";

/**
 * Loads product item row with the given format in the scrollable.
 * 
 * @param {GUID} productId
 * @param {ScrollableObjectInfo} scrollable
 * @param {string} itemRowFormat
 */
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

/**
 * Loads product item row with the given format in the scrollable.
 * 
 * @param {GUID} relationshipId
 * @param {ScrollableObjectInfo} scrollable
 * @param {string} itemRowFormat
 */
function loadSavedRelationshipInScrollable(relationshipId, scrollable, itemRowFormat) {

    $.ajax({
        url: "/SavedRelationships/Get",
        type: 'GET',
        data: { "relationshipId": relationshipId, "itemRowFormat": itemRowFormat },
        dataType: "html",
        cache: true,
    }).done(function (result) {

        console.log("Successfully retrieved relationship " + relationshipId);
        scrollable.append(result);
        onScrollableChanged(scrollable);

    }).fail(function (error) {

        if (error.status == 404) {
            console.log("The relationship " + relationshipId + " is missing!");
            return;
        }

        console.warn("Failed retrieving item id: " + relationshipId + "\n" + error);
    });

}

/**
 * Loads an additional page for the given scrollable.
 * 
 * @param {any} scrollableInfo
 */
function loadMorePageOnly(scrollableInfo) {
    var data = { "page": scrollableInfo.page };
    loadMore(scrollableInfo, data);
}

/**
 * Loads an additional page using search results based on given search parameter for the given scrollable.
 * 
 * @param {any} scrollableInfo
 * @param {any} search
 */
function loadMoreWithSearch(scrollableInfo, search) {
    var data = { "page": scrollableInfo.page, "search": search };
    loadMore(scrollableInfo, data);
}

/**
 * Loads an additional page for the given scrollable by using the given data.
 * 
 * @param {any} scrollableInfo
 * @param {any} data
 */
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

/**
 * Appends the given result(intended to be used after an ajax query) to the scrollable.
 * 
 * @param {any} scrollable
 * @param {any} result
 */
function addResultToScrollable(scrollable, result) {

    $(result).map(function () {
        return $('<div>').append(this).html();
    }).each(function (index, element) {
        addItemToScrollable(scrollable, index, element);
    });

}

/**
 * Appends a single item to the scrollable.
 * 
 * @param {any} scrollable
 * @param {any} index
 * @param {any} item
 */
function addItemToScrollable(scrollable, index, item) {

    var adjustedIndex = index + 1;
    var data = $(item);
    data.hide();
    scrollable.append(data);
    data.slideDown(adjustedIndex * 200 + (200 / adjustedIndex));

}