Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCoreSFML
Imports GameShardsCore2
Imports System.IO

Module MainBackbone

    'Base
    Public IsExit As Boolean = False
    Public IsPaused As Boolean = False

    'Main Window
    Public WithEvents window As RenderWindow
    Public WithEvents lv As New GameWindow

    'GUIs
    Public MainGameGUI As New GUI
    Public OptionGUI As New GUI
    Public EditorGUI As New GUI
    Public LevelSelectGUI As New GUI
    Public InGameOptionsGUI As New GUI

    'Core
    Public GUtils As New Utils

    'GameStates
    Public CurrentState As GameStates

    'Audio
    'Sound
    Public RdMusic As New AudioFileReader("F:\Backup\Users\utente\Desktop\Gameshards\Musiche\Bossfight\Bossfight - Be Gone Mr. Gawne.mp3")
    Public WaMusic As New WaveOut
    Public MusicIsMuted As Boolean = False
    'Music
    Public RdSound As AudioFileReader
    Public WaSound As New WaveOut
    Public SoundIsMuted As Boolean = False

    'Font
    'Public GlobalFont As SFML.Graphics.Font = New SFML.Graphics.Font("crash-a-like.ttf")
    Public GlobalFont As SFML.Graphics.Font = New SFML.Graphics.Font("crash-a-like.ttf")
    Public KeyFont As New SFML.Graphics.Font("times.ttf")

    'Load/Save level
    Dim r As BinaryReader
    Dim w As BinaryWriter
    Dim l As Level

    'KeyBoard
    Dim KeyboardGUI As New GUI
    Dim Keys As New List(Of SFMLButton)

    Sub Main()
        'CurrentState = GameStates.
        PreInit()
        Initialize()
        PostInit()
        CollisionEngineStart()
        PostInitMainMenu()
        GUILoadMainMenu()
        PostInitMainGame()
        GUILoadMainGame()
        PostInitLevelEditor()
        GUILoadLevelEditor()
        PostInitLevelSelect()
        GUILoadLevelSelect()
        PostInitInGameOptions()
        GUILoadInGameOptions()


        'Graphics thread
        While Window.IsOpen

            'Common
            'Do events
            Window.DispatchEvents()

            'Clear window
            window.Clear()

            Application.DoEvents()

            'Do the things to do
            If CurrentState.Name.ToUpper = "MAINGAME" Then
                IsPaused = False
                DoMainGame()
            ElseIf CurrentState.Name.ToUpper = "LEVELEDITOR" Then
                DoEditor()
            ElseIf CurrentState.Name.ToUpper = "MAINMENU" Then
                DoMainMenu()
            ElseIf CurrentState.Name.ToUpper = "LEVELSELECT" Then
                DoLevelSelect()
            ElseIf CurrentState.Name.ToUpper = "INGAMEOPTIONS" Then
                IsPaused = True
                DoMainGame()
                DoInGameOptions()

            End If

            'Invalidate the window
            window.Display()

            window.SetTitle("GameShards Breeze")

        End While
    End Sub


    ''' <summary>
    ''' Initializes the window and the elements
    ''' </summary>
    Public Sub PreInit()

        lv.WindowState = FormWindowState.Normal
        'lv.Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Console.WriteLine("Starting Pre-Initialization")
        'window = New RenderWindow(New VideoMode(1080, 720), "Breeze", Styles.None)
        window = New RenderWindow(lv.Panel1.Handle)
        lv.Show()

        window.SetFramerateLimit(60)
        Console.WriteLine("Created a new RenderWindow with MAXFPS set to 60")
        'window.RequestFocus()
        'window.SetActive()
        Console.WriteLine("Focus Request Sent")
        Console.WriteLine("Pre-Initialization finished!")
    End Sub

    Public Sub Initialize()
        'Do here the things to do when the application starts
        WaMusic.Init(RdMusic)
        WaMusic.Play()

        CurrentState = GameStates.MainMenu

    End Sub

    Public Sub PostInit()

    End Sub

    Sub WindowClick(ByVal sender As Object, e As MouseButtonEventArgs) Handles window.MouseButtonPressed
        Select Case True
            Case CurrentState.Name.ToUpper = "MAINGAME"
                MainGameWindowClick(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELEDITOR"
                EditorWindowClick(sender, e)
            Case CurrentState.Name.ToUpper = "MAINMENU"
                MainMenuWindowClick(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELSELECT"
                LevelSelectWindowClick(sender, e)
            Case CurrentState.Name.ToUpper = "INGAMEOPTIONS"
                InGameOptionsWindowClick(sender, e)
        End Select
    End Sub

    Sub WindowClickUp(ByVal sender As Object, e As MouseButtonEventArgs) Handles window.MouseButtonReleased
        Select Case True
            'Case CurrentState.Name.ToUpper = "MAINGAME"
            '    MainGameWindowClickUp(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELEDITOR"
                LevelEditorWindowClickUp(sender, e)
                'Case CurrentState.Name.ToUpper = "MAINMENU"
                '    MainMenuWindowClickUp(sender, e)
                'Case CurrentState.Name.ToUpper = "LEVELSELECT"
                'LevelSelectWindowClickUp(sender, e)
            Case CurrentState.Name.ToUpper = "INGAMEOPTIONS"
                InGameOptionsClickUp(sender, e)
        End Select
    End Sub

    Sub WindowClosed(ByVal sender As Object, ByVal e As EventArgs) Handles window.Closed
        window.Close()
        'Dim window = CType(sender, RenderWindow)
    End Sub

    'KeyDowns and KeyUps are now handled by Windows.Forms window (GameShardsBreeze.designer), since the SFML window didn't register Keys anymore (because it was included in a Windows.Forms.Panel)
#Region "KeyDowns/Ups"
    'Sub WindowKeyDown(ByVal sender As Object, e As SFML.Window.KeyEventArgs) Handles window.KeyPressed
    '    MsgBox("test")
    '    Select Case True
    '        Case CurrentState.Name.ToUpper = "MAINGAME"
    '            MainGameKeyDown(sender, e)
    '        Case CurrentState.Name.ToUpper = "LEVELEDITOR"
    '            EditorKeyDown(sender, e)
    '        Case CurrentState.Name.ToUpper = "MAINMENU"

    '        Case CurrentState.Name.ToUpper = "LEVELSELECT"

    '    End Select
    'End Sub

    'Sub WindowKeyUp(ByVal sender As Object, e As SFML.Window.KeyEventArgs) Handles window.KeyReleased
    '    Select Case True
    '        Case CurrentState.Name.ToUpper = "MAINGAME"
    '            MainGameKeyUp(sender, e)
    '        Case CurrentState.Name.ToUpper = "LEVELEDITOR"
    '            EditorKeyup(sender, e)
    '        Case CurrentState.Name.ToUpper = "MAINMENU"
    '        Case CurrentState.Name.ToUpper = "LEVELSELECT"
    '    End Select
    'End Sub
#End Region

    Sub WindowMouseMove(sender As Object, e As MouseMoveEventArgs) Handles window.MouseMoved
        Select Case True
            Case CurrentState.Name.ToUpper = "MAINGAME"
                MainGameMouseMoved(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELEDITOR"
                LevelEditorMouseMoved(sender, e)
            Case CurrentState.Name.ToUpper = "MAINMENU"
                MainMenuMouseMoved(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELSELECT"
                LevelSelectMouseMoved(sender, e)
            Case CurrentState.Name.ToUpper = "INGAMEOPTIONS"
                InGameOptionsMouseMoved(sender, e)
        End Select
    End Sub

    Public Function LoadLevel(ByVal Path As String) As Level
        l = New Level
        'Format:
        'Name|||Author|||Music|||Background||ScrollSpeed|||StartPosX||StartPosY|||Blocks||Block1ID|Block1Transparency|Block1Hardness|Block1Layer||Block2ID|Block2Transparency|Block2Hardness|Block2Layer||BlockN...|||Items||Item1ID|XSpeed|Yspeed|ItemType||Item2ID....|||Backgrounds||Background1ID||Background2ID|||
        'NOTE: FinishPosX and FinishPosY are contained in an item. When player intersects one of those the level ends
        Try

            l.BackGround = New Background(New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\Levels\Meadows\background2-13.gif")))
            l.BackGround.HScroll = Background.HorizontalScrollMode.Repeated
            l.BackGround.ScrollSpeedX = 10
            l.Width = 20000

            Return l
        Catch ex As Exception

        End Try
    End Function

    Public Sub SaveLevel(ByVal Path As String)
        'Format:
        'Name|||Author|||Music|||Background||ScrollSpeed|||StartPosX||StartPosY|||Blocks||Block1ID|Block1Transparency|Block1Hardness|Block1Layer||Block2ID|Block2Transparency|Block2Hardness|Block2Layer||BlockN...|||Items||Item1ID|XSpeed|Yspeed|ItemType||Item2ID....|||Backgrounds||Background1ID||Background2ID|||
        'NOTE: FinishPosX and FinishPosY are contained in an item. When player intersects one of those the level ends
        Try

        Catch ex As Exception

        End Try
    End Sub
End Module
