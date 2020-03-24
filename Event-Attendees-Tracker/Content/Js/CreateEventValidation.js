function Validate() {
    if (ValidateFileUpload())
        return true;
    return false;
}

function ValidateFileUpload() {
   
    if (document.getElementById("poster-upload").value == "" || document.getElementById("file-upload").value == "") {
        alert("You forgot to attach file!");    
        return false;
    }
    else {

        var poster_field = document.getElementById("poster-upload").value;
        var file_field = document.getElementById("file-upload").value;

        var posterExtension = poster_field.substr(poster_field.lastIndexOf('.') + 1).toLowerCase();
        var fileExtension = file_field.substr(file_field.lastIndexOf('.') + 1).toLowerCase();
        var allowedPosterExtensions = ['jpeg', 'png', 'jpg'];
        var allowedFileExtensions = ['xls', 'csv', 'xlsx'];
        if (poster_field.length > 0 && file_field.length > 0) {
            if (allowedPosterExtensions.indexOf(posterExtension) === -1) {
                alert('Invalid file Format in upload poster. Only ' + allowedPosterExtensions.join(', ') + ' are allowed.');
                return false;
            }
            if (allowedFileExtensions.indexOf(fileExtension) === -1) {
                alert('Invalid file Format in upload file. Only ' + allowedFileExtensions.join(', ') + ' are allowed.');
                return false;
            }
        }    
    }   
    return true;
}
function DateValidate(){
    var now = new Date();
    currentDate = now.toISOString().substring(0, 10);
    document.getElementById("date").setAttribute("min", currentDate);
}

function StartTime() {

    var now = new Date();
    currentDate = now.toISOString().substring(0, 10);
    var input = document.getElementById("date").value;

    if (input == currentDate) {
        var current = now.getHours() + ":" + now.getMinutes();
        document.getElementById("starttime").setAttribute("min", current);
    }
    else {
        document.getElementById("starttime").removeAttribute("min");
    }
}

function TimeValidation() {
    
    var timefrom = document.getElementById("starttime").value;
    var timeto = document.getElementById("endtime").value;
  
    if (timefrom > timeto) {
        alert("end time should be greater");
        document.getElementById("endtime").value = null;
    }  
}