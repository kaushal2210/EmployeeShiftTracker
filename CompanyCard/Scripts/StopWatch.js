var startTime, differenceTime = 0, workHours = 0, workMin = 0, workSec = 0;
var timeStart = false;

var endFunction = function () {
    if (timeStart) {
        clearInterval();
        document.getElementById("Start").style.display = "block";
        document.getElementById("End").style.display = "none";
        timeStart = false;
        differenceTime = 0;
        workHours = 0;
        workMin = 0;
        workSec = 0;
        document.getElementById("timeContainer").style.display = "none";
        document.getElementById("timeLabel").innerHTML = "";
    }
}    

var startFunction = function () {
    if (!timeStart) {
        document.getElementById("Start").style.display = "none";
        document.getElementById("End").style.display = "block";
        document.getElementById("timeContainer").style.display = "block";
        timeStart = true;
        getCurrentTime();
    }
}

var getCurrentTime = setInterval(function () {
    if (timeStart) {
        workSec = workSec + 1;
        if (workSec >= 60) {
            workMin = workMin + 1;
            workSec = 0; 
        }
        if (workMin >= 60) {
            workHours = workHours + 1;
            workMin = 0;
        }
        document.getElementById("timeLabel").innerHTML = "Worked Time = " + workHours + ":" + workMin + ":" + workSec;
    }
}, 1000);