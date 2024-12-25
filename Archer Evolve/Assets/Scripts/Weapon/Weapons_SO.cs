using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapons_SO : ScriptableObject
{
    public string weaponName;
    public string weaponType;
    public string weaponID;
    [HideInInspector] public float fireRate = 0;
    [HideInInspector] public float pullForce = 0;
    [HideInInspector] public float weight = 0;
    [SerializeField] private float baseFireRate;
    [SerializeField] private float basePullForce;
    [SerializeField] private float baseWeight;
    public Sprite weaponSprite;
    [TextArea(3,10)]
    public string discription;

    public void SetupWeapon()
    {
        fireRate = baseFireRate;
        pullForce = basePullForce;
        weight = baseWeight;
    }
}
