Imports System.Drawing
Public Class Level

    Private _BackGround As Background
    Private _BackGroundObjects As List(Of BackgroundObject)
    Private _Blocks As List(Of Block)
    Private _Items As List(Of Item)
    Private _PlayerPos As Point
    Private _Width As Long
    Private _height As Long

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

    Public Property Items As List(Of Item)
        Get
            Return _Items
        End Get
        Set(ByVal value As List(Of Item))
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
