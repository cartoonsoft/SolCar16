function ExibirModal() {
    if ($('#ExisteNoSistema').val() == "False" && $('#IdTipoAto').val() != 3) {
        $('.linkModal').click();
    }
}