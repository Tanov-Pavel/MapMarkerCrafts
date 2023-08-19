var map = undefined;
var drawnItems = new L.FeatureGroup();
var drawControl;


window.onload = (event) => {
    map = L.map('map').setView([41.505, 42.09], 5);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 7,
        attribution: '© Test App'
    }).addTo(map);
}
function addPoint() {
    var markers = [];
    map.on('click', function (event) {
        var lat = event.latlng.lat;
        var lon = event.latlng.lng;
        console.log("Координаты маркера: " + lat + ", " + lon);
        var marker = L.marker([lat, lon], { draggable: true }).addTo(map);
        markers.push(marker);
        marker.on('', function (event) {
            var updatedMarker = event.target;
            var updatedLat = updatedMarker.getLatLng().lat;
            var updatedLon = updatedMarker.getLatLng().lng;
            console.log("Обновленные координаты маркера: " + updatedLat + ", " + updatedLon);
        });
    });

}
function addLine() {
    map.addLayer(drawnItems);
    var drawControl = new L.Control.Draw({
        draw: {
            marker: false,
            polyline: true,
            polygon: false,
            circle: false,
            rectangle: false,
        },
        edit: {
            featureGroup: drawnItems,
        },
    });
    map.addControl(drawControl);
    map.on('draw:created', function (event) {
        var type = event.layerType,
            layer = event.layer;
        if (type === 'polyline') {
            drawnItems.addLayer(layer);
            var latLngs = layer.getLatLngs();
            var coordinates = [];
            for (var i = 0; i < latLngs.length; i++) {
                coordinates.push([latLngs[i].lat, latLngs[i].lng]);
            }
            console.log("Добавлена линия с координатами: ", JSON.stringify(coordinates));
        }
    });
}
function addRectangle() {
    map.addLayer(drawnItems);
    var drawControl = new L.Control.Draw({
        edit: {
            featureGroup: drawnItems
        },
        draw: {
            polygon: false,
            polyline: false,
            circle: false,
            rectangle: true,
            marker: false
        }
    });
    map.addControl(drawControl);
    map.on('draw:created', function (event) {
        var type = event.layerType,
            layer = event.layer;
        if (type === 'rectangle') {
            drawnItems.addLayer(layer);
            var bounds = layer.getBounds();
            var height = bounds.getNorthEast().distanceTo(bounds.getSouthEast());
            var width = bounds.getNorthEast().distanceTo(bounds.getNorthWest());
            var area = height * width;

            layer.bindTooltip("Площадь: " + area.toFixed(2) + " кв. м").openTooltip();

            console.log("Добавлен прямоугольник с высотой:", height.toFixed(2), "м, шириной:", width.toFixed(2), "м и площадью:", area.toFixed(2), "кв. м");
        }
    });
}
function addPolygon() {
    drawnItems.clearLayers();
    drawControl = new L.Control.Draw({
        draw: {
            polygon: true,
            marker: false,
            polyline: false,
            circle: false,
            rectangle: false
        },
        edit: {
            featureGroup: drawnItems,
            remove: true
        }
    });
    map.addControl(drawControl);
    var polygonDrawer = new L.Draw.Polygon(map, drawControl.options.polygon);
    polygonDrawer.enable();

    map.on('draw:created', function (event) {
        var type = event.layerType,
            layer = event.layer;
        if (type === 'polygon') {
            drawnItems.addLayer(layer);
            var area = L.GeometryUtil.geodesicArea(layer.getLatLngs()[0]);
            layer.bindTooltip("Площадь: " + area.toFixed(2) + " кв. м").openTooltip();

            console.log("Добавлен полигон с площадью:", area.toFixed(2), "кв. м");
        }
    });
}
function saveCoordinates() {
    map.off('click');
    map.removeControl(drawControl);
    drawnItems.clearLayers();
    addLine();
}
function showMenu() {
    const popup = document.getElementById("popup");
    const button = document.getElementById("main-button");

    if (popup.style.height === "0vh") {
        popup.style.height = "18vh";
        popup.style.display = "block"
        button.innerText = "Скрыть";
    } else {
        popup.style.height = "0vh";
        button.innerText = "Нарисовать";
    }
}
function addMapPopup(mapObj, latlng) {
    var popup = L.popup()
        .setLatLng(latlng)
        .setContent('<div>This is Popup</div>')
        .openOn(map);
    mapObj.bindPopup(popup);
}
