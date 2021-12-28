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
