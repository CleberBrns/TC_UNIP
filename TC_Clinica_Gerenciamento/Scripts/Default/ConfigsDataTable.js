
/**
 * Função "portugues"
 * Essa configuração permite a ordenação dos campos,
   considerando os caracteres especiais.
 * É preciso incluir a chamada na coluna onde deseja utilizar a configuração.
   Exemplo;

        $("#grid").DataTable({
            "aoColumns": [                
                { "sType": "portugues" }
            ]
        });

    * Função "data"
    * Essa configuração permite a ordenação dos campos,
      considerando os caracteres especiais.
    * É preciso incluir a chamada na coluna onde deseja utilizar a configuração.
        Exemplo;

        $("#grid").DataTable({
            "aoColumns": [                
                { "sType": "data" }
            ]
        });

 */

jQuery.extend(jQuery.fn.dataTableExt.oSort, {

    //Ordenação por data
    "date-br-pre": function (a) {
        var brDatea = a.split('/');
        return (brDatea[2] + brDatea[1] + brDatea[0]) * 1;
    },

    "date-br-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-br-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    },

    //Ordenação por string
    "portugues-pre": function (data) {
        var a = 'a';
        var e = 'e';
        var i = 'i';
        var o = 'o';
        var u = 'u';
        var c = 'c';
        var special_letters = {
            "Á": a, "á": a, "Ã": a, "ã": a, "À": a, "à": a,
            "É": e, "é": e, "Ê": e, "ê": e,
            "Í": i, "í": i, "Î": i, "î": i,
            "Ó": o, "ó": o, "Õ": o, "õ": o, "Ô": o, "ô": o,
            "Ú": u, "ú": u, "Ü": u, "ü": u,
            "ç": c, "Ç": c
        };
        for (var val in special_letters)
            data = data.split(val).join(special_letters[val]).toLowerCase();
        return data;
    },
    "portugues-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "portugues-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});

jQuery.extend(jQuery.fn.dataTable.defaults, {
    language: {
        url: "https://cdn.datatables.net/plug-ins/1.10.16/i18n/Portuguese-Brasil.json"
    },
    "order": []//Para não aparecer a seta de ordenação na primeira coluna
});