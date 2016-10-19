Imports System.Drawing
Imports GameShardsBreeze
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore

Public Class Particle
    Implements IEntityTicker

    Private _ID As Integer
    Private _XSpeed As Single
    Private _YSpeed As Single
    Private _XEspression As String
    Private _YEspression As String
    Private _STREntityType As IEntityTicker.EntityType
    Private _Corners As Integer
    Private _Location As Point
    Private _Size As Size
    Private _Rotation As Single
    Private _Sprite As Sprite
    Private _Color As SFML.Graphics.Color

    Public Property ID As Integer Implements IEntity.ID
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Public ReadOnly Property Center As Point Implements IEntity.Center
        Get
            Return New Point(CInt(Location.X + Size.Width / 2), CInt(Location.Y + Size.Height / 2))
        End Get
    End Property

    Private ReadOnly Property Bounds As Rectangle Implements IEntity.Bounds
        Get
            Return New Rectangle(Location, Size)
        End Get
    End Property

    Public Property Size As Size Implements IEntity.Size
        Get
            Return _size
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

    Public Property XSpeed As Single Implements IEntityTicker.XSpeed
        Get
            Return _XSpeed
        End Get
        Set(value As Single)
            _XSpeed = value
        End Set
    End Property

    Public Property YSpeed As Single Implements IEntityTicker.YSpeed
        Get
            Return _YSpeed
        End Get
        Set(value As Single)
            _YSpeed = value
        End Set
    End Property

    Public Property STREntityType As IEntityTicker.EntityType Implements IEntityTicker.STREntityType
        Get
            Return _STREntityType
        End Get
        Set(value As IEntityTicker.EntityType)
            _STREntityType = value
        End Set
    End Property

    Public Property Sprite As Sprite Implements IEntityTicker.Sprite
        Get
            Return _Sprite
        End Get
        Set(value As Sprite)
            _Sprite = value
        End Set
    End Property

    Public Property Color As SFML.Graphics.Color Implements IEntityTicker.SpriteColor
        Get
    Return _Color
        End Get
        Set(value As SFML.Graphics.Color)
            _Color = value
        End Set
    End Property


    Private Sub IItemTicker_Tick() Implements IEntityTicker.Tick '(ByVal AttachedObj As FloatRect) Implements IItemTicker.Tick
        Location = New Point(CInt(Location.X + XSpeed), CInt(Location.Y + YSpeed))

    End Sub

    Public Sub Draw(ByRef w As RenderWindow) Implements IEntityTicker.Draw
        w.Draw(Sprite)
    End Sub

    Public Sub New()
        STREntityType = IEntityTicker.EntityType.Particle
    End Sub

    Sub New(Id As Integer, xspeed As Single, yspeed As Single, location As Point, size As Size, variation As Integer, color As SFML.Graphics.Color, AttachOffset As Point)
        _ID = 0
        _XSpeed = xspeed
        _YSpeed = yspeed
        _Location = location
        _Size = size
        _Sprite = New Sprite(MainGame.LightText)
        _Sprite.Origin = New Vector2f(_Sprite.Origin.X + _Sprite.GetGlobalBounds.Width / 2, _Sprite.Origin.Y + _Sprite.GetGlobalBounds.Height / 2)
        _Color = color
    End Sub
End Class
