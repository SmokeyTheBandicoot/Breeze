Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports GameShardsCoreSFML
Module MainMenu

    'Extend GUI
    Public CareerSelected As Boolean = False

    'Background
    Dim Background As New Background(New Sprite(New Texture("F:\Backup\Users\utente\Pictures\Disegni SmokeyTheBandicoot\Editati\TributeToGrootOriginalCut.png")))

    'Button Background
    Dim btn1 As New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
    Dim btn2 As New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))

    'GUI location
    Dim UnitX As Integer
    Dim UnitY As Integer

    'GUI
    Dim MainMenuGUI As New GUI
    Dim MainMenuNewLoadCancelGUI As New GUI

    Dim WithEvents CareerBTN As New SFMLButton
    Dim WithEvents LevelEditorBTN As New SFMLButton
    Dim WithEvents FreePlayBTN As New SFMLButton
    Dim WithEvents ArcadeBTN As New SFMLButton
    Dim WithEvents TimeTrialBTN As New SFMLButton
    Dim WithEvents OptionsBTN As New SFMLButton
    Dim WithEvents CreditsBTN As New SFMLButton
    Dim WithEvents ExitBTN As New SFMLButton
    Dim WithEvents TitleLBL As New SFMLLabel
    Dim WithEvents DownTextLBL As New SFMLLabel
    Dim WithEvents FBBTN As New SFMLButton
    Dim WithEvents TWBTN As New SFMLButton
    Dim WithEvents GHBTN As New SFMLButton
    Dim WithEvents EMBTN As New SFMLButton
    Dim WithEvents NewBTN As New SFMLButton
    Dim WithEvents LoadBTN As New SFMLButton
    Dim WithEvents CancelBTN As New SFMLButton

    Sub DoMainMenu()

        window.Draw(Background.BGImage)
        MainMenuGUI.Draw(window)
        If CareerSelected Then
            MainMenuNewLoadCancelGUI.Draw(window)
        End If

    End Sub

    Sub PostInitMainMenu()
        UnitX = CInt(window.Size.X / 3.5)
        UnitY = CInt(window.Size.Y / 80)

        Background.BGImage.Scale = New Vector2f(CSng(window.Size.X / Background.BGImage.Texture.Size.X), CSng(window.Size.Y / Background.BGImage.Texture.Size.Y))
    End Sub

    Sub GUILoadMainMenu()
        Console.WriteLine("Loading Main Menu GUI...")
        With CareerBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Career"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 30 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(CareerBTN)
        End With

        With LevelEditorBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Level Editor"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 36 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(LevelEditorBTN)
        End With

        With FreePlayBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Free Play"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 42 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(FreePlayBTN)
        End With

        With TimeTrialBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Time Trials"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 48 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(TimeTrialBTN)
        End With

        With ArcadeBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Arcade"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 54 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(ArcadeBTN)
        End With

        With OptionsBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Options"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 60 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(OptionsBTN)
        End With

        With CreditsBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Credits"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 66 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(CreditsBTN)
        End With

        With ExitBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Quit"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 72 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuGUI.Controls.Add(ExitBTN)
        End With

        With NewBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "New"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX \ 2, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 + CareerBTN.Size.Width \ 2 + UnitX \ 30), 30 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = New Sprite(btn2)
            .TextOffset = New Vector2f(0, -12)
            MainMenuNewLoadCancelGUI.Controls.Add(NewBTN)
        End With

        With LoadBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Load"
            .IDStr = .Text
            .ForeColor = Drawing.Color.White
            .SFMLFontSize = 48
            .SFMLFont = GlobalFont
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX \ 2, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 + CareerBTN.Size.Width \ 2 + UnitX \ 30), 36 * UnitY)
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = btn1
            .SpriteToggled = btn2
            .TextOffset = New Vector2f(0, -12)
            MainMenuNewLoadCancelGUI.Controls.Add(LoadBTN)
        End With
        Console.WriteLine("Loaded Main Menu GUI!")
    End Sub

    Sub MainMenuWindowClick(sender As Object, e As MouseButtonEventArgs)

        CareerSelected = False

        For x = 0 To MainMenuGUI.Controls.Count - 1
            If TypeOf MainMenuGUI.Controls(x) Is SFMLButton Then
                If CheckIfRectangleIntersectsPoint(DirectCast(MainMenuGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    Select Case DirectCast(MainMenuGUI.Controls(x), SFMLButton).IDStr.ToUpper
                        Case "QUIT"
                            window.Close()
                        Case "CAREER"
                            CareerSelected = True
                        Case "LEVEL EDITOR"
                            CurrentState = GameStates.LevelEditor
                        Case "OPTIONS"
                            CurrentState = GameStates.MainMenuOptions
                    End Select
                End If
            End If
        Next

        For x = 0 To MainMenuNewLoadCancelGUI.Controls.Count - 1
            If TypeOf MainMenuNewLoadCancelGUI.Controls(x) Is SFMLButton Then
                If CheckIfRectangleIntersectsPoint(DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    CareerSelected = False
                    Select Case DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).IDStr.ToUpper
                        Case "NEW"
                            CurrentState = GameStates.MainGame
                        Case "LOAD"
                            CurrentState = GameStates.LevelSelect
                        Case "CANCEL"
                            CareerSelected = False
                    End Select

                End If
            End If
        Next
    End Sub

    Sub MainMenuMouseMoved(sender As Object, e As MouseMoveEventArgs)
        For x = 0 To MainMenuGUI.Controls.Count - 1
            If TypeOf MainMenuGUI.Controls(x) Is SFMLButton Then
                If CheckIfRectangleIntersectsPoint(DirectCast(MainMenuGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    DirectCast(MainMenuGUI.Controls(x), SFMLButton).IsToggled = True
                Else
                    DirectCast(MainMenuGUI.Controls(x), SFMLButton).IsToggled = False
                End If
            End If
        Next

        For x = 0 To MainMenuNewLoadCancelGUI.Controls.Count - 1
            If TypeOf MainMenuNewLoadCancelGUI.Controls(x) Is SFMLButton Then
                If CheckIfRectangleIntersectsPoint(DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).IsToggled = True
                Else
                    DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).IsToggled = False
                End If
            End If
        Next
    End Sub

End Module
