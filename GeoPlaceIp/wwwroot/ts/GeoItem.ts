export class GeoItem {
    constructor(
        public country: string,      // название страны (случайная строка с префиксом "cou_")
        public region: string,       // название области (случайная строка с префиксом "reg_")
        public postal: string,       // почтовый индекс (случайная строка с префиксом "pos_")
        public city: string,         // название города (случайная строка с префиксом "cit_")
        public organization: string,  // название организации (случайная строка с префиксом "org_")
        public latitude: number,        // широта
        public longitude: number		// долгота
    ){

    }
}


