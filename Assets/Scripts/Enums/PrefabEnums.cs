/// <summary>
/// Enum <c>MapAssetEnum</c>
/// Reprents all the spawnable map asset objects
/// </summary>
public enum MapAssetEnum
{
    Elevator,
    Healthkit,
    Barrier,
    Bitcoin
}

/// <summary>
/// Enum <c>CharacterEnum</c>
/// Represents all the spawnable character objects
/// </summary>
public enum CharacterEnum
{
    Enemy,
    Player
}

/// <summary>
/// Enum <c>WeaponEnum</c>
/// Represents all the spawnable weapon objects
/// </summary>
public enum WeaponEnum
{
    ElectricGuitar,
    Launcher,
    LightAR,
    MiniRifle,
    SciFiHandGun,
    ShotGun,
    Unarmed
}

// <summary>
/// Enum <c>BulletEnum</c>
/// Represents all the spawnable bullet objects
/// </summary>
public enum BulletEnum
{
    AR,
    Field,
    MiniGun,
    Pistol,
    Rocket,
    ShotPellet
}


public enum TransferEnum
{
    Launcher = BulletEnum.Rocket,
    LightAR =  BulletEnum.AR,
    MiniRifle = BulletEnum.MiniGun,
    SciFiHandGun = BulletEnum.Pistol,
    ShotGun = BulletEnum.ShotPellet,
} 