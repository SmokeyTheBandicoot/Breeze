Imports System.Drawing
Public Class Level

    Private _PlayerLightIntensity As Integer
    Private _PlayerLightColor As SFML.Graphics.Color = New SFML.Graphics.Color(0, 0, 0, 0)
    Private _Darkness As SFML.Graphics.Color = New SFML.Graphics.Color(0, 0, 0, 0)
    Private _BackGround As Background
    Private _BackGroundObjects As List(Of BackgroundObject)
    Private _Blocks As List(Of Block)
    Private _Items As List(Of IItemTicker)
    Private _PlayerPos As Point
    Private _Width As Long
    Private _height As Long

    Public Property PlayerLightIntensity As Integer
        Get
            Return _PlayerLightIntensity
        End Get
        Set(value As Integer)
            _PlayerLightIntensity = value
        End Set
    End Property

    Public Property PlayerLightColor As SFML.Graphics.Color
        Get
            Return _PlayerLightColor
        End Get
        Set(value As SFML.Graphics.Color)
            _PlayerLightColor = value
        End Set
    End Property

    Public Property Darkness As SFML.Graphics.Color
        Get
            Return _Darkness
        End Get
        Set(value As SFML.Graphics.Color)
            _Darkness = value
        End Set
    End Property

    Public Property BackGround As Background
        Get
            Return _BackGround
        End Get
        Set(ByVal value As Background)
            _BackGround = value
        End Set
    End Property

    Public Property BackGrounds As List(Of BackgroundObject)
        Get
            Return _BackGroundObjects
        End Get
        Set(ByVal value As List(Of BackgroundObject))
            _BackGroundObjects = value
        End Set
    End Property

    Public Property Blocks As List(Of Block)
        Get
            Return _Blocks
        End Get
        Set(ByVal value As List(Of Block))
            _Blocks = value
        End Set
    End Property

    Public Property Items As List(Of IItemTicker)
        Get
            Return _Items
        End Get
        Set(ByVal value As List(Of IItemTicker))
            _Items = value
        End Set
    End Property

    Public Property PlayerPos As Point
        Get
            Return _PlayerPos
        End Get
        Set(ByVal value As Point)
            _PlayerPos = value
        End Set
    End Property

    Public Property Width As Long
        Get
            Return _Width
        End Get
        Set(ByVal value As Long)
            _Width = value
        End Set
    End Property

    Public Property height As Long
        Get
            Return _height
        End Get
        Set(ByVal value As Long)
            _height = value
        End Set
    End Property



End Class
