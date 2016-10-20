Imports System.Drawing
Imports SFML.Graphics

Public Interface IItemTicker
    Inherits IEntity

    Enum ItemType As Byte
        Coin '1 Coins
        RedCoin 'Item required to unlock something
        BlueCoin  '5 Coins
        GreenCoin '-1 Coins
        WumpaFruit 'Small HP Boost
        Drill 'Destroys blocks
        WindBoostWeak
        WindBoostNormal
        WindBoostStrong
        WindSlowWeak
        WindSlowNormal
        WindSlowStrong
        WindDownWeak
        WindDownNormal
        WindDownStrong
        WindUpWeak
        WindUpNormal
        WindUpStrong
        Key
        Door
        StartPos
        FinishPos
        Checkpoint
        Projectile
        Light
    End Enum

    Property XSpeed As Single

    Property YSpeed As Single

    Property STRItemType As ItemType

    Property Sprite As Sprite

    Property SpriteColor As SFML.Graphics.Color

    Property AttachedObj As IEntity

    Sub SetAttachedObj(ByRef Obj As IEntity, Optional ByVal CenterOffset As Boolean = True)

    Function GetAttachedObj() As IEntity

    Sub Tick()

    Sub Draw(ByRef w As RenderWindow)


End Interface
