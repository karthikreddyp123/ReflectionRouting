//Validation for emaiLID
function emailValidation(event) {
    let emailID = $('#emailID')
    if (emailID.val() == "") {
        emailID.addClass('is-invalid');
        $('#emailHelper').removeClass('text-muted');
        $('#emailHelper').html("Username Required");
        event.preventDefault();
    }
    else {
        $('#emailHelper').html("");
        emailID.removeClass('is-invalid');
        $('#emailHelper').addClass('text-muted');
    }
}

//Validation for Password
function passwordValidation(event) {
    let password = $('#password')
    if (password.val() == "") {
        password.addClass('is-invalid');
        $('#passwordHelpBlock').removeClass('text-muted');
        $('#password').addClass('is-invalid');
        $('#passwordHelpBlock').html('Password Required');
        event.preventDefault();
    }
    else {
        $('#passwordHelpBlock').addClass('text-muted');
        $('#password').removeClass('is-invalid');
        $('#passwordHelpBlock').html('');
    }
}


$('#submitcheck').click((event) => {
    emailValidation(event);
    passwordValidation(event);
})

$('#emailID').blur((event) => {
    emailValidation(event);    
})

$('#password').blur((event) => {
    passwordValidation(event);
})

