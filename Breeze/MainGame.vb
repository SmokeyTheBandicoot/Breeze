Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCoreSFML
'Imports TGUI

Module MainGame
    'Base
    Dim IsExit As Boolean = False
    Dim IsPaused As Boolean = False

    'Threading
    Dim GameLoopThread As Thread

    'Framerate
    Dim TimeSpan As Date
    Dim FPS As Double

    'Engine
    Dim IsCont As Boolean = False
    Dim Pict As New PictureBox
    Dim angle As Single = 0
    Dim XSpeed As Single = 0
    Dim YSpeed As Single = 0
    Dim XAcceleration As Single = 0
    Dim YAcceleration As Single = 0
    Dim Gravity As Single = 0.2 'Breeze lows gravity
    Dim BreezeSpeed As Single = 0.1
    Public AngleMax As Single = 90 - 0.001
    Public MinY As Integer = 0
    Public MaxY As Integer = 646
    Public XLoc As Integer = 500
    Public Friction As Single = 0.98
    Public Env As FlightEnvironment = FlightEnvironment.Normal
    Public Wind As WeatherWind = WeatherWind.LandBreeze

    Public Enum FlightEnvironment As Byte
        Normal = 20
        Underwater = 10
        Moon = 5
    End Enum

    Public Enum WeatherWind As Integer
        LandBreeze = 35 'Weakest, Arid (Right)
        SeaBreeze = 35 'Weakest, Humid (Left)
        LightBreeze = 50 'Weak, almost non-existant, Clean
        MountainBreeze = 75 'Weak-Normal, Cold, Arid
        ValleyBreeze = 75 'Weak-Normal, Warm, Humid
        ModerateBreeze = 150 'Normal, Humid
        ModerateWind = 200 'Normal-Strong, Clean
        StrongWind = 250 'Strong, Cold, Clean
        TempestWind = 350 'Stronger, Cold, Humid
        WirlWind = 500 'Strongest, Cold
    End Enum

    'Rendering Engine
    Dim BackScroll As Single = 0

    'Level
    Dim Blocks(,) As Block

    'GamePlay
    Dim Score As Long = 0
    Dim Coins As Byte = 0
    Dim Lives As Byte = 3
    Dim HP As Byte = 100

    'Graphic Elements
    Dim Font As SFML.Graphics.Font
    Dim Player As New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Bankruptcy\[Bankruptcy]Bankruptcy.png"))
    Dim Background As New Sprite(New Texture("C:\Program Files (x86)\SMBX141\GFXPack\NSMB\NSMBWii\Backgrounds\New Super Mario Bros. Wii Custom Backgrounds\background2-19.gif"))
    Dim HUDTopLeft As New Text
    Dim Items As New List(Of Item)
    Dim ItemsSprite As New List(Of Sprite)


    'GUI elements
    'MainGame
    Dim LevelEditorButton As New SFMLButton

    Public Sub DoMainGame()
        If Not IsPaused Then

            'Start calculating FPS
            TimeSpan = Now

            'Execute Physics
            GameLoop()

            'Update Rendering

            'Update HUDs
            HUDTopLeft.DisplayedString = String.Format("Time: {1}{0}Position: X: {2}; Y:{3}{0}Speed: X: {4}; Y: {5}{0}Acceleration: X: {6}; Y: {7}{0}Rotation: {8}{0}Current State: {9}{0}FPS: {10}", vbNewLine, Now.ToString, Pict.Left, Pict.Top, XSpeed, YSpeed, XAcceleration, YAcceleration, Player.Rotation.ToString, CurrentState.Name.ToString, FPS.ToString)
            HUDTopLeft.CharacterSize = 24
            HUDTopLeft.Color = SFML.Graphics.Color.Black
            HUDTopLeft.Position = New Vector2f(0, 0)

            'Update Player position
            Player.Position = New Vector2f(Pict.Left, Pict.Top)
            Player.Rotation = 48 * CSng(Atan(YSpeed / XSpeed))
            Player.Color = New SFML.Graphics.Color(128, 128, 128)

            'Update CheckPoints

            'Update Items

            'Update Blocks

            'Update Background objects

            'Update Background
            Background.Position = New Vector2f(-BackScroll / 10, 0)


        End If

        'Draw everything
        'Draw Background
        Window.Draw(Background)

        'Draw Background objects
        'Draw Blocks
        'Draw Items

        'Draw Checkpoints

        'draw the GUI
        MainGameGUI.Draw(window)

        'Draw player
        window.Draw(Player)

        'Draw HUDs
        window.Draw(HUDTopLeft)

        'Finish calculating FPS
        FPS = 1000 / (Now - TimeSpan).TotalMilliseconds
    End Sub

#Region "Loading"
    Public Sub PostInitMainGame()

        HUDTopLeft.Font = New SFML.Graphics.Font("crash-a-like.ttf")
        HUDTopLeft.CharacterSize = 20

        'YLocation will be replaced by level's startpoint
        Player = New Sprite(New Texture("C:\GameShardsSoftware\paperplane.png"))
        Pict.Location = New Point(XLoc, 30)
        Pict.Size = New Size(20, 20)
    End Sub

    Sub GUILoadMainGame()
        Console.WriteLine("Loading Main Game GUI...")
        LevelEditorButton.TextAlign = ContentAlignment.MiddleLeft
        LevelEditorButton.Text = "LevelEditor"
        LevelEditorButton.ForeColor = Drawing.Color.Blue
        LevelEditorButton.SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
        LevelEditorButton.SFMLFontSize = 48
        LevelEditorButton.Toggleable = True
        LevelEditorButton.ToggleChangesSprite = False
        LevelEditorButton.ToggleChangesColor = True
        LevelEditorButton.Location = New Point(0, CInt(Window.Size.Y - 50))
        LevelEditorButton.Size = New Size(900, 50)
        LevelEditorButton.AutoPadding = True
        LevelEditorButton.ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
        LevelEditorButton.ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
        LevelEditorButton.SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
        LevelEditorButton.SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))

        MainGameGUI.Controls.Add(LevelEditorButton)
        Console.WriteLine("Successfully Loaded ""LevelEditorButton""")
        Console.WriteLine("Main GameGUI loaded successfully!")
    End Sub
#End Region



    Public Sub GameLoop()
        'Do While IsExit = False

        Application.DoEvents()

        If IsPaused Then

        Else
            'Start calculating framerate
            TimeSpan = Now

            'Do physics
            YSpeed += Gravity
            YSpeed += YAcceleration
            XSpeed = CSng(XSpeed + (Wind / 100))
            XSpeed += XAcceleration

            XSpeed *= Friction
            YSpeed *= Friction

            'Blocks.left -= xspeed
            'Pict.Left = CInt(Pict.Left + XSpeed)
            BackScroll += XSpeed / 25
            Pict.Top = CInt(Pict.Top + YSpeed)

            If Pict.Top > Window.Size.Y - Pict.Height Then
                Pict.Top = CInt(Window.Size.Y - Pict.Height)
                YSpeed *= -1
            ElseIf Pict.Top < 0 Then
                Pict.Top = 0
                YSpeed *= -1
            End If

            If Pict.Left > Window.Size.X - Pict.Width Then
                Pict.Left = CInt(Window.Size.X - Pict.Width)
                XSpeed *= -1
            ElseIf Pict.Left < 0 Then
                Pict.Left = 0
                XSpeed *= -1
            End If

            'Do light calculations
            If Items.Count > 0 Then
                For x = 0 To Items.Count - 1
                    If Items(x).Item = Item.ItemType.Light Then
                        Dim r As New IntRect(CInt(DirectCast(Items(x), Light).Location.X), CInt(DirectCast(Items(x), Light).Location.Y), CInt(DirectCast(Items(x), Light).AoE), CInt(DirectCast(Items(x), Light).AoE))
                        If Player.TextureRect.Intersects(r) Then
                            Player.Color = (DirectCast(Items(x), Light).Color)
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub LoadLevel(ByVal Path As String)

        'Format:
        'Name|||Author|||Music|||Background||ScrollSpeed|||StartPosX||StartPosY|||Blocks||Block1ID|Block1Transparency|Block1Hardness|Block1Layer||Block2ID|Block2Transparency|Block2Hardness|Block2Layer||BlockN...|||Items||Item1ID|XSpeed|Yspeed|ItemType||Item2ID....|||Backgrounds||Background1ID||Background2ID|||
        'NOTE: FinishPosX and FinishPosY are contained in an item. When player intersects one of those the level ends
        Try

        Catch ex As Exception

        End Try
    End Sub

    Sub MainGameKeyDown(sender As Object, e As SFML.Window.KeyEventArgs)

        Select Case True
            Case e.Code = Keyboard.Key.W Or e.Code = Keyboard.Key.Up
                Gravity = 0
                'Select Case Env
                '    Case FlightEnvironment.Normal
                '        YAcceleration = -0.2
                '    Case FlightEnvironment.Underwater
                '        YAcceleration = -0.1
                '    Case FlightEnvironment.Moon
                '        YAcceleration = -0.05
                'End Select
                YAcceleration = -XSpeed / (2 * Env)
            Case e.Code = Keyboard.Key.S Or e.Code = Keyboard.Key.Down
                Select Case Env
                    Case FlightEnvironment.Normal
                        YAcceleration = 0.2
                    Case FlightEnvironment.Underwater
                        YAcceleration = 0.1
                    Case FlightEnvironment.Moon
                        YAcceleration = 0.05
                End Select
            Case e.Code = Keyboard.Key.D Or e.Code = Keyboard.Key.Right

                'Select Case Env
                '    Case FlightEnvironment.Normal
                '        XAcceleration = CSng(BreezeSpeed + 0.1)
                '    Case FlightEnvironment.Underwater
                '        XAcceleration = CSng(BreezeSpeed + 0.05)
                '    Case FlightEnvironment.Moon
                '        XAcceleration = CSng(BreezeSpeed + 0.025)
                'End Select
                XAcceleration = CSng(Wind / 100 + (Wind / 200))
                'Wind = 0
            Case e.Code = Keyboard.Key.A Or e.Code = Keyboard.Key.Left

                'Select Case Env
                '    Case FlightEnvironment.Normal
                '        XAcceleration = CSng(BreezeSpeed - 0.1)
                '    Case FlightEnvironment.Underwater
                '        XAcceleration = CSng(BreezeSpeed - 0.05)
                '    Case FlightEnvironment.Moon
                '        XAcceleration = CSng(BreezeSpeed - 0.025)
                'End Select
                XAcceleration = CSng(-Wind / 100) 'CSng(Wind / 100 - (Wind / 200))
        End Select
    End Sub

    Sub MainGameKeyUp(ByVal sender As Object, e As SFML.Window.KeyEventArgs)

        Select Case Env
            Case FlightEnvironment.Normal
                Gravity = 0.2
            Case FlightEnvironment.Underwater
                Gravity = 0.1
            Case FlightEnvironment.Moon
                Gravity = 0.05
        End Select

        Select Case e.Code
            Case Keyboard.Key.Right Or Keyboard.Key.Left
                XAcceleration = BreezeSpeed
        End Select


        XAcceleration = 0
        YAcceleration = 0
    End Sub

#Region "Handles"

    Sub MainGameWindowClick(sender As Object, e As MouseButtonEventArgs)
        For x = 0 To MainGameGUI.Controls.Count - 1
            If TypeOf MainGameGUI.Controls(x) Is SFMLButton Then
                Dim b As New SFMLButton
                b = DirectCast(MainGameGUI.Controls(x), SFMLButton)
                If GGeom.CheckIfRectangleIntersectsPoint(b.Bounds, New Point(e.X, e.Y)) Then
                    MainGameButtonClick(b)
                Else
                    CurrentState = GameStates.MainGame
                End If
            End If
        Next
    End Sub

    Sub MainGameButtonClick(ByVal sender As SFMLButton)
        If sender Is LevelEditorButton Then
            CurrentState = GameStates.LevelEditor
        End If

    End Sub


#End Region
End Module
