Imports System.Windows.Forms
Imports System.Math
Imports GameShardsBreeze
Imports SFML.Graphics
Imports System.Drawing

Public Class Block
    Implements IMovableObject

    Public Property Color As SFML.Graphics.Color Implements IMovableObject.Color
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As SFML.Graphics.Color)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Hardness As Byte Implements IMovableObject.Hardness
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Byte)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ID As Integer Implements IMovableObject.ID
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Layer As Integer Implements IMovableObject.Layer
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Location As Point Implements IMovableObject.Location
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Point)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Size As Point Implements IMovableObject.Size
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Point)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Sprite As Sprite Implements IMovableObject.Sprite
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Sprite)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property XEspr As String Implements IMovableObject.XEspr
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property XSpeed As Single Implements IMovableObject.XSpeed
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Single)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property YEspr As String Implements IMovableObject.YEspr
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property YSpeed As Single Implements IMovableObject.YSpeed
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Single)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub Draw(ByRef w As RenderWindow) Implements IMovableObject.Draw
        Throw New NotImplementedException()
    End Sub
End Class
