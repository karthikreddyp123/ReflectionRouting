window.onload = function () {

    var name = sessionStorage.getItem('name');
    if (name != null) $('#name').val(name);
    var venue = sessionStorage.getItem('venue');
    if (venue != null) $('#venue').val(venue);
    var date = sessionStorage.getItem('date');
    if (date != null) $('#date').val(date);
    var poster-upload = sessionStorage.getItem('poster-upload');
    if (poster - upload != null) $('#poster-upload').val(poster - upload);
    var file-upload = sessionStorage.getItem('file-upload');
    if (file - upload != null) $('#file-upload').val(file - upload);
    var description = sessionStorage.getItem('description');
    if (description != null) $('#description').val(description);



}
window.onbeforeunload = function () {
    sessionStorage.setItem("name", $('#name').val());
    sessionStorage.setItem("venue", $('#venue').val());
    sessionStorage.setItem("date", $('#date').val());
    sessionStorage.setItem("poster-upload", $('#time1').val());
    sessionStorage.setItem("file-upload", $('#file-upload').val());
    sessionStorage.setItem("description", $('#description').val());
}