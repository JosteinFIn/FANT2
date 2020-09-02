
window.onload = function () {
  initMap();
};

function initMap() {
    console.log('hello');
    window.onload
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
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);  

    addMarker();
    //Set Marker on the Map  
    // var marker = new google.maps.Marker({  
    //     position: myAddress,  
    //     animation: google.maps.Animation.BOUNCE,  
    // });  

    // marker.setMap(map);
    
    //---------------


   }