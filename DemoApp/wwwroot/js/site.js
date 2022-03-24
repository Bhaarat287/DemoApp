// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Load Data in Table when documents is ready  

$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({
        url: APIUrl + "Address",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        headers: { "Apikey": apiKey },
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.id + '</td>';
                html += '<td>' + item.line1 + '</td>';
                html += '<td>' + item.line2 + '</td>';
                html += '<td>' + item.city + '</td>';
                html += '<td>' + item.postCode + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (message) {            
            alert(message.statusText + ": Unauthorised");
        }
    });
}

//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#Id').val(),
        line1: $('#Line1').val(),
        line2: $('#Line2').val(),
        city: $('#City').val(),
        postCode: $('#PostCode').val()
    };
    $.ajax({
        url: APIUrl + "Address",
        data: JSON.stringify(empObj),
        type: "POST",
        headers: { "Apikey": apiKey },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (message) {
            alert(message.statusText + ": Unauthorised");
        }
    });
}

//Function for getting the Data Based upon Address Id  
function getbyID(EmpID) {
    $('#Id').css('border-color', 'lightgrey');
    $('#Line1').css('border-color', 'lightgrey');
    $('#Line2').css('border-color', 'lightgrey');
    $('#City').css('border-color', 'lightgrey');
    $('#PostCode').css('border-color', 'lightgrey');
    $.ajax({
        url: APIUrl + "Address/GetById/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        headers: { "Apikey": apiKey },
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.id);
            $('#Line1').val(result.line1);
            $('#Line2').val(result.line2);
            $('#City').val(result.city);
            $('#PostCode').val(result.postCode);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (message) {
            alert(message.statusText + ": Unauthorised");
        }
    });
    return false;
}

//function for updating Address's record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        Id: $('#Id').val(),
        line1: $('#Line1').val(),
        line2: $('#Line2').val(),
        city: $('#City').val(),
        postCode: $('#PostCode').val(),
    };
    $.ajax({
        url: APIUrl + "Address?id=" + empObj.Id,
        data: JSON.stringify(empObj),
        type: "put",
        contentType: "application/json;charset=utf-8",
        headers: { "Apikey": apiKey },
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#Line1').val("");
            $('#Line2').val("");
            $('#City').val("");
            $('#PostCode').val("");
        },
        error: function (message) {
            alert(message.statusText + ": Unauthorised");
        }
    });
}

//function for deleting Address's record  
function Delele(Id) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: APIUrl + "Address?id=" + Id,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            headers: { "Apikey": apiKey },
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (message) {
                alert(message.statusText + ": Unauthorised");
            }
        });
    }
}
//function to close the modal
function closeModel() {
    $('#myModal').modal('hide');
}
//Function for clearing the textboxes  
function clearTextBox() {
    $('#myModal').modal('show');
    $('#Id').val("");
    $('#Line1').val("");
    $('#Line2').val("");
    $('#City').val("");
    $('#PostCode').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Id').css('border-color', 'lightgrey');
    $('#Line1').css('border-color', 'lightgrey');
    $('#Line2').css('border-color', 'lightgrey');
    $('#City').css('border-color', 'lightgrey');
    $('#PostCode').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#Id').val() == "") {
        $('#Id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Line1').css('border-color', 'lightgrey');
    }
    if ($('#Line1').val() == "") {
        $('#Line1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Line1').css('border-color', 'lightgrey');
    }
    if ($('#City').val() == "") {
        $('#City').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#City').css('border-color', 'lightgrey');
    }
    if ($('#PostCode').val() == "") {
        $('#PostCode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PostCode').css('border-color', 'lightgrey');
    }
    return isValid;
}