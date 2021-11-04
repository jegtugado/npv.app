// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Requires jQuery
var dialogHelper = new dialog();

function dialog() {
    /* Bootstrap Modal Dialog
    *  Displays message from parameter
    */
    this.ShowModalDialog = function (message, title, buttons) {
        var dialogMessage = '';
        var dialogTitle = 'System Message';
        var dialogButtons = [];

        if (message)
            dialogMessage = message;
        if (title)
            dialogTitle = title;
        if (buttons) {
            dialogButtons = buttons;
        }

        var id = randomString(10);

        jQuery('<div/>', {
            id: id,
            class: 'modal fade',
            // href: 'http://google.com',
            //title: title,
            //rel: 'external',
            //text: message
        })
            .attr('tabindex', '-1')
            .attr('role', 'dialog')
            .attr('aria-labelledby', id + 'Label')
            .attr('aria-hidden', true)
            .attr('data-backdrop', 'static')
            .attr('data-keyboard', false)
            .load('bootstrap-modal-template.html', function () {
                const $this = $(this);
                $this.find('.modal-title')
                    .attr('id', id + 'Label')
                    .text(dialogTitle);
                $this.find('.modal-body', this).html(dialogMessage);

                var footer = $this.find('.modal-footer', this);

                dialogButtons.forEach(function (element) {
                    $('<button/>', {
                        text: element.Text,
                        click: function () {
                            if (element.Event) {
                                element.Event();
                            }
                        },
                        class: element.Class
                    })
                    .attr('data-bs-dismiss', 'modal')
                    .appendTo(footer);
                });                
                $this.appendTo(document.body)
                    .modal('show');
                $this.on('hidden.bs.modal', e => {
                    e.currentTarget.remove();
                });
            });
    };
};

/* Automatically destroy modal on close */
//$('.modal').on('hidden.bs.modal', function (e) {
//    console.log('destroy e');
//    e.currentTarget.remove();
//});

function randomString(length) {
    var chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz';
    var string_length = length;
    var randomstring = '';

    for (var i = 0; i < string_length; i++) {
        var rnum = Math.floor(Math.random() * chars.length);
        randomstring += chars.substring(rnum, rnum + 1);
    }

    return randomstring;
};