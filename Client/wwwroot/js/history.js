const baseUrl = "https://localhost:60107/api/transactionevent/";

$.ajax({
    url: "https://localhost:60107/api/province/",
}).done((result) => {
    console.log(result);
    optionProvince = `<option value="" disabled selected>Choose..</li>`;
    result.data.forEach(data => {
        optionProvince += `<option value="${data.guid}">${data.name}</li>`;
    });
    $('#province').html(optionProvince);
});

$("#province").change(function () {
    var selectedProvinsi = $(this).val();
    console.log(selectedProvinsi);
    if(selectedProvinsi != ""){
        enableSelect();
        $.ajax({
            url: "https://localhost:60107/api/City/province/" + selectedProvinsi,

        }).done((result) => {
            if (result.data != null) {
                console.log(result);
                optionCity = `<option value="" disabled selected>Choose..</li>`;
                result.data.forEach(data => {
                    optionCity += `<option value="${data.guid}">${data.name}</li>`;
                });
                    $('#city').html(optionCity);
            }
        });
    };
});

function disableSelect() {
    document.getElementById("city").disabled = true;
}

function enableSelect() {
    document.getElementById("city").disabled = false;
}

detail = function (id) {
    console.log(id);
    $.ajax({
        url: baseUrl + "detailByGuid/" + id,
        type: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    }).done((result) => {
        const street = result.data.street;
        const subDistrict = result.data.subDistrict;
        const district = result.data.district;
        const city = result.data.city;
        const province = result.data.province;
        const inputEventDate = new Date(result.data.eventDate).toLocaleDateString('en-CA');
        const status = changeStatus(result.data.status);

        $('#wInvoice2').val(result.data.invoice);
        $('#wFullName2').val(result.data.firstName + " " + result.data.lastName);
        $('#wEmail2').val(result.data.email);
        $('#wLocation2').val(`${street}, ${subDistrict}, ${district}, Kota ${city}, Provinsi ${province}.`);
        $('#wPackage2').val(result.data.package);
        $('#wPrice2').val(result.data.price);
        $('#wEventDate2').val(inputEventDate);
        $('#wStatusInput2').val(status);

    }).fail((error) => {
        console.log(error);
    })
}

changeStatus = function (id) {
    let status = "";
    switch (id) {
        case 0:
            status = `Canceled`;
            break;
        case 1:
            status = `Complete`;
            break;
        case 2:
            status = `Pending`;
            break;
        case 2:
            status = `Approve`;
    }
    return status;
} 