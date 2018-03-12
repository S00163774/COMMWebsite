var numOfBookings = document.getElementsByName("TimetableDetail").length;
var numoOfStylists = document.getElementsByName("Stylist").length;
var stylists = [];

var timetable = new Timetable();
timetable.setScope(9, 22);

for (i = 0; i < numoOfStylists; i++)
{
    stylists.push(document.getElementsByName("Stylist")[i].value);
}

timetable.addLocations(stylists);

for (i = 0; i < numOfBookings; i++)
{
    var TimetableDetail = document.getElementsByName("TimetableDetail")[i].value;
    var splitDetail = TimetableDetail.split(",");

    timetable.addEvent(splitDetail[1], splitDetail[0], new Date(splitDetail[4], splitDetail[3], splitDetail[2], splitDetail[5], splitDetail[6]), new Date(splitDetail[4], splitDetail[3], splitDetail[2], splitDetail[7], splitDetail[8]));
}

var renderer = new Timetable.Renderer(timetable);
renderer.draw('.timetable');