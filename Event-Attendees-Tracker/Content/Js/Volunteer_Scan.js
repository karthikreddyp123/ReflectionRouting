var video = document.createElement("video");
var canvasElement = document.getElementById("canvas");
var canvas = canvasElement.getContext("2d");
var loadingMessage = document.getElementById("loadingMessage");
var outputContainer = document.getElementById("output");
var outputMessage = document.getElementById("outputMessage");
var outputData = document.getElementById("outputData");
var listOfQrStrings = new Set();

function stopScan() {
    window.history.back();
}
function drawLine(begin, end, color) {
    canvas.beginPath();
    canvas.moveTo(begin.x, begin.y);
    canvas.lineTo(end.x, end.y);
    canvas.lineWidth = 4;
    canvas.strokeStyle = color;
    canvas.stroke();
}

// Use facingMode: environment to attemt to get the front camera on phones
navigator.mediaDevices.getUserMedia({ video: { facingMode: "environment" } }).then(function (stream) {
    video.srcObject = stream;
    video.setAttribute("playsinline", true); // required to tell iOS safari we don't want fullscreen
    video.play();
    requestAnimationFrame(tick);
});

function tick() {
    loadingMessage.innerText = "⌛ Loading scanner..."
    if (video.readyState === video.HAVE_ENOUGH_DATA) {
        loadingMessage.hidden = true;
        canvasElement.hidden = false;
        outputContainer.hidden = false;

        canvasElement.height = video.videoHeight;
        canvasElement.width = video.videoWidth;
        canvas.drawImage(video, 0, 0, canvasElement.width, canvasElement.height);
        var imageData = canvas.getImageData(0, 0, canvasElement.width, canvasElement.height);
        var code = jsQR(imageData.data, imageData.width, imageData.height, {
            inversionAttempts: "dontInvert",
        });
        if (code) {
            drawLine(code.location.topLeftCorner, code.location.topRightCorner, "#FF3B58");
            drawLine(code.location.topRightCorner, code.location.bottomRightCorner, "#FF3B58");
            drawLine(code.location.bottomRightCorner, code.location.bottomLeftCorner, "#FF3B58");
            drawLine(code.location.bottomLeftCorner, code.location.topLeftCorner, "#FF3B58");
            outputMessage.hidden = true;
            outputData.parentElement.hidden = false;
            var qrString = code.data;
            outputData.innerText = qrString;
            if (listOfQrStrings.has(qrString)) {
            } else {
                listOfQrStrings.add(qrString);
                this.markAttendance(qrString);
            }
            studentCount.innerText = " Number of students scanned: " + listOfQrStrings.size;
        } else {
            outputMessage.hidden = false;
            outputData.parentElement.hidden = true;
        }
    }
    requestAnimationFrame(tick);
}
function markAttendance(qrString) {
    $.ajax({
        url: "api/Volunteer/MarkAttendance/" + qrString,
        success: function (data) {
        },
        error: function (errorThrown) {
            alert('Please scan previous student QR Code again.');
            listOfQrStrings.delete(qrString);
        }
    });
};
$(document).ready(function () {
    var volunteerID = 'ViewBag.volunteerID';
    $.ajax({
        url: "api/Volunteer/GetActiveEventDetails/" + volunteerID,
        context: document.body,
        success: async function (data) {
            const hasEvent = data.Status == 'OK';
            if (hasEvent) {
                hasActiveEventMessage.hidden = true;
                hasActiveEvent.hidden = false;
                const obj = JSON.parse(data.Event);
                eventName.innerText = obj.EventName;
                eventVenue.innerText = obj.Venue;
            } else {
                hasActiveEventMessage.innerText = "No Active Event Found. Going back to dashboard...";
                hasActiveEventMessage.hidden = false;
                hasActiveEvent.hidden = true;
                await new Promise(r => setTimeout(r, 5000));
                window.history.back();
            }
        },
        error: async function (errorThrown) {
            hasActiveEventMessage.innerText = "Unknown error Occured.Try again later. Going back to dashboard...";
            hasActiveEventMessage.hidden = false;
            hasActiveEvent.hidden = true;
            await new Promise(r => setTimeout(r, 4000));
            window.history.back();
        }
    });
});