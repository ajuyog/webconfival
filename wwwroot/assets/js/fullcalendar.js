//Full Calendar
document.addEventListener('DOMContentLoaded', function () {
	var containerEl = document.getElementById('external-events');
	new FullCalendar.Draggable(containerEl, {
		itemSelector: '.fc-event',
		eventData: function (eventEl) {
			return {
				title: eventEl.innerText.trim(),
				title: eventEl.innerText,
				className: eventEl.className + ' overflow-hidden '
			}
		}
	});
	var calendarEl = document.getElementById('calendar2');
	var token = $("#json-eventos").val();

	var calendar = new FullCalendar.Calendar(calendarEl, {
		headerToolbar: {
			left: 'prev,next today',
			center: 'title',
			right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
		},

		defaultView: 'month',
		navLinks: true, // can click day/week names to navigate views
		businessHours: true, // display business hours
		editable: true,
		selectable: true,
		selectMirror: true,
		droppable: true, // this allows things to be dropped onto the calendar
		select: async function (arg) {
			//var title = prompt('Event Title:');
			//if (title) {
			//	calendar.addEvent({
			//		title: title,
			//		start: arg.start,
			//		end: arg.end,
			//		allDay: arg.allDay
			//	})
			//}
			//calendar.unselect()

			await EventByDate(arg.startStr);

		},
		eventClick: function (arg) {
			//if (confirm('Are you sure you want to delete this event?')) {
			//	arg.event.remove()
			//}
			console.log(arg);
		},
		editable: true,
		dayMaxEvents: true, // allow "more" link when too many events
		events: '/Graph/GetEventosCalendar?token=' + token
	});
	$("#json-eventos").val(" ");
	calendar.render();
});

async function EventByDate(data) {
	const response = await fetch(`/Graph/GetEventByDateTime?date=${data}`, {
		method: 'GET',
		headers: {
			'Content-Type': 'application/Json'
		}
	});
	const json = await response.json();
	const detalleEventos = new bootstrap.Modal(document.getElementById('modalEvents'));
	$("#body-modalEvents").children().remove();

	$.each(json, function (element, index) {
		$("#body-modalEvents").append('<div class="col-lg-4 col-md-6 col-sm-12" style="margin:auto;">    <div class="card"><div class="card-body"><h5 class="card-title"><i class="fa fa-briefcase" style="color: #0088CC; margin-right: 2px;"></i>   ' + index.subject + '</h5><h6 class="card-subtitle mb-2 text-muted"><i class="fa fa-map-marker" style="color: #0088CC; margin-right: 15px;"></i>' + index.location.displayName + '</h6><p class="card-text"><i class="fa fa-clock-o" style="color: #0088CC; margin-right: 13px;"></i>' + index.start.dateTime + '</p><p class="card-text"><i class="fa fa-clock-o" style="color: #0088CC; margin-right: 13px;"></i>' + index.end.dateTime + '</p><p class="card-text"><i class="fa fa-user" style="color: #0088CC; margin-right: 13px;"></i>' + index.organizer.emailAddress.name + '</p> <div>' + index.body.content + '</div> </div></div></div>');
	})
	$('span[style*="white-space:nowrap"]').remove();
	detalleEventos.show();
	console.log(json);
}


//List FullCalendar
document.addEventListener('DOMContentLoaded', function () {
	var calendarEl = document.getElementById('calendar');
	var calendar = new FullCalendar.Calendar(calendarEl, {
		height: 'auto',
		headerToolbar: {
			left: 'prev,next today',
			center: 'title',
			right: 'listDay,listWeek'
		},

		// customize the button names,
		// otherwise they'd all just say "list"
		views: {
			listDay: { buttonText: 'list day' },
			listWeek: { buttonText: 'list week' }
		},
		initialView: 'listWeek',
		initialDate: '2021-07-12',
		navLinks: true, // can click day/week names to navigate views
		editable: true,
		eventLimit: true, // allow "more" link when too many events
		dayMaxEvents: true, // allow "more" link when too many events
		events: [{
			title: 'All Day Event',
			start: '2021-11-01'
		}, {
			title: 'Long Event',
			start: '2019-11-07',
			end: '2021-10-10'
		}, {
			id: 999,
			title: 'Repeating Event',
			start: '2021-11-09T16:00:00'
		}, {
			id: 999,
			title: 'Repeating Event',
			start: '2021-11-16T16:00:00'
		}, {
			title: 'Conference',
			start: '2019-11-11',
			end: '2021-11-13'
		}, {
			title: 'Meeting',
			start: '2019-11-12T10:30:00',
			end: '2021-11-12T12:30:00'
		}, {
			title: 'Lunch',
			start: '2021-11-12T12:00:00'
		}, {
			title: 'Meeting',
			start: '2021-11-12T14:30:00'
		}, {
			title: 'Happy Hour',
			start: '2021-11-12T17:30:00'
		}, {
			title: 'Dinner',
			start: '2021-11-12T20:00:00'
		}, {
			title: 'Birthday Party',
			start: '2021-11-13T07:00:00'
		}, {
			title: 'Click for Google',
			url: 'http://google.com/',
			start: '2021-11-28'
		}]
	});
	//select2 dropdown
	$('.select2').select2({
		minimumResultsForSearch: Infinity,
		width: '100%'
	});

	// Select2 by showing the search
	$('.select2-show-search').select2({
		minimumResultsForSearch: '',
		width: '100%'
	})

});