// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let table = new DataTable('#myTable');

// şu kadar saniye sonra çalıştır anlamını çalıştır
setTimeout(() => {
    $(".notification").fadeOut("slow")
}, 3000);

$(".dt-length label").html("");
$(".dt-search label").html("Arama:")
$(".dt-info").html("");

var loadFile = function (event) {
    var output = document.getElementById("imageOutput");
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src);
    }
}

var phoneInput = document.getElementById('phone');
var myForm = document.forms.myForm;
var result = document.getElementById('result');  // only for debugging purposes

phoneInput.addEventListener('input', function (e) {
    var x = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
    e.target.value = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
});

myForm.addEventListener('submit', function (e) {
    phoneInput.value = phoneInput.value.replace(/\D/g, '');
    result.innerText = phoneInput.value;  // only for debugging purposes
    e.preventDefault(); // You wouldn't prevent it
});
