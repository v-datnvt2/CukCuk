$(".button.add-employee").on("click", function() {
    $(".popup.add-employee").css('display', "block");
})
$(".popup .btn-close").on("click", function() {
    $(".popup.add-employee").css('display', "none");
})

$(".button.cancel").on("click", function() {
    $(".popup.add-employee").css('display', "none");
})