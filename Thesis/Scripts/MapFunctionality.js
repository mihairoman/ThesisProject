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
        triggerLoadingScreen();
        var targeturl = '/Map/GetQueryResult?resource=' + regionName + "&" + "queryType=region";
        $.ajax({
            type: "GET",
            url: targeturl,
            //data: targeturl,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            dataType: "json",
            success: function (response) {
                if (response.results.bindings.length <= 0) {
                    handleError("No data could be found.");
                }
                else {
                    initCurrentRegion(response);
                    saveStorageItem(currentRegion.name, currentRegion);
                    setInfoBoxMainData(currentRegion);
                }
                $(".overlay").remove();
            },
            error: function (e) {
                handleError("No data could be found.");
            }
        });
    }
};

function GetListOfItems(regionName, queryType, callBackFn) {
    triggerLoadingScreen();
    var url = "/Map/GetQueryResult?resource=" + regionName + "&" + "queryType=" + queryType;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            callBackFn(response);
            $(".overlay").remove();
        },
        error: function (e) {
            handleError("Data could not be found or transaction took too long.");
        }
    });
}

function getResource(resource, type) {
    triggerLoadingScreen();
    var url = "/Map/GetQueryResult?resource=" + resource + "&" + "queryType=" + type;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            triggerDataModal(response);
            $(".overlay").remove();
        },
        error: function (e) {
            handleError("No data could be found.");
        }
    });
}

/****************** End of Ajax requests functions **********************/

/******************* Current region obj init/update functions ********/

function initCurrentRegion(requestResult) {
    var result = requestResult.results.bindings[0];
    currentRegion.name = result.name.value;
    currentRegion.description = result.description.value;
    currentRegion.regionImg = result.imgLink.value;
    currentRegion.organisations = [];
    currentRegion.persons = [];
    currentRegion.places = [];
    currentRegion.events = [];
}

function updateCurrentRegion(newRegion) {
    currentRegion.name = newRegion.name;
    currentRegion.description = newRegion.description;
    currentRegion.regionImg = newRegion.regionImg;
    currentRegion.organisations = newRegion.organisations;
    currentRegion.customName = newRegion.name.formatCustomName();
    currentRegion.persons = newRegion.persons;
    currentRegion.places = newRegion.places;
    currentRegion.events = newRegion.events;
}


/******************* end of current region obj init/update functions ********/


/**************** Footer buttons functions ************************/
function selectedRegionExists() {
    if (currentRegion.name === undefined) {
        handleError("Please select a region from the map first");
        return false;
    }
    return true;
};

$('#home').on('click', function (event) {
    if (selectedRegionExists()) {
        setInfoBoxMainData(currentRegion);
    }
});

$('#organisations').on('click', function (event) {
    if (selectedRegionExists()) {
        if ((currentRegion.organisations === undefined) || (currentRegion.organisations.length === 0)) {
            GetListOfItems(currentRegion.name, "organisations", updateOrganisations);
        } else {
            updateInfoBoxContent(currentRegion.organisations, "organisations");
        }
    }
});

$(".items-list").on('click', "li.data-list-item.organisations", function (event) {
    event.preventDefault();
    if (selectedRegionExists()) {
        getResource($(this).attr('data-resource'), "organisations_single");
    }
});

function updateOrganisations(response) {
    currentRegion.organisations = response.results.bindings;
    currentRegion.customName = currentRegion.name.formatCustomName();
    updateStorageItem(currentRegion.name, currentRegion);
    updateInfoBoxContent(currentRegion.organisations, "organisations");
}

$('#persons').on('click', function (event) {
    if (selectedRegionExists()) {
        if (currentRegion.persons === undefined || currentRegion.persons.length <= 0) {
            GetListOfItems(currentRegion.name, "persons", updatePersons);
        }
        else {
            updateInfoBoxContent(currentRegion.persons, "persons");
        }
    }
});

$(".items-list").on('click', "li.data-list-item.persons", function (event) {
    event.preventDefault();
    if (selectedRegionExists()) {
        getResource($(this).attr('data-resource'), "persons_single");
    }
});

function updatePersons(response) {
    currentRegion.persons = response.results.bindings;
    updateStorageItem(currentRegion.name, currentRegion);
    updateInfoBoxContent(currentRegion.persons, "persons");
}

$('#places').on('click', function (event) {
    if (selectedRegionExists()) {
        if (currentRegion.places === undefined || currentRegion.places.length <= 0) {
            GetListOfItems(currentRegion.name, "locations", updatePlaces);
        }
        else {
            updateInfoBoxContent(currentRegion.places, "places");
        }
    }
});

function updatePlaces(response) {
    currentRegion.places = response.results.bindings;
    updateStorageItem(currentRegion.name, currentRegion);
    updateInfoBoxContent(currentRegion.places, "places");
}

$(".items-list").on('click', "li.data-list-item.places", function (event) {
    event.preventDefault();
    if (selectedRegionExists()) {
        getResource($(this).attr('data-resource'), "persons_single");
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
        var listItem = $('<li class="data-list-item ' + title + '" data-resource="' + data[i]['resource'].value + '">'
                       + (data[i][currentRegion.customName] === undefined ? data[i]['name'].value : data[i][currentRegion.customName].value) + '</li>');
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

function handleError(message) {
    $('#errorModal .modal-body').text(message);
    $('#errorModal').modal('toggle');
    $(".overlay").remove();
}

function triggerDataModal(response) {
    var resultObj = response.results.bindings[0];
    if (resultObj === undefined) {
        handleError("No data could be found.");
        return false;
    }
    var modalContent = $('.modal-body>.main-body-content>#contentDescription');
    modalContent.empty();
    for (var prop in resultObj) {
        var valueToBeAdded = resultObj[prop].value ? resultObj[prop].value : "N\\A";
        var link = null;
        var paragraph = $('<p>' + '<b>' + (prop === "img" ? "" : prop.capitalizeFirstLetter().formatProperty() + ":") + ' </b></p>');
        if (prop == 'website' || prop == 'wikipedia_article') {
            $('<a href="' + valueToBeAdded + '" onclick="window.open(this.href); return false;">' + valueToBeAdded + '</a>').insertAfter($(paragraph).find('b'));
        }
        else if (prop == 'img') {
            $('<img style="margin-top:10px;" src="' + valueToBeAdded + '"</img>').insertAfter($(paragraph).find('b'));
        }
        else {
            $('<span>' + valueToBeAdded + '</span>').insertAfter($(paragraph).find('b'));
        }
        modalContent.append(paragraph);
    }
    $('#myModal').modal('toggle');
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
        if (e.name == ("QuotaExceededError")) {
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

/**************** Misc functions **************************/

String.prototype.capitalizeFirstLetter = function () {
    return this.charAt(0).toUpperCase() + this.slice(1);
}

String.prototype.formatProperty = function () {
    return this.replace("_", " ");
}

String.prototype.formatCustomName = function () {
    return this.replace(" ", "_");
}

function triggerLoadingScreen() {
    var body = $("body");
    var overlayDiv = document.createElement('div');
    var spinner = $('<i class="fa fa-spinner fa-pulse fa-3x" style="color:white; position:absolute; top:48%;"></i>');
    $(overlayDiv).append(spinner);
    $(overlayDiv).addClass("overlay").appendTo(body);
}