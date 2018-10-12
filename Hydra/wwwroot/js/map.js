let map, dataLayer, infobox, searchManager;
const distanceUnits = 'km',
    dataSourceUrl = 'https://spatial.virtualearth.net/REST/v1/data/515d38d4d4e348d9a61c615f59704174/CoffeeShops/CoffeeShop';

function getMap() {
    map = new Microsoft.Maps.Map('#store-locations-map', {
        zoom: 18,
        center: new Microsoft.Maps.Location(32.078863, 34.794393)
    });
    dataLayer = new Microsoft.Maps.Layer();
    Microsoft.Maps.Events.addHandler(dataLayer, 'click', (e => displayInfobox(e.primitive)));
    map.layers.insert(dataLayer);
    infobox = new Microsoft.Maps.Infobox(new Microsoft.Maps.Location(0, 0), {
        visible: false,
        offset: new Microsoft.Maps.Point(0, 20),
        height: 170,
        width: 230
    });
    infobox.setMap(map);
    Microsoft.Maps.loadModule(['Microsoft.Maps.Search', 'Microsoft.Maps.SpatialDataService', 
        'Microsoft.Maps.SpatialMath'], () => searchManager = new Microsoft.Maps.Search.SearchManager(map));
    $('#search-btn').click(performSearch);
    $('#searchBox').keypress((e) => {
        if (e.which == 13) {
            performSearch();
        }
    });
}

function performSearch() {
    clearMap();
    const geocodeRequest = {
        where: $('#search-box')[0].value,
        count: 1,
        callback: (r) => {
            if (!r || !r.results ||
                r.results.length === 0 ||
                !r.results[0].location) {
                    showErrorMsg('Unable to geocode query');
                    return;
            }

            findNearbyLocations(r.results[0].location);
        },
        errorCallback: () => {
            showErrorMsg('Unable to geocode query');
        }
    };

    searchManager.geocode(geocodeRequest);
}

function showErrorMsg(msg) {
    const html = '<span class="error-msg">' + msg + '</span>';
    $('#results-panel').html(html);
}

function clearMap() {
    dataLayer.clear();
    infobox.setOptions({ visible: false });
    $('#results-panel').html('');
}

function findNearbyLocations(location) {
    const queryOptions = {
        queryUrl: dataSourceUrl,
        spatialFilter: {
            spatialFilterType: 'nearby',
            location: location,
            radius: 20
        },
        top: 10
    };

    Microsoft.Maps.SpatialDataService.QueryAPIManager.search(queryOptions, map, (results) => {
        const locs = [], listItems = [];

        results.forEach((res, i) => {
            res.setOptions({
                icon: '../images/red_pin.png',
                text: (i + 1) + ''
            });
            locs.push(res.getLocation());
            listItems.push('<table class="listItem"><tr><td rowspan="3"><span>', (i + 1), '.</span></td>');
            const metadata = results[i].metadata;
            listItems.push('<td><a class="title" href="javascript:void(0);" rel="', metadata.ID, '">', metadata.Name, '</a></td>');
            listItems.push('<td>', convertSdsDistance(metadata.__Distance), ' ', distanceUnits, '</td></tr>');
            listItems.push('<tr><td colspan="2" class="listItem-address">', metadata.AddressLine, '<br/>', metadata.Locality, ', ');
            listItems.push(metadata.AdminDistrict, '<br/>', metadata.PostalCode, '</td></tr>');
            listItems.push('<tr><td colspan="2"><a target="_blank" href="http://bing.com/maps/default.aspx?rtp=~pos.', 
                metadata.Latitude, '_', metadata.Longitude, '_', encodeURIComponent(metadata.Name), '">Directions</a></td></tr>');
            listItems.push('</table>');
        });

        dataLayer.add(results);
        if (locs.length > 1)
            map.setView({ bounds: Microsoft.Maps.LocationRect.fromLocations(locs), padding: 80 });
        else 
            map.setView({ center: locs[0], zoom: 15 });
        
        const resultsPanel = $('#results-panel');
        resultsPanel.html(listItems.join(''));
        const resultItems = resultsPanel.find('.title').each((i, element) => 
            $(element).click(resultClicked)
        );
    });
}

function resultClicked(e) {
    const id = e.target.getAttribute('rel');
    const pins = dataLayer.getPrimitives();
    for (var i = 0; i < pins.length; i++) {
        var pin = pins[i];
        if (pin.metadata.ID !== id) {
            pin = null;
        }
        else {
            break;
        }
    }

    if (!pin) 
        return;

    map.setView({ center: pin.getLocation(), zoom: 17 });
    displayInfobox(pin);
}

function displayInfobox(pin) {
    const metadata = pin.metadata;
    const desc = [
        '<table>',
        '<tr><td colspan="2">', metadata.AddressLine, ', ', metadata.Locality, ', ',
        metadata.AdminDistrict, ', ', metadata.PostalCode, '</td></tr>',
        '<tr><td><b>Hours:</b></td><td>', formatTime(metadata.Open), ' - ', formatTime(metadata.Close), '</td></tr>',
        '<tr><td><b>Store Type:</b></td><td>', metadata.StoreType, '</td></tr>',
        '<tr><td><b>Has Wifi:</b></td><td>', (metadata.IsWiFiHotSpot) ? 'Yes' : 'No', '</td></tr>',
        '<tr><td colspan="2"><a target="_blank" href="http://bing.com/maps/default.aspx?rtp=~pos.', metadata.Latitude, '_', metadata.Longitude, '_', encodeURIComponent(metadata.Name), '">Directions</a></td></tr>',
        '</table>'];
    infobox.setOptions({ visible: true, location: pin.getLocation(), title: metadata.Name, description: desc.join('') });
}

function formatTime(val) {
    let minutes = val % 100;
    const hours = Math.round(val / 100);
    if (minutes == 0) 
        minutes = '00';

    return hours > 12 ?
        (hours - 12) + ':' + minutes + 'PM' :
        hours + ':' + minutes + 'AM';
}

function convertSdsDistance(distance) {
    let toUnits;
    switch (distanceUnits.toLowerCase()) {
        case 'mi':
        case 'miles':
            toUnits = Microsoft.Maps.SpatialMath.DistanceUnits.Miles;
            break;
        case 'km':
        case 'kilometers':
        default:
            toUnits = Microsoft.Maps.SpatialMath.DistanceUnits.Kilometers;
            break;
    }
    let d = Microsoft.Maps.SpatialMath.convertDistance(distance, Microsoft.Maps.SpatialMath.DistanceUnits.Kilometers, toUnits);
    d = Math.round(d * 100) / 100;
    return d;
}