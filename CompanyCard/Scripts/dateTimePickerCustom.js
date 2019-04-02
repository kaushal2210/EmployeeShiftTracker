$(function () {
    $('#date_timepicker_start').datetimepicker({
        onShow: function (ct) {
            this.setOptions({
                maxDate: $('#date_timepicker_end').val() ? $('#date_timepicker_end').val() : false,
                maxTime: $('#date_timepicker_end').val() ? $('#date_timepicker_end').val() : false
            })
        }
    });
    $('#date_timepicker_end').datetimepicker({
        onShow: function (ct) {
            this.setOptions({
                minDate: $('#date_timepicker_start').val() ? $('#date_timepicker_start').val() : false,
                minTime: $('#date_timepicker_start').val() ? $('#date_timepicker_start').val() : false
            })
        }
    });
});