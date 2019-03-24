$(function () {
    $('#startTimeInput').datetimepicker();
    $('#endTimeInput').datetimepicker({
        useCurrent: false //Important! See issue #1075
    });
    $("#startTimeInput").on("dp.change", function (e) {
        $('#endTimeInput').data("DateTimePicker").minDate(e.date);
    });
    $("#endTimeInput").on("dp.change", function (e) {
        $('#startTimeInput').data("DateTimePicker").maxDate(e.date);
    });
});