Imports System.Windows.Forms
Imports SFML.Graphics

Public Class Background
    Inherits PictureBox

    Private _BGImage As New Sprite
    Private _HScroll As HorizontalScrollMode
    Private _VScroll As VerticalScrollMode
    Private _ScrollSpeedX As Single = 1
    Private _ScrollSpeedy As Single = 1

    Public Sub New(ByVal sprite As Sprite)
        BGImage = sprite
    End Sub

    Public Function GetHowManyRepeated(ByVal window As RenderWindow, ByVal Level As Level) As Integer
        Return CInt((Level.Width / BGImage.Texture.Size.X) + 1)
    End Function

    Public Property ScrollSpeedY As Single
        Get
            Return _ScrollSpeedy
        End Get
        Set(value As Single)
            _ScrollSpeedy = value
        End Set
    End Property

    Public Property ScrollSpeedX As Single
        Get
            Return _ScrollSpeedX
        End Get
        Set(value As Single)
            _ScrollSpeedX = value
        End Set
    End Property

    Public Property BGImage As Sprite
        Get
            Return _BGImage
        End Get
        Set(value As Sprite)
            _BGImage = value
        End Set
    End Property

    Public Property HScroll As HorizontalScrollMode
        Get
            Return _HScroll
        End Get
        Set(value As HorizontalScrollMode)
            _HScroll = value
        End Set
    End Property

    Public Property vscroll As VerticalScrollMode
        Get
            Return _VScroll
        End Get
        Set(value As VerticalScrollMode)
            _VScroll = value
        End Set
    End Property

    Public Enum HorizontalScrollMode As Byte
        Fixed = 0
        Repeated = 1
        Stretched = 2 'Repeats until the screen is entirely covered, then moves accordingly, never exeeding
    End Enum

    Public Enum VerticalScrollMode As Byte
        Fixed
        Repeated
        Stretched 'Repeats until the screen is entirely covered, then moves accordingly, never exeeding
    End Enum
End Class
