System.register("GeoItem", [], function (exports_1, context_1) {
    "use strict";
    var GeoItem;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            GeoItem = class GeoItem {
                constructor(country, region, postal, city, organization, latitude, longitude) {
                    this.country = country;
                    this.region = region;
                    this.postal = postal;
                    this.city = city;
                    this.organization = organization;
                    this.latitude = latitude;
                    this.longitude = longitude;
                }
            };
            exports_1("GeoItem", GeoItem);
        }
    };
});
System.register("GetItems", ["GeoItem"], function (exports_2, context_2) {
    "use strict";
    var GeoItem_1, GetItems;
    var __moduleName = context_2 && context_2.id;
    return {
        setters: [
            function (GeoItem_1_1) {
                GeoItem_1 = GeoItem_1_1;
            }
        ],
        execute: function () {
            GetItems = class GetItems {
                getItems() {
                    return new GeoItem_1.GeoItem('China', 'SingHuan', 'Cainio', 'Pekin', 'Apple', 47.56, 37.23);
                }
            };
        }
    };
});
//# sourceMappingURL=main.js.map