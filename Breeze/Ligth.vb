Imports SFML.Graphics

Public Class Light
    Inherits Item

    Public Color As Color
    Public AoE As Single
    Public Shade As Single

    Public Sub New(ByVal Color As Color, AoE As Single, Shade As Single)
        Color = Color
        AoE = AoE
        Shade = Shade
        Item = ItemType.Light
    End Sub
End Class
