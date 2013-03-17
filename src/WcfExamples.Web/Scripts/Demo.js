$(function() {

    $('#btnLoadHtml').on('click', loadPersonHtml);
    $('#btnLoadPersonData').on('click', loadPersonData);
    $('#btnLoadFromWcfService').on('click', loadFromWcfController);

});

function loadPersonHtml() {
    $.ajax({
        url: '/Demo/GetPersonDisplayHtml',
        type: 'GET',
        cache: false,
        success: function(data) {
            $("#personDetails").html(data);
        },
        error: function(jxhr) { // error callback is called if something goes wrong
            if (typeof(console) != 'undefined') {
                console.log(jxhr.status);
                console.log(jxhr.responseText);
            }
        }
    });
}

function loadPersonData() {
    $.ajax({
        url: '/Demo/GetPersonData',
        type: 'POST',
        cache: false,
        success: function (data) {
            $("#personDetails #txtPersonName").val(data.Name);
            $("#personDetails #txtPersonAge").val(data.Age);
        },
        error: function (jxhr) { // error callback is called if something goes wrong
            if (typeof (console) != 'undefined') {
                console.log(jxhr.status);
                console.log(jxhr.responseText);
            }
        }
    });
}

function loadFromWcfController() {
    $.ajax({
        url: '/Wcf/GetDataFromWcfService',
        type: 'POST',
        cache: false,
        success: function(data) {
            alert('loaded data: ' + JSON.stringify(data));
        },
        error: function(jxhr) { // error callback is called if something goes wrong
            if (typeof(console) != 'undefined') {
                console.log(jxhr.status);
                console.log(jxhr.responseText);
            }
        }
    });
}