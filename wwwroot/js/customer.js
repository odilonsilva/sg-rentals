function clearForm() {
    $("#address").val("");
    $("#neighborhood").val("");
    $("#city").val("");
}

$(document).ready(function () {
    $('#phonenumber').mask("(00) 00000-0000");
    $('#zipcode').mask("00000-000");
    $('#zipcode').on('blur', function () {
        var cep = $(this).val().replace(/\D/g, '');

        if (cep != "") {

            var validacep = /^[0-9]{8}$/;

            if (validacep.test(cep)) {

                $("#address").val("...");
                $("#neighborhood").val("...");
                $("#city").val("...");
                
                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        $("#address").val(dados.logradouro);
                        $("#neighborhood").val(dados.bairro);
                        $("#city").val(dados.localidade);
                    } else {
                        clearForm();
                        alert("CEP não encontrado.");
                    }
                });
            } else {
                clearForm();
                alert("Formato de CEP inválido.");
            }
        } else {
            clearForm();
        }
    });
});