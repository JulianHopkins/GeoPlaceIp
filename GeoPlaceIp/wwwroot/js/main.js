class GeoItem {
    constructor(country, region, postal, city, organization, latitude, longitude) {
        this.country = country;
        this.region = region;
        this.postal = postal;
        this.city = city;
        this.organization = organization;
        this.latitude = latitude;
        this.longitude = longitude;
    }
}

class Exc {
    constructor(extype, message) {
        this.extype = extype;
        this.message = message;
    }
}
class GetItems {
    setResult(url) {
        $(document).ready(() => {
            let mnb = $(".mainblock1");
            let restab = $(".resultTab");
            mnb.hide();
            restab.html("");
            $.get(url, (data) => {
                let opr = data;
                if (opr == null || opr === undefined) {
                    notification.error("Null object accepted", 7000);
                    return;
                }
                if (opr.error != null) {
                    if (opr.error.extype.indexOf('KeyNotFoundException') != -1) {
                        notification.info(opr.error.message, 7000);
                    }
                    else {
                        notification.error(opr.error.message, 7000);
                    }
                    return;
                }
                if (opr.gi != null) {
                    mnb.show();
                    restab.html(this.getGiHTML(opr.gi));
                    return;
                }
                if (opr.items != null) {
                    let tb;
                    for (let y of opr.items) {
                        tb = `${tb}${this.getGiHTML(y)}`;
                    }
                    mnb.show();
                    restab.html(tb);
                    return;
                }
                console.log(data);
            });
        });
    }
    getGiHTML(gi) {
        return `<tr><td><dl class="row">
<dt class="col-sm-3">Country</dt><dd class="col-sm-9">${gi.country}</dd>
<dt class="col-sm-3">Region</dt><dd class="col-sm-9">${gi.region}</dd>
<dt class="col-sm-3">Postal</dt><dd class="col-sm-9">${gi.postal}</dd>
<dt class="col-sm-3">City</dt><dd class="col-sm-9">${gi.city}</dd>
<dt class="col-sm-3">Organization</dt><dd class="col-sm-9">${gi.organization}</dd>
<dt class="col-sm-3">Latitude</dt><dd class="col-sm-9">${gi.latitude.toString()}</dd>
<dt class="col-sm-3">Longitute</dt><dd class="col-sm-9">${gi.longitude.toString()}</dd>
</dl></td></tr>`;
    }
}

class OperationResult {
    constructor(gi, items, error) {
        this.gi = gi;
        this.items = items;
        this.error = error;
    }
}

class PageBase {
    constructor(url, placeholder, labeltext, historytitle, validator, mustextract) {
        this.validator = validator;
        this.mustextract = mustextract;
        let startvalue;
        this._historytitle = historytitle;
        let wndl = window.location.href;
        let yz = wndl.lastIndexOf("#");
        this._urlpostfix = wndl.substr(yz + 1).trim();
        if (mustextract) {
            let xz = wndl.indexOf('?');
            if (xz != -1) {
                xz = wndl.indexOf('=', xz + 1);
                if (xz != -1) {
                    let lz = yz - xz;
                    if (lz > 0) {
                        startvalue = wndl.substr(xz + 1, lz - 1);
                    }
                }
            }
        }
        let xr = url.indexOf('?');
        let jr = url.indexOf('=', xr + 1);
        let lr = jr - xr;
        this._urlparamname = url.substr(xr + 1, lr);
        let restab = $(".resultTab");
        let mnb = $(".mainblock1");
        restab.html("");
        mnb.hide();
        var _label = $('.searchlabel');
        this._inputField = $('.searchinput');
        this._inputField.val('');
        this._validationText = $('.validationmess');
        this._searchButton = $('.searchbutton');
        this._searchButton.prop('disabled', true);
        this._searchButton.off("click");
        this._searchButton.on("click", (Obj) => this.StartSearch(Obj));
        this._inputField.off("keyup");
        this._inputField.on("keyup", (Obj) => this.ValidationFunc(Obj));
        if (this._getItems == null) {
            this._getItems = new GetItems();
        }
        this._urlfirst = url;
        this._inputField.attr({ "placeholder": placeholder });
        _label.html(labeltext);
        if (startvalue != null) {
            this._inputField.val(startvalue);
            var e = this._inputField.keypress();
            if (this._searchButton.prop('disabled') == false)
                this._searchButton.click();
            this._searchButton.click();
        }
        this.clearValidationText();
    }
    StartSearch(eventObj) {
        eventObj.stopPropagation();
        this._inputField.keyup();
        let v = this._validationText.html();
        let trg = $(eventObj.target);
        trg.prop('disabled', true);
        if (v != null && v != "") {
            return;
        }
        let ifieldtext = this._inputField.val().toString().trim();
        this._getItems.setResult(this._urlfirst + ifieldtext);
        trg.prop('disabled', false);
        let hrec = '?' + this._urlparamname + ifieldtext + '#' + this._urlpostfix;
        window.history.pushState(null, this._historytitle, hrec);
    }
    ValidationFunc(eventObj) {
        eventObj.stopPropagation();
        let trg = $(eventObj.target);
        let valout = this.validator(trg.val().toString().trim());
        if (valout != null) {
            this.showValidationText(valout);
        }
        else {
            this.clearValidationText();
        }
    }
    addErrorToIF() {
        if (!this._inputField.hasClass("is-invalid")) {
            this._inputField.addClass("is-invalid");
        }
    }
    removeErrorFromIF() {
        if (this._inputField.hasClass("is-invalid")) {
            this._inputField.removeClass("is-invalid");
        }
    }
    showValidationText(Text) {
        this._validationText.html(Text);
        this.addErrorToIF();
        this._searchButton.prop('disabled', true);
    }
    clearValidationText() {
        this._validationText.html("");
        this.removeErrorFromIF();
        this._searchButton.prop('disabled', false);
    }
}
var forCitySearch = (query) => {
    let nullmess = "Required field";
    if ((query == null) || (query === undefined))
        return nullmess;
    let len = query.length;
    if (len == 0)
        return nullmess;
    let prefix = "The search string must be prefixed with 'cit_'";
    if (len < 4)
        return prefix;
    if (query.substr(0, 4) != "cit_")
        return prefix;
    if (len < 6)
        return "Please enter more than 5 characters";
    if (len > 100)
        return "Please enter less than 100 characters";
    return null;
};
var forIpSearch = (query) => {
    let nullmess = "Required field";
    if ((query == null) || (query === undefined))
        return nullmess;
    let len = query.length;
    if (len == 0)
        return nullmess;
    var ip = /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;
    if (!ip.test(query))
        return "Type the correct IP address";
    return null;
};
class SearchIp extends PageBase {
    constructor() {
        let mustextract;
        let wnd = window.location.href;
        if (wnd.lastIndexOf("#geoip") == -1) {
            window.history.pushState(null, "GeoIp", "?#geoip");
            mustextract = false;
        }
        if (wnd.lastIndexOf("?#geoip") != -1)
            mustextract = false;
        mustextract = true;
        super("/ip/location?ip=", "123.234.123.234", "Type an IP address", "GeoIp", forIpSearch, mustextract);
    }
}
class SearchCity extends PageBase {
    constructor() {
        let mustextract;
        let wnd = window.location.href;
        if (wnd.lastIndexOf("#geocity") == -1) {
            window.history.pushState(null, "GeoCity", "?#geocity");
            mustextract = false;
        }
        if (wnd.lastIndexOf("?#geocity") != -1)
            mustextract = false;
        mustextract = true;
        super("/city/locations?city=", "cit_Gbqw4", "Type a city name", "GeoCity", forCitySearch, mustextract);
    }
}
class StartServ {
    constructor() {
        this.btnSearchIp = $('.fromip');
        this.btnSearchCity = $('.fromcity');
        this.btnSearchIp.on("click", (Obj) => this.BtnSearchIp_Click(Obj));
        this.btnSearchCity.on("click", (Obj) => this.BtnSearchCity_Click(Obj));
        let wnd = window.location.href;
        if (wnd.lastIndexOf("#geocity") != -1) {
            this.btnSearchCity.click();
        }
        else {
            this.btnSearchIp.click();
        }
    }
    BtnSearchIp_Click(eventObj) {
        eventObj.stopPropagation();
        let trg = $(eventObj.target);
        trg.prop('disabled', true);
        this.btnSearchCity.prop('disabled', false);
        this.Worker = new SearchIp();
    }
    BtnSearchCity_Click(eventObj) {
        eventObj.stopPropagation();
        let trg = $(eventObj.target);
        trg.prop('disabled', true);
        this.btnSearchIp.prop('disabled', false);
        this.Worker = new SearchCity();
    }
}
