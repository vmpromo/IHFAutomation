var timerID = null;
var timerRunning = false;

function stopclock() {
    if (timerRunning)
        clearTimeout(timerID);
    timerRunning = false;
}

function showtime() {
    var now = new Date();
    var hours = now.getHours();
    var minutes = now.getMinutes();
    var seconds = now.getSeconds()
    var timeValue = "" + hours;
    timeValue += ((minutes < 10) ? ":0" : ":") + minutes
    timeValue += ((seconds < 10) ? ":0" : ":") + seconds
    document.getElementById("time").innerHTML = timeValue;
    timerID = setTimeout("showtime()", 1000);
    timerRunning = true;
}

function showdate() {
    var todaydate = new Date();
    document.getElementById("date").innerHTML = todaydate.toLocaleDateString();
}

function startclock() {
    showdate();
    stopclock();
    showtime();
}
