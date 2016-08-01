Imports System.Windows.Forms
Imports System.Math
Public Class Block
    Inherits PictureBox

    Public ID As Integer 'Type of block (Texture)
    Public Transparency As Byte = 0
    Public Hardness As Byte = 255 '255: Solid. 1 to 254: Fragile. 0: Fake
    Public Layer As Byte = 0
End Class
