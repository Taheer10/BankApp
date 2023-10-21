


function FillMunicipalities(lstDistrictCtrl, lstMunicipalityId) {
    var lstMunicipalities = $("#" + lstMunicipalityId);
    lstMunicipalities.empty();

    var selectedDistrict = lstDistrictCtrl.options[lstDistrictCtrl.selectedIndex].value;

    if (selectedDistrict != null && selectedDistrict != '') {
        $.getJSON("/Member/GetMunicipalitiesByDistrict", { districtId: selectedDistrict }, function (municipalities) {
            if (municipalities != null && !$.isEmptyObject(municipalities)) {
                $.each(municipalities, function (index, municipality) {
                    lstMunicipalities.append($('<option/>', {
                        value: municipality.value,
                        text: municipality.text
                    }));
                });
            }
        });
    }
    return;
}

function FillWards(lstMunicipalitiesCtrl, lstWardId) {
    var lstWards = $("#" + lstWardId);
    lstWards.empty();

    var selectedMunicipalities = lstMunicipalitiesCtrl.options[lstMunicipalitiesCtrl.selectedIndex].value;

    if (lstMunicipalitiesCtrl != null && lstMunicipalitiesCtrl != '') {
        $.getJSON("/Member/GetWardsByMunicipalities", { municipalityId: selectedMunicipalities }, function (wards) {
            if (wards != null && !$.isEmptyObject(wards)) {
                $.each(wards, function (index, ward) {
                    lstWards.append($('<option/>', {
                        value: ward.value,
                        text: ward.text
                    }));
                });
            }
        });
    }
    return;
}



function FillToles(lstWardsCtrl, lstToleId) {
    var lstToles = $("#" + lstToleId);
    lstToles.empty();

    var selectedToles = lstWardsCtrl.options[lstWardsCtrl.selectedIndex].value;

    if (lstWardsCtrl != null && lstWardsCtrl != '') {
        $.getJSON("/Member/GetTolesByWards", { wardId: selectedToles }, function (toles) {
            if (toles != null && !$.isEmptyObject(toles)) {
                $.each(toles, function (index, tole) {
                    lstToles.append($('<option/>', {
                        value: tole.value,
                        text: tole.text
                    }));
                });
            }
        });
    }
    return;
}