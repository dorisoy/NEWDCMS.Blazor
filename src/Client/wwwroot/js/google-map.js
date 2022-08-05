var resolveCallbacks = [];
var rejectCallbacks = [];

window.BlazorDCMS = {
    loadMaps: function (defaultView, apiKey, resolve, reject) {
        resolveCallbacks.push(resolve);
        rejectCallbacks.push(reject);

        if (defaultView['rz_map_init']) {
            return;
        }

        defaultView['rz_map_init'] = function () {
            for (var i = 0; i < resolveCallbacks.length; i++) {
                resolveCallbacks[i](defaultView.google);
            }
        };

        var document = defaultView.document;
        var script = document.createElement('script');

        script.src =
            'https://maps.googleapis.com/maps/api/js?' +
            (apiKey ? 'key=' + apiKey + '&' : '') +
            'callback=rz_map_init';

        script.async = true;
        script.defer = true;
        script.onerror = function (err) {
            for (var i = 0; i < rejectCallbacks.length; i++) {
                rejectCallbacks[i](err);
            }
        };

        document.body.appendChild(script);
    },
    createMap: function (wrapper, ref, id, apiKey, zoom, center, markers) {
        var api = function () {
            var defaultView = document.defaultView;

            return new Promise(function (resolve, reject) {
                if (defaultView.google && defaultView.google.maps) {
                    return resolve(defaultView.google);
                }

                blazorDCMS.loadMaps(defaultView, apiKey, resolve, reject);
            });
        };

        api().then(function (google) {
            BlazorDCMS[id] = ref;
            BlazorDCMS[id].google = google;

            BlazorDCMS[id].instance = new google.maps.Map(wrapper, {
                center: center,
                zoom: zoom
            });

            BlazorDCMS[id].instance.addListener('click', function (e) {
                BlazorDCMS[id].invokeMethodAsync('Map.OnMapClick', {
                    Position: { Lat: e.latLng.lat(), Lng: e.latLng.lng() }
                });
            });

            blazorDCMS.updateMap(id, zoom, center, markers);
        });
    },
    updateMap: function (id, zoom, center, markers) {
        if (BlazorDCMS[id] && BlazorDCMS[id].instance) {
            if (BlazorDCMS[id].instance.markers && BlazorDCMS[id].instance.markers.length) {
                for (var i = 0; i < BlazorDCMS[id].instance.markers.length; i++) {
                    BlazorDCMS[id].instance.markers[i].setMap(null);
                }
            }

            BlazorDCMS[id].instance.markers = [];

            markers.forEach(function (m) {
                var marker = new this.google.maps.Marker({
                    position: m.position,
                    title: m.title,
                    label: m.label
                });

                marker.addListener('click', function (e) {
                    BlazorDCMS[id].invokeMethodAsync('Map.OnMarkerClick', {
                        Title: marker.title,
                        Label: marker.label,
                        Position: marker.position
                    });
                });

                marker.setMap(BlazorDCMS[id].instance);

                BlazorDCMS[id].instance.markers.push(marker);
            });

            BlazorDCMS[id].instance.setZoom(zoom);

            BlazorDCMS[id].instance.setCenter(center);
        }
    },
    destroyMap: function (id) {
        if (BlazorDCMS[id].instance) {
            delete BlazorDCMS[id].instance;
        }
    },
};