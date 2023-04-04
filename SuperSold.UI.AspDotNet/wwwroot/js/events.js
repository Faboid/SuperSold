
function onScrollableChanged(scrollable) {

    //get list of item rows
    items = scrollable.find('.item-row');

    //trigger event with the items
    scrollable.trigger('scrollableChanged', [items]);

}

//refreshes all fields that use the username
function onUsernameChanged(newName) {

    elements = $(document).find('.usernameTextPresenter');

    $.map(elements, function (element) {
        $(element).text(newName);
    });

}

//refreshes all fields that use the email
function onEmailChanged(newEmail) {

    elements = $(document).find('.emailTextPresenter');

    $.map(elements, function (element) {
        $(element).text(newEmail);
    });

}