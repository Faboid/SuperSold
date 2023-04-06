
/**
 * Executes 'scrollableChanged' event and gives the items '.item-row' as arguments. To be used after modifying a scrollable's state.
 * 
 * @param {any} scrollable
 */
function onScrollableChanged(scrollable) {

    //get list of item rows
    items = scrollable.find('.item-row');

    //trigger event with the items
    scrollable.trigger('scrollableChanged', [items]);

}

/**
 *Refreshes all fields that use the username with the new username.
 *Meant for use on renaming user accounts.
 * @param {string} newName
 */
function onUsernameChanged(newName) {

    elements = $(document).find('.usernameTextPresenter');

    $.map(elements, function (element) {
        $(element).text(newName);
    });

}

//refreshes all fields that use the email

/**
 * Refreshes all fields that display the email with the new given email.
 * 
 * @param {string} newEmail
 */
function onEmailChanged(newEmail) {

    elements = $(document).find('.emailTextPresenter');

    $.map(elements, function (element) {
        $(element).text(newEmail);
    });

}