if (
    "mediaDevices" in navigator &&
    "getUserMedia" in navigator.mediaDevices
  ) {

var imageCapture;
const video = document.querySelector("#my_camera");
var width, height;

var Camera = {
    
    
    attach: function(id){
      
        const constraints = {
            video: {
                width: {
                  min: 100,
                  ideal: 350,
                  max: 400,
                },
                // advanced: [{width: 100, height: 100}],
                facingMode: "environment",
                // height: {
                //   min: 720,
                //   ideal: 1080,
                //   max: 1440,
                // },
              },
          };

          try {
                // const player = document.getElementById(id);
                navigator.mediaDevices.getUserMedia(constraints)
                    .then((videoStream) => {
                      video.srcObject = videoStream;
                      const track = videoStream.getVideoTracks()[0];
                      imageCapture = new ImageCapture(track);
                    })
            }
          
          catch (error) 
          {
              throw alert(error, "No inteface found");
          }
          
      },

      grabFrame: function(){

        var canvas = document.createElement('canvas')
        imageCapture.grabFrame().then(function(bitmapImage){
          
          console.log('Grabbed frame:', bitmapImage);
          canvas.width = bitmapImage.width;
          canvas.height = bitmapImage.height;
          canvas.getContext('2d').drawImage(bitmapImage, 0, 0);
          // canvas.classList.remove('hidden');
          video.pause();
          var dataUrl = canvas.toDataURL('image/jpeg');
          console.log(dataUrl);
          document.getElementById('Image').value = dataUrl;
        })

        
      },

      capture: function(){

        // var imageCapture;
        // const takePhotoButton = document.querySelector(buttonId);
        // takePhotoButton.onclick = takePhoto;
        var reader = new FileReader();
        // takePhoto();

        // function takePhoto() {
          // video.play();
          imageCapture.takePhoto()
            // .then (video.pause())
            .then(function(blob) {
             
              reader.onload = function (readerEvent) {
                var image = new Image();
                image.src = blob;
                image.onload = function () {
                  
                  console.log(image);
                  // Resize the image
                  var canvas = document.createElement('canvas'),
                      max_size = 544,// TODO : pull max size from a site config
                      width = image.width,
                      height = image.height;
                  if (width > height) {
                      if (width > max_size) {
                          height *= max_size / width;
                          width = max_size;
                      }
                  } else {
                      if (height > max_size) {
                          width *= max_size / height;
                          height = max_size;
                      }
                  }
                  canvas.width = width;
                  canvas.height = height;
                  canvas.getContext('2d').drawImage(image, 0, 0, width, height);
                  var dataUrl = canvas.toDataURL('image/jpeg');
                  console.log(dataUrl);
                  video.pause();
                  document.getElementById('Image').value = dataUrl;
                }
                image.src = readerEvent.target.result;
            }
            reader.readAsDataURL(blob);
          });
      },
      playVideo: function() {
        video.play();
      }
    }
}
