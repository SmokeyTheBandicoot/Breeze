Public Class CheckPoint
    Inherits Item

    'Item Data
    Public IsLatest As Boolean = False
    Public IsEndLevel As Boolean = False

    'Save data
    Public PlXSpeed As Single = 0
    Public PlYSpeed As Single = 0
    Public PlXAccel As Single = 0
    Public PlYAccel As Single = 0
    Public PlGravity As Single = 0
    Public PlCoins As Single = 0
    Public PlScore As Single = 0

    Public Shadows ReadOnly Item As ItemType = ItemType.Checkpoint
End Class