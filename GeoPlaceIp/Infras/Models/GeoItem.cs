using System;

public class GeoItem
{
	public string country { get; set; }       // название страны (случайная строка с префиксом "cou_")
	public string region { get; set; }        // название области (случайная строка с префиксом "reg_")
	public string postal { get; set; }        // почтовый индекс (случайная строка с префиксом "pos_")
	public string city { get; set; }          // название города (случайная строка с префиксом "cit_")
	public string organization { get; set; }  // название организации (случайная строка с префиксом "org_")
	public float latitude { get; set; }         // широта
	public float longitude { get; set; }         // долгота

}
