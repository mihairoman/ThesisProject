var map;
var generateColors;
var currentRegion = {};

$(document).ready(function () {
    $('#map').fadeIn('slow');
    initMap();
    $('.jvectormap-zoomin').remove();
    $('.jvectormap-zoomout').remove();
});

function initMap() {
    var palette = ['#088A85', '#084B8A', '#0B614B', '#0B615E', '#0489B1', '#0B6138', '#0174DF', '#0B3B39', '#045FB4','#086A87'];
    generateColors = function () {
        var colors = {},
            key;

        for (key in map.regions) {
            colors[key] = palette[Math.floor(Math.random() * palette.length)];
        }
        return colors;
    },
    map;

    map = new jvm.Map({
        zoomOnScroll: true,
        zoomMax: 2,
        zoomAnimate: true,
        map: 'world_mill_en',
        backgroundColor: "white",
        container: $('#map'),
        series: {
            regions: [{
                attribute: 'fill'
            }]
        },
        initial: {
            fill: 'white',
            "fill-opacity": 1,
            stroke: 'none',
            "stroke-width": 0,
            "stroke-opacity": 1
        },
        hover: {
            "fill-opacity": 0.8,
            cursor: 'pointer',
            fill: 'red',
            stroke : 'solid'
        },
        selected: {
            fill: 'yellow'
        },
        selectedHover: {
            fill : 'red'
        },
        onRegionClick: function (event, code) {
            var regionName = map.getRegionName(code);
            GetRegionDescription(regionName);
        },
    
    });
    map.series.regions[0].setValues(generateColors());
};

function GetRegionDescription(regionName) {
    //var temp = regionName;
    var url = "/Home/GetQueryResult";
    var targeturl = '/Home/GetQueryResult?region=' + regionName;
    $.ajax({
        type: "GET",
        url: targeturl,
        //data: targeturl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.results.bindings.length <= 0) {
                handleError();
            }
            else {
                intiRegionData(response);
                createDialog(currentRegion.name, currentRegion.description, currentRegion.regionImg);
            }
        },
        error: function (e) {
            handleError();
        }
    });
};

function intiRegionData(requestResult) {
    var result = requestResult.results.bindings[0];
    currentRegion.name = result.name.value;
    currentRegion.description = result.description.value;
    currentRegion.regionImg = result.imgLink.value;
    currentRegion.organisations = [];
}

function GetRegionOrganisations(regionName) {
    var url = "/Home/GetOrganizations?region=" + regionName;
    $.ajax({
        type: "GET",
        url: url,
        //data: targeturl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            currentRegion.organisations = response.results.bindings;
            updateDialogContent(currentRegion.organisations);
        },
        error: function (e) {
            handleError();
        }
    });
}

$('#btnOrganisations').on('click', function (event) {
    if (currentRegion.organisations.length <= 0) {
        GetRegionOrganisations(currentRegion.name); 
    }
    else {
    }
});

function createDialog(title, text, imgLink) {
    var myModal = $("#myModal>.modal-dialog>.modal-content");
    myModal.find('.modal-header>.modal-title').text(title);
    myModal.find('.modal-body>.main-body-content>#contentImg').prop('src', imgLink);
    myModal.find('.modal-body>.main-body-content>#contentDescription').text(text);
    myModal.find('.list-data').hide();
    myModal.find('.modal-body>.main-body-content').show();
    $("#myModal").modal('toggle');
}

function handleError() {
    $('#errorModal').modal('toggle');
}

function updateDialogContent(data) {
    var myModal = $("#myModal>.modal-dialog>.modal-content");
    var mainList = myModal.find('.modal-body>.list-data');
    $.each(data, function (index, item) {
        var listItem = $('<li>' + item.rname.value + '</li>');
        mainList.append(listItem);
    });
    //$("#myModal .modal-title").text("Organisations");
    myModal.find('.main-body-content').hide();
    mainList.show();
}

function triggerAditionalDialog()
{
    //var leftPosition = $('#myModal .modal-content').offset().left + $('#myModal .modal-content').width();
    //$('#aditionalModal').css('left', leftPosition);
    $('#aditionalModal').toggle('slide', { direction: 'left' }, 500).modal('toggle');
    //$('#myModal').toggleClass('modal-changed-pos',500);
}

$('#myModal').on('close', function () {
    alert("sdasd");
});

$('#btnMoreOptions').on('click', function () {
    var leftPosition = $('#myModal .modal-content').offset().left + $('#myModal .modal-content').width();
    $('#aditionalModal').css('left', leftPosition);
    $('#aditionalModal').toggle('slide', { direction: 'left' }, 500).modal('toggle');
});