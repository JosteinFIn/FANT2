function DisplayGoogleMap() {  
  
    //Set the Latitude and Longitude of the Map  
    var myAddress = new google.maps.LatLng(59.9139, 10.7522);  

    //Create Options or set different Characteristics of Google Map  
    var mapOptions = {  
        center: myAddress,  
        zoom: 15,  
        minZoom: 30,  
        mapTypeId: google.maps.MapTypeId.ROADMAP  
    };  

    //Display the Google map in the div control with the defined Options  
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);  

    //Set Marker on the Map  
    var marker = new google.maps.Marker({  
        position: myAddress,  
        animation: google.maps.Animation.BOUNCE,  
    });  

    marker.setMap(map);
    
    //---------------

    // Create the search box and link it to the UI element.
  const input = document.getElementById("pac-input");
  const searchBox = new google.maps.places.SearchBox(input);
  map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
  // Bias the SearchBox results towards current map's viewport.
  map.addListener("bounds_changed", () => {
    searchBox.setBounds(map.getBounds());
  });
  let markers = [];
  // Listen for the event fired when the user selects a prediction and retrieve
  // more details for that place.
  searchBox.addListener("places_changed", () => {
    const places = searchBox.getPlaces();

    if (places.length == 0) {
      return;
    }
    // Clear out the old markers.
    markers.forEach(marker => {
      marker.setMap(null);
    });
    markers = [];
    // For each place, get the icon, name and location.
    const bounds = new google.maps.LatLngBounds();
    places.forEach(place => {
      if (!place.geometry) {
        console.log("Returned place contains no geometry");
        return;
      }
      const icon = {
        url: place.icon,
        size: new google.maps.Size(71, 71),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(17, 34),
        scaledSize: new google.maps.Size(25, 25)
      };
      // Create a marker for each place.
      markers.push(
        new google.maps.Marker({
          map,
          icon,
          title: place.name,
          position: place.geometry.location
        })
      );

      if (place.geometry.viewport) {
        // Only geocodes have viewport.
        bounds.union(place.geometry.viewport);
      } else {
        bounds.extend(place.geometry.location);
      }
    });
    map.fitBounds(bounds);
  });

   }