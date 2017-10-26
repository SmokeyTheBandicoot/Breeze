Imports System.Drawing
Imports System.Threading

Module CollisionEngine

    Dim T As Thread
    Dim r1 As New Rectangle


    Sub CollisionEngineStart()
        T = New Thread(AddressOf CheckCollision)
        T.Start()
    End Sub

    Private Sub CheckCollision()
        'Do
        '    If MovableObjs.Count > 0 Then

        '        For x = 0 To MovableObjs.Count - 1


        '            'r1.Location = B.Location
        '            'r1.Size = B.Size

        '            If r1.IntersectsWith(Pict.Bounds) Then
        '                'B.PlayerCollide()
        '            End If
        '        Next

        '        Thread.Sleep(10)
        '    End If
        'Loop
    End Sub
End Module