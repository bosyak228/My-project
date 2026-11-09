using UnityEngine;

[CreateAssetMenu(menuName = "Sporelight/MushroomData", fileName = "NewMushroom")]
public class MushroomData : ScriptableObject
{
    [Tooltip("Имя")]
    public string mushroomName = "Новый гриб";

    [Tooltip("Цена продажи")]
    public int price = 10;

    [Tooltip("Цвет")]
    public Color displayColor = Color.white;
}