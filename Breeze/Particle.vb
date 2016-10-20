Imports System.Drawing
Imports GameShardsBreeze
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore
'Imports NCalc

Public Class Particle
    Implements IEntityTicker

    Private _ID As Integer
    Private _XSpeed As Single
    Private _YSpeed As Single
    Private _XEspression As String
    Private _YEspression As String
    Private _STREntityType As IEntityTicker.EntityType
    Private _Corners As Integer
    Private _RotationSpeed As Single
    Private _Cshape As CircleShape
    Private _Texture As Texture
    Private _Location As Point
    Private _Size As Size
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

    Public Property XEspression As String
        Get
            Return _XEspression
        End Get
        Set(value As String)
            _XEspression = value
        End Set
    End Property

    Public Property yEspression As String
        Get
            Return _YEspression
        End Get
        Set(value As String)
            _YEspression = value
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

    Public Property Texture As Texture Implements IEntityTicker.Texture
        Get
            Return _Texture
        End Get
        Set(value As Texture)
            _Texture = value
        End Set
    End Property

    Public Property Color As SFML.Graphics.Color Implements IEntityTicker.Color
        Get
            Return _Color
        End Get
        Set(value As SFML.Graphics.Color)
            _Color = value
        End Set
    End Property

    Public Property CShape As CircleShape Implements IEntityTicker.CShape
        Get
            Return _Cshape
        End Get
        Set(value As CircleShape)
            _Cshape = value
        End Set
    End Property

    Public Property RotationSpeed As Single
        Get
            Return _RotationSpeed
        End Get
        Set(value As Single)
            _RotationSpeed = value
        End Set
    End Property

    Private Sub IItemTicker_Tick() Implements IEntityTicker.Tick '(ByVal AttachedObj As FloatRect) Implements IItemTicker.Tick
        Location = New Point(CInt(Location.X + XSpeed), CInt(Location.Y + YSpeed))
        CShape.Position = New Vector2f(Location.X, Location.Y)
        CShape.Rotation -= RotationSpeed
    End Sub

    Public Sub Draw(ByRef w As RenderWindow) Implements IEntityTicker.Draw
        w.Draw(CShape)
    End Sub

    Public Sub New()
        STREntityType = IEntityTicker.EntityType.Particle
    End Sub

    Sub New(Id As Integer, xspeed As Single, yspeed As Single, location As Point, size As Size, variation As Integer, texture As Texture, color As SFML.Graphics.Color, corners As Integer, rotation0 As Single, rotationspeed As Integer, AttachOffset As Point, Optional ByVal XEspr As String = "0", Optional ByVal YEspr As String = "0")
        _ID = 0
        _XSpeed = xspeed
        _YSpeed = yspeed
        _XEspression = XEspr
        _YEspression = YEspr
        _Location = location
        _Size = size
        _Cshape = New CircleShape
        _Cshape.Texture = texture
        _Cshape.Origin = New Vector2f(_Cshape.Origin.X + _Cshape.GetGlobalBounds.Width / 2, _Cshape.Origin.Y + _Cshape.GetGlobalBounds.Height / 2)
        _Cshape.Radius = size.Width
        _Cshape.SetPointCount(CUInt(corners))
        _Cshape.Rotation = rotation0
        _Cshape.FillColor = color
        _Cshape.OutlineColor = New SFML.Graphics.Color(0, 0, 0, 0)
        _Color = color
        STREntityType = IEntityTicker.EntityType.Particle
    End Sub
End Class
