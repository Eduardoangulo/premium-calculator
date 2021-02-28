var app = app || {}; app.pages = app.pages || {};

app.pages.Calculator = app.pages.Calculator || (function () {

    var URL = {
        GetValuePremium: '/Home/GetValuePremium',
        CalculateOtherAmounts: '/Home/CalculateOtherAmounts',
    }

    function getValuePremium() {
        clearControls();

        var form = $('#frmCalculator');

        var request = {
            Age: $('#txtAge').val(),
            DateBirth: $('#dtDateBirth').val(),
            State: $('#txtState').val(),
        };

        if (form.valid()) {

            $.post(URL.GetValuePremium, { premiumCalculatorRequest: request })
                .done(function response(response) {
                    if (response.isSuccess) {
                        $('#txtPremium').val(response.premiumValue);
                        $('#lstFrequency').prop('disabled', false);
                    } else {
                        alert(response.errorMessage);
                    }
                }).fail(function (xhr, status, error) {
                    alert(xhr.responseText);
                });
        }
    }

    function calculateOtherAmounts() {
        var frecuencyValue = $(this).val();

        var request = {
            PremiumValue: $('#txtPremium').val(),
            FrequencyValue: frecuencyValue,
        };

        $.post(URL.CalculateOtherAmounts, { premiumCalculatorModel: request })
            .done(function response(response) {
                $('#txtMonthlyValue').val(response.monthlyValue);
                $('#txtAnnualValue').val(response.annualValue);


            }).fail(function (xhr, status, error) {
                alert(xhr.responseText);
            });

    }

    function clearControls() {
        $('#txtPremium').val('');
        $('#txtMonthlyValue').val('');
        $('#txtAnnualValue').val('');
        $('#lstFrequency').val('');
        $('#lstFrequency').prop('disabled', true);
    }

    function InitCalculator() {
        $('#btnGetValue').on('click', getValuePremium);
        $('#lstFrequency').on('change', calculateOtherAmounts);
    }

    return {
        InitCalculator: InitCalculator
    }
})();