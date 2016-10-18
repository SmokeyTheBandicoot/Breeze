Imports System.Drawing
Imports GameShardsBreeze
Imports SFML.Graphics

Public Class Player
    Implements IEntity

    Private _ID As Integer
    Private _Location As Point
    Private _Size As Size

    Private _LColor As New SFML.Graphics.Color
    Private _BColor As New SFML.Graphics.Color
    Private _Sprite As New Sprite '(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Bankruptcy\[Bankruptcy]Bankruptcy.png"))

    Public Property LightColor As SFML.Graphics.Color
        Get
            Return _LColor
        End Get
        Set(value As SFML.Graphics.Color)
            _LColor = value
        End Set
    End Property

    Public Property BodyColor As SFML.Graphics.Color
        Get
            Return _BColor
        End Get
        Set(value As SFML.Graphics.Color)
            _BColor = value
        End Set
    End Property

    Public Property Sprite As Sprite
        Get
            Return _Sprite
        End Get
        Set(value As Sprite)
            _Sprite = value
        End Set
    End Property

    Public Property ID As Integer Implements IEntity.ID
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Public Property Size As Size Implements IEntity.Size
        Get
            Return _Size
        End Get
        Set(value As Size)
            _Size = value
        End Set
    End Property

    Public Property Location As Point Implements IEntity.Location
        Get
            Return _Location
        End Get
        Set(value As Point)
            _Location = value
        End Set
    End Property

    Public ReadOnly Property Center As Point Implements IEntity.Center
        Get
            Return New Point(CInt(Location.X + Size.Width / 2), CInt(Location.Y + Size.Height / 2))
        End Get
    End Property

    Public ReadOnly Property Bounds As Rectangle Implements IEntity.Bounds
        Get
            Return New Rectangle(Location, Size)
        End Get
    End Property
End Class
