Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports GameShardsCore
Imports NAudio.Wave
Imports System.Windows.Forms
Imports System.Threading

Module Module1
    Dim WithEvents window As RenderWindow

    'Core


    'Base
    Dim IsExit As Boolean = False
    Dim IsPaused As Boolean = False
    Dim ShowGrid As Boolean = False
    Dim ResourceFold As String = "GameShardsCore\Resources\Fonts"
    Dim CurrentState As GameStates

    'Audio
    'Sound
    Dim RdMusic As New AudioFileReader("C:\GameShardsSoftware\Resources\Music\TerraSwoopForceTheme.mp3")
    Dim WaMusic As New WaveOut
    Dim MusicIsMuted As Boolean = False
    'Music
    Dim RdSound As AudioFileReader
    Dim WaSound As New WaveOut
    Dim SoundIsMuted As Boolean = False

    'Threading
    Dim GameLoopThread As Thread

    'Framerate
    Dim TimeSpan As Date
    Dim FPS As Double
    Dim IdealFPS As Integer = 30

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

    Sub Main()
        'CurrentState = GameStates.
        PreInit()
        Initialize()
        'PostInit(GUI)

        'Graphics thread
        While window.IsOpen

            If Not IsPaused Then

                'Execute Physics
                GameLoop()

                'Update Rendering

                'Update HUDs
                HUDTopLeft.DisplayedString = String.Format("Time: {1}{0}Position: X: {2}; Y:{3}{0}Speed: X: {4}; Y: {5}{0}Acceleration: X: {6}; Y: {7}{0}Rotation: {8}", vbNewLine, Now.ToString, Pict.Left, Pict.Top, XSpeed, YSpeed, XAcceleration, YAcceleration, Player.Rotation.ToString)
                HUDTopLeft.CharacterSize = 24
                HUDTopLeft.Color = SFML.Graphics.Color.Black
                HUDTopLeft.Position = New Vector2f(0, 0)

                'Update Player position
                Player.Position = New Vector2f(Pict.Left, Pict.Top)
                Player.Rotation = 48 * CSng(Atan(YSpeed / XSpeed))
                Player.Color = New SFML.Graphics.Color(128, 128, 128)

                'Update CheckPoints

                'Update Items
                ItemsSprite.Clear()
                ItemsSprite.Add(New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Bankruptcy\[Bankruptcy]Bankruptcy.png")))

                'Update Blocks

                'Update Background objects

                'Update Background
                Background.Position = New Vector2f(-BackScroll / 10, 0)

            End If

            'Clear window
            window.Clear()

            'Do events
            window.DispatchEvents()

            'Draw everything
            'Draw Background
            window.Draw(Background)

            'Draw Background objects
            'Draw Blocks
            'Draw Items
            window.Draw(ItemsSprite(0))

            'Draw Checkpoints

            'Draw player
            window.Draw(Player)

            'Draw HUDs
            window.Draw(HUDTopLeft)

            window.Display()

            'Player = New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Bankruptcy\[Bankruptcy]Bankruptcy.png"), New IntRect(Pict.Left, Pict.Top, 20, 20))
        End While
    End Sub

#Region "Loading"
    ''' <summary>
    ''' Initializes the window and the elements
    ''' </summary>
    Public Sub PreInit()
        'Console.WriteLine("Starting Sub Main")

        window = New RenderWindow(New VideoMode(800, 600), "Breeze")
        Player = New Sprite(New Texture("C:\GameShardsSoftware\paperplane.png"))
        'GUI.GlobalFont = New SFML.Graphics.Font("crash-a-like.ttf")

        'Dim Texture As New Texture("C:\\GameShardsCore\")
    End Sub

    ''' <summary>
    ''' Sets music and starting conditions
    ''' </summary>
    Public Sub Initialize()
        'Do here the things to do when the application starts
        WaMusic.Init(RdMusic)
        WaMusic.Play()

        window.SetFramerateLimit(60)

        'Should load here all resources
        HUDTopLeft.Font = New SFML.Graphics.Font("crash-a-like.ttf")

        'YLocation will be replaced by level's startpoint
        Pict.Location = New Point(XLoc, 30)
        Pict.Size = New Size(20, 20)

    End Sub

    ''' <summary>
    ''' Sets the GUI elements and gamestates
    ''' </summary>
    Public Sub PostInit()
        Items.Add(New Light(SFML.Graphics.Color.Red, 1000, 1))
        Items(0).Location = New Point(CInt(Player.Position.X), CInt(Player.Position.Y))
    End Sub
#End Region

    Public Sub GameLoop()
        'Do While IsExit = False

        Application.DoEvents()

        If IsPaused Then

        Else
            'Start calculating framerate
            TimeSpan = Now

            'If MusicIsMuted Then
            '    WaMusic.Volume = 0
            'Else
            '    WaMusic.Volume = CSng(TrackBar1.Value / 100)
            'End If

            'If SoundIsMuted Then
            '    WaSound.Volume = 0
            'Else
            '    WaSound.Volume = CSng(TrackBar2.Value / 100)
            'End If

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

            If Pict.Top > window.Size.Y - Pict.Height Then
                Pict.Top = CInt(window.Size.Y - Pict.Height)
                YSpeed *= -1
            ElseIf Pict.Top < 0 Then
                Pict.Top = 0
                YSpeed *= -1
            End If

            If Pict.Left > window.Size.X - Pict.Width Then
                Pict.Left = CInt(window.Size.X - Pict.Width)
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



            'Finish calculatingf FPS
            'If Not CheckBox1.Checked Then
            '    Thread.Sleep(GBase.SmallestPositiveValueINT(CInt(1000 / IdealFPS - (Now - TimeSpan).TotalMilliseconds)))
            'End If
            'FPS = Round((1 / (Now - TimeSpan).TotalMilliseconds) * 1000, 2)
        End If

        'Loop
    End Sub

    Public Sub LoadLevel(ByVal Path As String)

        'Format:
        'Name|||Author|||Music|||Background||ScrollSpeed|||StartPosX||StartPosY|||Blocks||Block1ID|Block1Transparency|Block1Hardness|Block1Layer||Block2ID|Block2Transparency|Block2Hardness|Block2Layer||BlockN...|||Items||Item1ID|XSpeed|Yspeed|ItemType||Item2ID....|||Backgrounds||Background1ID||Background2ID|||
        'NOTE: FinishPosX and FinishPosY are contained in an item. When player intersects one of those the level ends
        Try

        Catch ex As Exception

        End Try
    End Sub



    Sub WindowClosed(ByVal sender As Object, ByVal e As EventArgs) Handles window.Closed
        Application.Exit()
        window.Close()
        'Dim window = CType(sender, RenderWindow)
    End Sub

    Sub WindowClick(ByVal sender As Object, e As EventArgs) Handles window.MouseButtonPressed
        IsPaused = (Not IsPaused)
    End Sub

    Sub WindowKeyDown(ByVal sender As Object, e As SFML.Window.KeyEventArgs) Handles window.KeyPressed
        Console.WriteLine("Button pressed " & e.Code.ToString)
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

    Sub WindowKeyUp(ByVal sender As Object, e As SFML.Window.KeyEventArgs) Handles window.KeyReleased
        Console.WriteLine("Button released " & e.Code.ToString)

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
End Module
