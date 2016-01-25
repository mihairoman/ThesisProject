var map;
var generateColors;
var currentRegion = {};

$(document).ready(function () {
    $('body').fadeIn(2000);
    initMap();
    $('.jvectormap-zoomin').remove();
    $('.jvectormap-zoomout').remove();

    var infoBox = document.getElementById("infoBox");
});

function initMap() {
    //['#088A85', '#084B8A', '#0B614B', '#0B615E', '#0489B1', '#0B6138', '#0174DF', '#0B3B39', '#045FB4','#086A87']
    var palette = ['#A1BE95', '#E2DFA2', '#92AAC7', '#ED5752', '#FA6E59', '#4897D8', '#D0E1F9', '#4D648D', '#283655',
                    '#B9D9C3', '#EB5E30', '#31A9B8', '#258039', '#1E656D', '#C6D166', '#34888C', '#FA6775', '#ACD0C0',
                    '#75B1A9', '#D9B44A', '#063852', '#F0810F', '#EC96A4', '#1995AD'];
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
        zoomMax: 3,
        zoomAnimate: true,
        zoomOnScrollSpeed: 1,
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
            stroke: 'solid'
        },
        selected: {
            fill: 'yellow'
        },
        selectedHover: {
            fill: 'red'
        },
        onRegionClick: function (event, code) {
            var regionName = map.getRegionName(code);
            GetRegionDescription(regionName);
        },

    });
    map.series.regions[0].setValues(generateColors());
};

/****************** Ajax requests functions **********************/

function GetRegionDescription(regionName) {
    if (localStorage.getItem(regionName) !== null) {
        updateCurrentRegion(getStorageItem(regionName));
        setInfoBoxMainData(getStorageItem(regionName));
    }
    else {
        var targeturl = '/Map/GetQueryResult?resource=' + regionName + "&" + "queryType=region";
        $.ajax({
            type: "GET",
            url: targeturl,
            //data: targeturl,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            dataType: "json",
            success: function (response) {
                if (response.results.bindings.length <= 0) {
                    handleError();
                }
                else {
                    initCurrentRegion(response);
                    saveStorageItem(currentRegion.name, currentRegion);
                    setInfoBoxMainData(currentRegion);
                }
            },
            error: function (e) {
                handleError();
            }
        });
    }
};

function GetListOfItems(regionName, queryType, callBackFn) {
    var url = "/Map/GetQueryResult?resource=" + regionName + "&" + "queryType=" + queryType;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            callBackFn(response);
        },
        error: function (e) {
            handleError();
        }
    });
}

function getResource(resource, type) {
    var url = "/Map/GetQueryResult?resource=";
}

/****************** End of Ajax requests functions **********************/

/******************* Current region obj init/update functions ********/

function initCurrentRegion(requestResult) {
    var result = requestResult.results.bindings[0];
    currentRegion.name = result.name.value;
    currentRegion.description = result.description.value;
    currentRegion.regionImg = result.imgLink.value;
    currentRegion.organisations = [];
}

function updateCurrentRegion(newRegion) {
    currentRegion.name = newRegion.name;
    currentRegion.description = newRegion.description;
    currentRegion.regionImg = newRegion.regionImg;
    currentRegion.organisations = newRegion.organisations;
    currentRegion.customName = newRegion.customName;
}


/******************* end of current region obj init/update functions ********/


/**************** Footer buttons functions ************************/
$('#home').on('click', function (event) {
    setInfoBoxMainData(currentRegion);
});

$('#organisations').on('click', function (event) {
    if ((currentRegion.organisations === undefined) || (currentRegion.organisations.length === 0)) {
        GetListOfItems(currentRegion.name, "organisations", updateOrganisations);
    } else {
        updateInfoBoxContent((getStorageItem(currentRegion.name)).organisations, "organisations | businesses");
    }
});

function updateOrganisations(response) {
    currentRegion.organisations = response.results.bindings;
    currentRegion.customName = response.head.vars[0];
    updateStorageItem(currentRegion.name, currentRegion);
    updateInfoBoxContent(currentRegion.organisations, "organisations | businesses");
}

$('#persons').on('click', function (event) {
    if (currentRegion.organisations.length <= 0) {

    }
});

$('#places').on('click', function (event) {
    if (currentRegion.organisations.length <= 0) {

    }
});
/**************** End of footer buttons functions ************************/

/*************** Functions for updating the info box data ****************/

function setInfoBoxMainData(newRegion) {
    $('#title').text(newRegion.name);
    $('#contentImg').prop('src', newRegion.regionImg).css('display', 'inline-block');
    $('#contentDescription').text(newRegion.description);
    $(infoBox).find('.list-container').hide();
    $('#infoBoxBody').show();
}

function updateInfoBoxContent(data, title) {
    var mainList = $('.items-list');
    mainList.empty();
    $('#title').text(currentRegion.name + " - " + title);
    for (var i = 0; i < data.length; i++) {
        var listItem = $('<li class="data-list-item">' + data[i][currentRegion.customName].value + '</li>');
        mainList.append(listItem);
    }
    $('#infoBoxBody').hide();
    $('.list-container').show();
}

function clearInfoBox() {
    $(infoBox).find('#title').text('');
    $(infoBox).find('#contentImg').css('display', 'none');
    $(infoBox).find('#contentDescription').text('');
}

/************** end of functions which update info box data *****************/

function handleError() {
    $('#errorModal').modal('toggle');
}


//function triggerAditionalDialog() {
//    //var leftPosition = $('#myModal .modal-content').offset().left + $('#myModal .modal-content').width();
//    //$('#aditionalModal').css('left', leftPosition);
//    $('#aditionalModal').toggle('slide', { direction: 'left' }, 500).modal('toggle');
//    //$('#myModal').toggleClass('modal-changed-pos',500);
//}

//$('#myModal').on('close', function () {
//    alert("sdasd");
//});

//$('#btnMoreOptions').on('click', function () {
//    var leftPosition = $('#myModal .modal-content').offset().left + $('#myModal .modal-content').width();
//    $('#aditionalModal').css('left', leftPosition);
//    $('#aditionalModal').toggle('slide', { direction: 'left' }, 500).modal('toggle');
//});

/***** Start of local storage functions ***/
function isLocalStorageAvailable() {
    try {
        return 'localStorage' in window && window['localStorage'] !== null;
    } catch (e) {
        return false;
    }
}

function saveStorageItem(key, value) {
    try {
        if (localStorage.getItem(key) === null) {
            localStorage.setItem(key, JSON.stringify(value));
        }
    } catch (e) {
        if (e.name.eq("QuotaExceededError")) {
            clearLocalStorage();
        } else {
            alert("Not saved!");
        }
    }
}

function updateStorageItem(key, value) {
    try {
        localStorage.setItem(key, JSON.stringify(value));
    }
    catch (e) {
        if (e.name.eq("QuotaExceededError")) {
            clearLocalStorage();
        } else {
            alert("Not saved!");
        }
    }
}

function getStorageItem(key) {
    return JSON.parse(localStorage.getItem(key));
}

function removeStorageItem(key) {
    localStorage.removeItem(key);
}

function clearLocalStorage() {
    localStorage.clear();
}

/* end of local storage functions */