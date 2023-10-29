$(document).ready(function () {
    $('#licensePlate').on('keypress', (function () {
        $(this).val($(this).val().toUpperCase());
    }));

    $('#price').mask("#.##0,00", { reverse: true });

    let brandCreate = document.querySelector('#create #brand');
    if (brandCreate.value != "") {
        getBrandModel($('#brand'));
    }

    $('#brand').change(function () {
        getBrandModel(this);
    });

    function getBrandModel(brand) {
        const selected = $(brand).val();
        $.getJSON('/Car/GetCarModels/' + selected, function (result) {
            $('#model option').remove();
            $('#model').append('<option value="">Escolha o modelo</option>');
            $.each(result, function (index, item) {
                $('#model').append('<option value="' + item.id + '">' + item.name + '</option>');
            });
        });
    }
});