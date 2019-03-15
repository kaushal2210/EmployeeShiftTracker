var differenceTime = 0, workHours = 0, workMin = 0, workSec = 0,timeStart;

window.onload = function () {
    if (localStorage.getItem("timeNotStarted") == false) {
        //alert("In false");
        document.getElementById("Start").style.display = "none";
        document.getElementById("End").style.display = "block";
        document.getElementById("timeContainer").style.display = "block";
        var currentTimer = JSON.parse(window.localStorage.getItem('currentTimer'));
        workHours = currentTimer.workHours;
        workMin = currentTimer.workMin;
        workSec = currentTimer.workSec;
        getCurrentTime();
    } else {
        alert("In Else");
        document.getElementById("Start").style.display = "block";
        document.getElementById("End").style.display = "none";
        localStorage.setItem("timeNotStarted", true);
        document.getElementById("timeContainer").style.display = "none";
    }
}



var endFunction = function () {
    if (localStorage.getItem("timeNotStarted") == false) {
        clearInterval();
        document.getElementById("Start").style.display = "block";
        document.getElementById("End").style.display = "none";
        localStorage.setItem("timeNotStarted", true);
        var currentTimer = {
            'workHours': 0,
            'workMin': 0,
            'workSec': 0
        }
        localStorage.setItem('currentTimer', JSON.stringify(currentTimer));
        document.getElementById("timeContainer").style.display = "none";
        document.getElementById("timeLabel").innerHTML = "";
    } else {
        alert("Shift is not started.");
    }
}    

var startFunction = function () {
    alert("startFunction");
    if (localStorage.getItem("timeNotStarted") == true){
        document.getElementById("Start").style.display = "none";
        document.getElementById("End").style.display = "block";
        document.getElementById("timeContainer").style.display = "block";
        localStorage.setItem("timeNotStarted", false);
        alert("Here is the data");
        getCurrentTime();
    }else{
        alert("Shift has been already started.");
    }
}

var getCurrentTime = function () {
    setInterval(function () {
        if (localStorage.getItem("timeNotStarted") == false) {
            workSec = workSec + 1;
            if (workSec >= 60) {
                workMin = workMin + 1;
                workSec = 0;
            }
            if (workMin >= 60) {
                workHours = workHours + 1;
                workMin = 0;
            }
            var currentTimer = {
                'workHours': workHours,
                'workMin': workMin,
                'workSec': workSec
            }
            localStorage.setItem('currentTimer', JSON.stringify(currentTimer));
            document.getElementById("timeLabel").innerHTML = "Worked Time = " + workHours + ":" + workMin + ":" + workSec;
        }
    }, 1000);
}

window.unload = function () {
    localStorage.clear();
}