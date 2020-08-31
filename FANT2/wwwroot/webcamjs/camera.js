if (
    "mediaDevices" in navigator &&
    "getUserMedia" in navigator.mediaDevices
  ) {

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
                    .then((videoStream) => {player.srcObject = videoStream});
            }
          
          catch (error) 
          {
              throw alert(error, "No inteface found");
          }
          
      }
    }
}
