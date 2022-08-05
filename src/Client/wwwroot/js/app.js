



window.addEventListener('load', function (e) {
    
});

$(window).on('load', function () {
    //Activating all the plugins
    //setTimeout(init_template, 0);
})

$(document).ready(function () {
    function init_template() {

        //QR Generator
        var generateQR = $('.generate-qr-result, .generate-qr-auto');

        function activate_qr_generator() {
            //QR Code Generator 
            var qr_auto_link = 'http://tlssoftwarevn.com';
            var qr_api_address = 'https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=';

            $('.generate-qr-auto').attr('src', qr_api_address + qr_auto_link)
            $('.generate-qr-button').on('click', function () {
                if ($(this).parent().find('.fa').hasClass('fa-exclamation-triangle')) {
                    console.log('Invalid URL');
                } else {
                    var get_qr_url = $('.generate-qr-input').val();
                    if (!get_qr_url == '') {
                        $('.generate-qr-result').empty();
                        setTimeout(function () {
                            $('.generate-qr-result').append('<img class="mx-auto polaroid-effect shadow-l mt-4 delete-qr" width="200" src="' + qr_api_address + get_qr_url + '" alt="img"><p class="font-11 text-center mb-0">' + get_qr_url + '</p>')
                        }, 30);
                    }
                }
            });
        }
        if (generateQR.length) { activate_qr_generator(); }
    }

    setTimeout(init_template, 0);
});

