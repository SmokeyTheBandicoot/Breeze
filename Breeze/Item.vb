Imports System.Windows.Forms
Public Class Item
    Inherits PictureBox


    Public ID As Integer
    Public XSpeed As Single = 0
    Public YSpeed As Single = 0

    Public Item As ItemType

    Public Enum ItemType As Byte
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
End Class

