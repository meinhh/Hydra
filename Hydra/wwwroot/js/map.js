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
    performSearch();
    $('#search-btn').click(performSearch);
    $('#searchBox').keypress((e) => {
        if (e.which == 13) {
            performSearch();
        }
    });
}

function performSearch() {
    $.ajax({
        url: '/About/GetStoresByName?name=' + $('#search-box')[0].value,
        type: 'GET',
    }).done(data => showStores(data))
      .fail(() => showErrorMsg('failed getting data'));
}

function showErrorMsg(msg) {
    const html = '<span class="error-msg">' + msg + '</span>';
    $('#results-panel').html(html);
}

function showStores(stores) {
    if (!stores || stores.length === 0)
        return;

    showStoresOnMap(stores);

    window.navigator.geolocation.getCurrentPosition(userPosition => {
        const itemsPromises = stores.map(async (store, i) => {
            const temp = await $.ajax({ 
                url: `/About/GetTemprature?lot=${store.latitude}&lat=${store.latitude}`, 
                type: 'GET' });
            return [
                `<table class="listItem"><tr>`,
                `<td><span onclick="zoomOnMap(${store.latitude}, ${store.lontitude})" class="title">${store.name}</span></td></tr>`,
                `<tr><td>Distance: ${distance(userPosition.coords.longitude, userPosition.coords.latitude, 
                    store.lontitude, store.latitude)} ${distanceUnits}</td></tr>`,
                `<tr><td>Temprature: ${temp} °C</tr>`,
                `<tr><td>Opening Hours: ${store.openingHour}-${store.closingHour}</tr>`,
                `</table>`,
            ].join('');
        });

        Promise.all(itemsPromises).then(items => {
            const resultsPanel = $('#results-panel');
            resultsPanel.html(items.join(''));
        });
    });
}

function clearMap() {
    map.entities.clear();   
    $('#results-panel').html(''); 
}

function showStoresOnMap(stores) {
    const locations = stores.map(store => {
        const loc = new Microsoft.Maps.Location(store.latitude, store.lontitude);
        const pin = new Microsoft.Maps.Pushpin(loc);
        map.entities.push(pin);

        return loc;
    });

    const bounds = Microsoft.Maps.LocationRect.fromLocations(locations);
    map.setView({bounds:bounds, padding: 100});
}

function zoomOnMap(lat, lon) {
    const loc = new Microsoft.Maps.Location(lat, lon);
    map.setView({ center: loc, zoom: 15 });
}

function toRad(num) {
    return num * Math.PI / 180;
}

function distance(lon1, lat1, lon2, lat2) {
  var R = 6371; // Radius of the earth in km
  var dLat = toRad(lat2-lat1);
  var dLon = toRad(lon2-lon1); 
  var a = Math.sin(dLat/2) * Math.sin(dLat/2) +
          Math.cos(toRad(lat1)) * Math.cos(toRad(lat2)) * 
          Math.sin(dLon/2) * Math.sin(dLon/2); 
  var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a)); 
  var d = R * c; // Distance in km
  return d.toFixed(2);
}