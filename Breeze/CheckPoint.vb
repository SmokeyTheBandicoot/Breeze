Imports System.Drawing
Imports GameShardsBreeze
Imports SFML.Graphics

Public Class CheckPoint
    Implements IItemTicker

    'Item Data
    Public IsLatest As Boolean = False
    Public IsEndLevel As Boolean = False

    'Save data
    Public PlXSpeed As Single = 0
    Public PlYSpeed As Single = 0
    Public PlXAccel As Single = 0
    Public PlYAccel As Single = 0
    Public PlGravity As Single = 0
    Public PlCoins As Single = 0
    Public PlScore As Single = 0

    Public Property ID As Integer Implements IItemTicker.ID
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property XSpeed As Single Implements IItemTicker.XSpeed
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Single)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property YSpeed As Single Implements IItemTicker.YSpeed
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Single)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property STRItemType As IItemTicker.ItemType Implements IItemTicker.STRItemType
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As IItemTicker.ItemType)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Size As Size Implements IItemTicker.Size
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Size)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Location As Point Implements IItemTicker.Location
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Point)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Sprite As Sprite Implements IItemTicker.Sprite
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Sprite)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SpriteColor As SFML.Graphics.Color Implements IItemTicker.SpriteColor
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As SFML.Graphics.Color)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AttachedObj As IEntity Implements IItemTicker.AttachedObj
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As IEntity)
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property Center As Point Implements IEntity.Center
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private ReadOnly Property IEntity_Bounds As Rectangle Implements IEntity.Bounds
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Sub Tick() Implements IItemTicker.Tick
        Throw New NotImplementedException()
    End Sub

    Public Sub Draw(ByRef w As RenderWindow) Implements IItemTicker.Draw
        Throw New NotImplementedException()
    End Sub

    Public Sub SetAttachedObj(ByRef Obj As IEntity, Optional ByVal CenterOffset As Boolean = True) Implements IItemTicker.SetAttachedObj
        Throw New NotImplementedException()
    End Sub

    Public Function GetAttachedObj() As IEntity Implements IItemTicker.GetAttachedObj
        Throw New NotImplementedException()
    End Function
End Class