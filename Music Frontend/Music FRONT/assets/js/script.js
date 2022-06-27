var modal = document.getElementById("myModal");

var btn = document.getElementById("myBtn");

var span = document.getElementsByClassName("close")[0];

// var blurr = document.getElementById("blurr");

function modal2(){
    modal.style.display = "block";
}

// function MyBlur(){
//     blurr.style.border = "3px solid #31EB13"
// }

// btn.onclick = function() {
//   modal.style.display = "block";
// }

span.onclick = function() {
  modal.style.display = "none";
}

window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}