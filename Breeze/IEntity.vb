Imports System.Drawing
Imports SFML.Graphics

Public Interface IEntity
    Property ID As Integer

    Property Size As Size

    Property Location As Point


    ReadOnly Property Center As Point


    ReadOnly Property Bounds As Rectangle
End Interface
