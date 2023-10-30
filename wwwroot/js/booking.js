$(document).ready(function () {
    toogleFields();
    $('#type').change(function () {
        toogleFields();
    });
    $('#CarId, #HouseId').change(function () {
        getBookingPrice();
    });

    $('#rangeDate').daterangepicker({
        opens: 'left',
        language: 'pt_br',
        startDate: $('#initDate').val(),
        endDate: $('#endDate').val(),
        locale: {
            format: 'DD/MM/YYYY'
        },
    }, function (start, end, label) {
        $('#initDate').val(start.format('YYYY-MM-DD'));
        $('#endDate').val(end.format('YYYY-MM-DD'));
        getBookingPrice();
    });

    $('#price').mask("#.##0,00", { reverse: true });

    function getBookingPrice() {
        let initDate = $('#initDate').val();
        let endDate = $('#endDate').val();
        const type = $('#type').val();
        const car = $('#CarId').val();
        const house = $('#HouseId').val();

        if (initDate == '' ||
            endDate == '' ||
            (type == 0 && car == '') ||
            (type == 1 && house == '')
        ) return

        initDate = moment(initDate);
        endDate = moment(endDate);
        const bookingQtd = endDate.diff(initDate, 'day');
        const itemId = type == 0 ? car : house;

        if (bookingQtd == 0 || isNaN(bookingQtd)) {
            return;
        }

        $.getJSON(`/Booking/GetPrice?type=${type}&days=${bookingQtd}&itemId=${itemId}`, function (result) {
            $('#price').val(result);
        });
    }

    function toogleFields() {
        const typeVal = $('#type').val();
        const isEdit = document.querySelector('#id') == null ? false : true;
        
        if (!isEdit) {
            $('#price').val('');
        }
        if (typeVal == 0) {
            $('#HouseId').val(0);
            $('.container-house').hide();
            $('.container-car').show();
        } else {
            if (!isEdit) {
                getHouses();
            }
            $('#CarId').val(0);
            $('.container-car').hide();
            $('.container-house').show();
        }
    }

    function getHouses(brand) {
        const selected = $(brand).val();
        $.getJSON('/Booking/GetHouses', function (result) {
            $('#HouseId option').remove();
            $('#HouseId').append('<option value="">Selecione uma casa</option>');
            $.each(result, function (index, item) {
                $('#HouseId').append('<option value="' + item.id + '">' + item.description + '</option>');
            });
        });
    }
});