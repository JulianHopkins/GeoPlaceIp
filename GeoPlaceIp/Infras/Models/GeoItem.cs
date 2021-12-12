using System;

public class GeoItem
{
	public sbyte[] country = new sbyte[8];        // название страны (случайная строка с префиксом "cou_")
	public sbyte[] region = new sbyte[12];        // название области (случайная строка с префиксом "reg_")
	public sbyte[] postal = new sbyte[12];        // почтовый индекс (случайная строка с префиксом "pos_")
	public sbyte[] city = new sbyte[24];          // название города (случайная строка с префиксом "cit_")
	public sbyte[] organization = new sbyte[32];  // название организации (случайная строка с префиксом "org_")
	public float latitude;          // широта
	public float longitude;         // долгота
}
