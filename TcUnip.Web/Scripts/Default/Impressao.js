

var configuraImpressao = {

    Imprimir: function (selector) {
        setTimeout(function () {
            configuraImpressao.Popup(document.getElementById(selector).innerHTML);
        }, 500);
    },
    Popup: function (data) {

        var mywindow = window.open('');
        mywindow.document.write('<html><head><title>Imprimir</title>');
        mywindow.document.write(configuraImpressao.StyleSheetsPage());
        mywindow.document.write('</head><body >');
        mywindow.document.write(data);
        mywindow.document.write('</body></html>');

        mywindow.document.close();
        mywindow.focus();

        mywindow.document.body.onload = function () {
            // continue to print
            mywindow.print();
            mywindow.close();
        };


        return true;
    },
    StyleSheetsPage: function () {
        var stylesPage = "";
        for (var i = 0; i < document.styleSheets.length; i++) {

            stylesPage += '<link rel="stylesheet" href="' + document.styleSheets[i].href + '" type="text/css" />'

        }
        return stylesPage;
    }

};