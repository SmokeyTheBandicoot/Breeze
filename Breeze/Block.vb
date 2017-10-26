Imports SFML.Graphics
Imports SFML.System
Imports System.Drawing
Imports GameShardsBreeze

Public Class Block
    Implements IMovableObject

    Private _Color As SFML.Graphics.Color
    Private _Hardness As Byte
    Private _IDStr As String
    Private _Layer As Integer
    Private _Location As Point
    Private _Size As Size
    Private _Sprite As Sprite
    Private _XEspr As String
    Private _XSpeed As Single
    Private _YEspr As String
    Private _YSpeed As Single

    Public Property Color As SFML.Graphics.Color Implements IMovableObject.Color
        Get
            Return _Color
        End Get
        Set(value As SFML.Graphics.Color)
            _Color = value
        End Set
    End Property

    Public Property Hardness As Byte Implements IMovableObject.Hardness
        Get
            Return _Hardness
        End Get
        Set(value As Byte)
            _Hardness = value
        End Set
    End Property

    Public Property IDStr As String Implements IMovableObject.IDStr
        Get
            Return _IDStr
        End Get
        Set(value As String)
            _IDStr = value
        End Set
    End Property

    Public Property Layer As Integer Implements IMovableObject.Layer
        Get
            Return _Layer
        End Get
        Set(value As Integer)
            _Layer = value
        End Set
    End Property

    Public Property Location As Point Implements IMovableObject.Location
        Get
            Return _Location
        End Get
        Set(value As Point)
            _Location = value
        End Set
    End Property

    Public Property Size As Size Implements IMovableObject.Size
        Get
            Return _Size
        End Get
        Set(value As Size)
            _Size = value
        End Set
    End Property

    Public Property Sprite As Sprite Implements IMovableObject.Sprite
        Get
            Return _Sprite
        End Get
        Set(value As Sprite)
            _Sprite = value
        End Set
    End Property

    Public Property XEspr As String Implements IMovableObject.XEspr
        Get
            Return _XEspr
        End Get
        Set(value As String)
            _XEspr = value
        End Set
    End Property

    Public Property XSpeed As Single Implements IMovableObject.XSpeed
        Get
            Return _XSpeed
        End Get
        Set(value As Single)
            _XSpeed = value
        End Set
    End Property

    Public Property YEspr As String Implements IMovableObject.YEspr
        Get
            Return _YEspr
        End Get
        Set(value As String)
            _YEspr = value
        End Set
    End Property

    Public Property YSpeed As Single Implements IMovableObject.YSpeed
        Get
            Return _YSpeed
        End Get
        Set(value As Single)
            _YSpeed = value
        End Set
    End Property

    Public Sub Draw(ByRef w As RenderWindow) Implements IMovableObject.Draw
        w.Draw(Sprite)
    End Sub

    Public Sub Tick() Implements IMovableObject.Tick


        'XSpeed = CSng(e.calculate)

        'e = New Expression("YEspr")
        'YSpeed = CSng(e.calculate)

        'Location = New Point(CInt(Location.X + XSpeed), CInt(Location.Y + YSpeed))

        Sprite.Position = New Vector2f(Location.X, Location.Y)

        'e = Nothing
    End Sub

    Public Sub New()
        Size = New Size(32, 32)
    End Sub

    Public Sub PlayerCollide() Implements IMovableObject.PlayerCollide
        'MsgBox("Collided with player  " & Me.ToString)
    End Sub
End Class
