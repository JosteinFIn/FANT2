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
             
              var canvas = document.createElement('canvas'),
                ctx = canvas.getContext('2d');

              // set its dimension to target size
              canvas.width = 500;
              canvas.height = 500;

              // draw source image into the off-screen canvas:
              ctx.drawImage(base64data, 0, 0, 500, 500);
              var downScale = canvas.toDataURL(img);
              console.log(downScale);
              document.getElementById('Image').value = downScale;
              }
            // img.classList.remove('hidden');
            // img.src = URL.createObjectURL(blob);
          }).catch(function(error) {
            console.log('takePhoto() error: ', error);
          });
        // }
      },
      playVideo: function() {
        video.play();
      }
    }
}
