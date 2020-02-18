jQuery(document).ready(function () {
    $.ajaxSetup({ cache: false });
    GetDashBoardData();
    $(".data_modal").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});

function OpenModelPopup(elem) {

    $.ajaxSetup({ cache: false });
    var href = elem.attributes.data.nodeValue;

    $('#myModalContent').load(href, function () {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false
        }, 'show');
        bindForm(this);
    });
}

function bindForm(dialog) {
    $('.form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    var id = "replacetarget";
                    $('#' + id).html('<div class="lds-ripple"><div class="lds-pos"></div><div class="lds-pos"></div></div>');
                    $('#myModal').modal('hide');
                    if (result.index) {
                        id += result.index;
                    }
                    $('#' + id).load(result.url, function () {
                        if (result.callback != null) {
                            GetList();
                        }
                    });
                    //$('#datatable-responsive').DataTable().ajax.reload();
                    //  Load data from the server and place the returned HTML into the matched element
                } else {
                    $('#divMessage').text(result.message);
                    $('#divMessage').fadeIn(result.message);
                }
            }
        });
        return false;
    });
}

// Restricts input for the given textbox to the given inputFilter.
function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
        });
    });
}

function GetDashBoardData() {
    $.ajax({
        type: 'Get',
        url: "/Admin/GetDashboardData",
        success: function (response) {
            BindDashboardData(response.data);
        }
    });


}

function BindDashboardData(countdata) {
    if (countdata) {
        $('#countuser').text(countdata.UsersCount);
        $('#countcategories').text(countdata.CategoriesCount);
        $('#countorders').text(countdata.OrdersCount);
        $('#countcustomers').text(countdata.CustomersCount);
    }
}

function getParameterByName(name, url) {
	if (!url) url = window.location.href;
	name = name.replace(/[\[\]]/g, '\\$&');
	var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
	if (!results) return null;
	if (!results[2]) return '';
	return decodeURIComponent(results[2].replace(/\+/g, ' '));
}