
function onScrollableChanged(scrollable) {

    //get list of item rows
    items = scrollable.find('.item-row');

    //trigger event with the items
    scrollable.trigger('scrollableChanged', [items]);

}