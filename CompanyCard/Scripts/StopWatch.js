var differenceTime = 0, workHours = 0, workMin = 0, workSec = 0,timeStart = false;




var endFunction = function () {
    if (timeStart == true) {
        clearInterval();
        document.getElementById("Start").style.display = "block";
        document.getElementById("End").style.display = "none";
        timeStart = false;
        workHours = 0;
        workMin = 0;
        workSec = 0;
        document.getElementById("timeContainer").style.display = "none";
        document.getElementById("timeLabel").innerHTML = "";
    } else {
        alert("Shift is not started.");
    }
}    

var startFunction = function () {
    if (timeStart == false) {
        document.getElementById("Start").style.display = "none";
        document.getElementById("End").style.display = "block";
        document.getElementById("timeContainer").style.display = "block";
        timeStart = true
        getCurrentTime();
    }else{
        alert("Shift has been already started.");
    }
}

var getCurrentTime = function () {
    setInterval(function () {
        if (timeStart == true) {
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
}