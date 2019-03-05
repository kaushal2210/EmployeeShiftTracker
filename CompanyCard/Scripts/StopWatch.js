var startTime, currentTime, differenceTime;
var timeStart = false;

var endFunction = function () {
    if (timeStart) {
        clearInterval();
        document.getElementById("Start").style.display = "block";
        document.getElementById("End").style.display = "none";
        timeStart = false;
        document.getElementById("timeContainer").style.display = "none";
    }
}    

var startFunction = function () {
    if (!timeStart) {
        startTime = new Date()
        document.getElementById("Start").style.display = "none";
        document.getElementById("End").style.display = "block";
        document.getElementById("timeContainer").style.display = "block";
        timeStart = true;
        getCurrentTime();
    }
}

var getCurrentTime = setInterval(function () {
    if (timeStart) {
        var currentTime = new Date();
        var workHours = currentTime.getHours() - startTime.getHours();
        if (currentTime.getMinutes() <= startTime.getMinutes()) {
            workmin = startTime.getMinutes() - currentTime.getMinutes();
        } else {
            workmin = currentTime.getMinutes() - startTime.getMinutes();
        }
        document.getElementById("timeLabel").innerHTML = "Worked Time = " + workHours + ":" + workmin;
    }
}, 1000);