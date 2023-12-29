var calendar;
var Calendar = FullCalendar.Calendar;
var events = [];

$(function () {
    if (!!scheds) {
        Object.keys(scheds).map(k => {
            var row = scheds[k];
            events.push({
                id: row.id,
                title: row.title,
                start: row.next_date,
                end: row.next_date
            });
        });
    }

    calendar = new Calendar(document.getElementById('calendar'), {
        headerToolbar: {
            left: 'prev,next today',
            right: 'dayGridMonth,dayGridWeek,list',
            center: 'title',
        },
        selectable: true,
        themeSystem: 'bootstrap',
        events: events,
        eventContent: function (info) {
            var now = new Date();
            var eventDate = new Date(info.event.start);
            if (eventDate >= now) {
                info.backgroundColor = '#6571ff';
            } else {
                info.backgroundColor = '#ff3366';
            }
        },
        eventClick: function (info) {
            info.jsEvent.preventDefault();
            /*var eventDate = new Date(info.event.start);
            var month = eventDate.getMonth() + 1;
            var date = eventDate.getFullYear() + '-' + month + '-' + eventDate.getDate();*/

            info.jsEvent.preventDefault();
            var eventDate = new Date(info.event.start);
            var month = eventDate.getMonth() + 1;
            month = month <= 9 ? "0" + month : month;
            var day = eventDate.getDate() <= 9 ? "0" + eventDate.getDate() : eventDate.getDate();
            var date = eventDate.getFullYear() + '-' + month + '-' + day;

            var url = 'cases?start_date=' + date + '&end_date=' + date;
            window.open(url);
        },
        editable: true
    });

    calendar.render();

    // Function to get the count of events for a specific date
    function getCountForDate(date) {
        var count = 0;
        events.forEach(function (event) {
            var eventDate = event.start.split('T')[0];
            if (eventDate === date) {
                count++;
            }
        });
        return count;
    }
});