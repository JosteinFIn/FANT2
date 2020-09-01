if (
    "mediaDevices" in navigator &&
    "getUserMedia" in navigator.mediaDevices
  ) {

var imageCapture;
const video = document.querySelector("#my_camera");




var Camera = {
    
    
    attach: function(id){
      
        const constraints = {
            video: {
                width: {
                  min: 100,
                  ideal: 350,
                  max: 400,
                },
                facingMode: "environment",
                // height: {
                //   min: 720,
                //   ideal: 1080,
                //   max: 1440,
                // },
              },
          };

          try {
                const player = document.getElementById(id);
                navigator.mediaDevices.getUserMedia(constraints)
                    .then((videoStream) => {
                      player.srcObject = videoStream;
                      const track = videoStream.getVideoTracks()[0];
                      imageCapture = new ImageCapture(track);
                    })
            }
          
          catch (error) 
          {
              throw alert(error, "No inteface found");
          }
          
      },

      capture: function(){

        // var imageCapture;
        // const takePhotoButton = document.querySelector(buttonId);
        // takePhotoButton.onclick = takePhoto;
        var reader = new FileReader();
        // takePhoto();

        // function takePhoto() {
          video.play();
          imageCapture.takePhoto()
            .then(function(blob) {
              console.log('Took photo:', blob);
              video.pause()
              // .then(reader.readAsDataURL(blob))

            reader.readAsDataURL(blob); 
            reader.onloadend = function() {
              var base64data = reader.result;                
            
              document.getElementById('Image').value = base64data;
              }
           
            console.log('takePhoto() error: ', error);
          });
        // }
      },
      playVideo: function() {
        video.play();
      }
    }
}
