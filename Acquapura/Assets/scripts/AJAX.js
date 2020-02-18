//generic ajax function
var methodTypePost = "POST";
var methodTypeGet = "GET";
function callAjaxMethod(url, method, parameters, successCallback) {

	try {
		$.ajax({
			type: method,
			url: url,
			data: parameters,
			cache: false,
			beforeSend: function () {
				//enable loader
				$('.loader').fadeIn();
			},
			success: successCallback,
			error: function (xhr, textStatus, errorThrown) {
				//console.log(errorThrown);
				$('.loader').fadeOut();
			},
			complete: function () {
				//disable loader
				$('.loader').fadeOut();
			}
		});
	} catch (e) {
		console.log(e);
	}
};

////common ajax handler to run ajax request
//var ajaxHandler = function (url, methodType, parameters, func) {

//	function onSuccess(response) {
//		//response = response.trim();
//		if (response === 'SESSION_EXPIRED') {
//			alert("Your session is expired. Please login to continue.");
//			window.location.href = 'login.htm?auth=1';
//		}
//		else
//			func(response);
//	};
//	try {
//		callAjaxMethod(url, methodType, parameters, onSuccess);
//	} catch (e) {
//		console.log(e);
//	}
//};

////sample code to make ajax call
//var url = "test.html";
//var dataParams = { name: 'hello' };
//ajaxHandler(url, methodTypePost, dataParams, function (data) {
//	if (data !== "INVALID_PARAMETERS") {
//		//appropriate feedback to the user.	
//	}
//	else {
//		alert("There is a problem on server side. Please try again later.");
//	}
//	console.log(data);
//});