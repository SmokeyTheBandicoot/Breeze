Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System

Public Interface IMovableObject

    Property ID As Integer

    Property Layer As Integer

    Property Location As Point

    Property Hardness As Byte

    Property Size As Point

    Property Sprite As Sprite

    Property Color As SFML.Graphics.Color

    Property XEspr As String

    Property YEspr As String

    Property XSpeed As Single

    Property YSpeed As Single

    Sub Draw(ByRef w As RenderWindow)
End Interface
