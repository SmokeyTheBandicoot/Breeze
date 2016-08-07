Public Structure GameStates

    Private _Name As String

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property


    Private Sub New(ByVal Name As String)
        _Name = Name
    End Sub

    Public Shared ReadOnly Property Loading() As GameStates
        Get
            Return New GameStates("Loading")
        End Get
    End Property

    Public Shared ReadOnly Property MainMenu() As GameStates
        Get
            Return New GameStates("MainMenu")
        End Get
    End Property

    Public Shared ReadOnly Property MainMenuOptions() As GameStates
        Get
            Return New GameStates("MainMenuOptions")
        End Get
    End Property

    Public Shared ReadOnly Property LevelEditor() As GameStates
        Get
            Return New GameStates("LevelEditor")
        End Get
    End Property

    Public Shared ReadOnly Property LevelSelect() As GameStates
        Get
            Return New GameStates("LevelSelect")
        End Get
    End Property

    Public Shared ReadOnly Property InGameOptions() As GameStates
        Get
            Return New GameStates("InGameOptions")
        End Get
    End Property

    Public Shared ReadOnly Property MainGame() As GameStates
        Get
            Return New GameStates("MainGame")
        End Get
    End Property

    Public Shared ReadOnly Property Credits() As GameStates
        Get
            Return New GameStates("Credits")
        End Get
    End Property
End Structure
