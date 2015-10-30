var map;
var generateColors;

$(document).ready(function () {
    //$('#map').hide
    $('#map').fadeIn('slow');
    initMap();
    $('.jvectormap-zoomin').remove();
    $('.jvectormap-zoomout').remove();
});

function initMap() {
    var palette = ['#088A85', '#084B8A', '#0B614B', '#0B615E', '#0489B1', '#01DFA5', '#0B6138', '#0174DF', '#0B3B39', '#045FB4','#086A87'];
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
        zoomOnScroll: false,
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
            var result = response.results.bindings[0];
            var fn = new createDialog(result.name.value,result.description.value, result.imgLink.value);
        },
        error: function (e) {
            var fn = new createDialog('Error', 'Something went wrong', '');
        }
    });
};

function createDialog(title, text, imgLink) {
    $('#contentImg').prop('src',imgLink);
    $('#contentDescription').text(text);
    $('.modal-title').text(title);
    $("#myModal").modal('toggle');
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