$('#checkBoxMain').click(function () {
    $('#table input:checkbox').prop('checked', $(this).is(':checked') ? true : false);
});
$('#table input:checkbox').click(function () {
    $('#checkBoxMain').prop('checked', false);
});