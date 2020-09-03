(function () {
    markers = [];
    map = {};

    initMap();
})();

function addMarker() {
    console.log(globalStuff)

    globalStuff.forEach(function (element) {
      markers.push(new google.maps.Marker({
          position: { lat: parseFloat(element.Lat), lng: parseFloat(element.Lng) },
        // label: labels[labelIndex++ % labels.length],
        // title: google.maps.getPlaces,
        map: map
      }))
    })
}

function initMap() {
    var mapElement = document.getElementById("mapContainer");
    mapElement.style.display = "block";
    //Set the Latitude and Longitude of the Map  
    var myAddress = new google.maps.LatLng(59.9139, 10.7522);  
    

    //Create Options or set different Characteristics of Google Map  
    var mapOptions = {  
        center: myAddress,  
        zoom: 15,  
        // minZoom: 30,  
        mapTypeId: google.maps.MapTypeId.ROADMAP  
    }; 

    var marker = new google.maps.Marker();
    //Display the Google map in the div control with the defined Options  
    map = new google.maps.Map(document.getElementById("map"), mapOptions);  

    addMarker();

   }