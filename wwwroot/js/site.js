function activeMenu()
{
    const menus = document.querySelectorAll('.sidebar-custom ul.nav-pills li a');
    let currentController = window.location.pathname.split('/')[1];
    currentController = currentController == '' ? 'Home' : currentController;
    
    for (const menu of menus)
    {
        const menuController = $(menu).attr('data-controller');
        if (currentController == menuController) {
            $(menu).addClass('active');
            $(menu).removeClass('link-dark');
        } else {
            $(menu).addClass('link-dark');
            $(menu).removeClass('active');
        }
    }
}

activeMenu();

function openModalDelete(id, controller) {
    const titleMap = {
        User: 'usuário',
        Car: 'carro',
        House: 'casa',
        Customer: 'cliente',
        Booking: 'reserva'
    }
    var myModal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
    const modalBody = document.querySelector("#staticBackdrop .modal-body");
    const modalTitle = document.querySelector("#staticBackdrop .modal-title");
    const buttonOk = document.querySelector("#staticBackdrop .modal-footer button.btn.btn-danger");

    modalTitle.textContent = `Excluir ${titleMap[controller]}`;
    modalBody.textContent = `Deseja excluir esse ${titleMap[controller]}?`;
    myModal.show();

    buttonOk.addEventListener('click', function () {
        window.location = `${controller}/Delete/${id}`;
    });
}

$(document).ready(function () {
    $('table').dataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhum dado disponível",
            "info": "Mostrando _START_ - _END_ de _TOTAL_ itens",
            "infoEmpty": "Mostrando 0 a 0 de 0 itens",
            "infoFiltered": "(filtrado de _MAX_ itens totais)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ itens",
            "loadingRecords": "Carregando...",
            "processing": "",
            "search": "Buscar:",
            "zeroRecords": "No matching records found",
            "paginate": {
                "first": "Primeiro",
                "last": "Ultimo",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    });

    setTimeout(function () {
        $('.alert').fadeOut();
    }, 5000);
});