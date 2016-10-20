Imports System.Drawing
Imports GameShardsBreeze
Imports SFML.Graphics
Imports SFML.System

Public Class Light
    Implements IItemTicker

    Private _ID As Integer
    Private _XSpeed As Single
    Private _YSpeed As Single
    Private _ItemType As IItemTicker.ItemType
    Private _AttachedObj As IEntity
    Private _Radius As Integer
    Private _Variation As Integer
    Private _Location As Point
    Private _Sprite As Sprite
    Private _SpriteColor As SFML.Graphics.Color
    Private _AttachOffset As Point

    Private LightShade As New Sprite

    Public Property AttachOffset As Point
        Get
            Return _AttachOffset
        End Get
        Set(value As Point)
            _AttachOffset = value
        End Set
    End Property

    Public Property Variation As Integer
        Get
            Return _Variation
        End Get
        Set(value As Integer)
            _Variation = value
        End Set
    End Property

    'Public ReadOnly Property SpriteForm As Sprite
    '    Get
    '        Return New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\LightShade.png"))
    '    End Get
    'End Property

    Public Property ID As Integer Implements IItemTicker.ID
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
            Return New Size(_Radius, _Radius)
        End Get
        Set(value As Size)
            _Radius = value.Width
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

    Public Property XSpeed As Single Implements IItemTicker.XSpeed
        Get
            Return _XSpeed
        End Get
        Set(value As Single)
            _XSpeed = value
        End Set
    End Property

    Public Property YSpeed As Single Implements IItemTicker.YSpeed
        Get
            Return _YSpeed
        End Get
        Set(value As Single)
            _YSpeed = value
        End Set
    End Property

    Public Property STRItemType As IItemTicker.ItemType Implements IItemTicker.STRItemType
        Get
            Return _ItemType
        End Get
        Set(value As IItemTicker.ItemType)
            _ItemType = value
        End Set
    End Property

    Public Property Sprite As Sprite Implements IItemTicker.Sprite
        Get
            Return _Sprite
        End Get
        Set(value As Sprite)
            _Sprite = value
        End Set
    End Property

    Private Property AttachedObj As IEntity Implements IItemTicker.AttachedObj
        Get
            Return _AttachedObj
        End Get
        Set(value As IEntity)
            _AttachedObj = value
        End Set
    End Property

    Public Property SpriteColor As SFML.Graphics.Color Implements IItemTicker.SpriteColor
        Get
            Return _SpriteColor
        End Get
        Set(value As SFML.Graphics.Color)
            _SpriteColor = value
        End Set
    End Property

    Private Sub IItemTicker_Tick() Implements IItemTicker.Tick '(ByVal AttachedObj As FloatRect) Implements IItemTicker.Tick

        'Location = New Point(CInt(AttachedObj.Left + AttachedObj.Width / 2), CInt(AttachedObj.Top + AttachedObj.Height / 2))
        'Sprite.Position = New Vector2f(AttachedObj.Left, AttachedObj.Top)

        If AttachedObj IsNot Nothing Then
            Location = New Point(CInt(AttachedObj.Location.X + AttachedObj.Size.Width / 2), CInt(AttachedObj.Location.Y + AttachedObj.Size.Height / 2))
        Else
            Location = New Point(CInt(Location.X + XSpeed), CInt(Location.Y + YSpeed))
        End If

        Sprite.Position = New Vector2f(Location.X + AttachOffset.X, Location.Y + AttachOffset.Y)
        LightShade.Position = Sprite.Position
        'LightShade.Color = SpriteColor
    End Sub

    Public Sub Draw(ByRef w As RenderWindow) Implements IItemTicker.Draw
        'w.Draw(Sprite, New RenderStates(BlendMode.Multiply))
        w.Draw(LightShade, New RenderStates(BlendMode.Alpha))
    End Sub

    Sub New(Id As Integer, xspeed As Single, yspeed As Single, location As Point, radius As Integer, variation As Integer, color As SFML.Graphics.Color, AttachOffset As Point)

        _ID = 0
        _XSpeed = xspeed
        _YSpeed = yspeed
        _AttachOffset = AttachOffset
        _Location = location
        _Radius = radius
        _Sprite = New Sprite(LightText)
        _Sprite.Origin = New Vector2f(_Sprite.Origin.X + _Sprite.GetGlobalBounds.Width / 2, _Sprite.Origin.Y + _Sprite.GetGlobalBounds.Height / 2)
        _SpriteColor = color
        STRItemType = IItemTicker.ItemType.Light

        'To color the light

        LightShade = New Sprite(MainGame.LightShade)
        LightShade.Origin = New Vector2f(LightShade.Origin.X + LightShade.GetGlobalBounds.Width / 2, LightShade.Origin.Y + LightShade.GetGlobalBounds.Height / 2)
        LightShade.Color = color
        LightShade.Scale = New Vector2f(CSng((Size.Width) / LightShade.Texture.Size.X), CSng((Size.Height) / LightShade.Texture.Size.Y))
    End Sub


    Public Sub SetAttachedObj(ByRef Obj As IEntity, Optional ByVal CenterOffset As Boolean = True) Implements IItemTicker.SetAttachedObj
        _AttachedObj = Obj
        If CenterOffset Then
            _AttachOffset = New Point(CInt(Obj.Size.Width / 2 + AttachOffset.X), CInt(Obj.Size.Height / 2 + AttachOffset.Y))
        End If
    End Sub

    Public Function GetAttachedObj() As IEntity Implements IItemTicker.GetAttachedObj
        Return _AttachedObj
    End Function
End Class



