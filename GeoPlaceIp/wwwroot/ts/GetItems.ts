/// <reference path="GeoItem.ts"/>
import { GeoItem } from 'GeoItem';
class GetItems {
    getItems(): GeoItem {
        return new GeoItem('China','SingHuan','Cainio','Pekin','Apple',47.56,37.23);
    }
}