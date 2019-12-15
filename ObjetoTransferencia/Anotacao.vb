Public Class Anotacao
    Private codigo As Integer
    Private nome As String
    Private completo As String

    Public Property aCodigo
        Get
            Return codigo
        End Get
        Set(value)
            codigo = value
        End Set
    End Property
    Public Property aNome
        Get
            Return nome
        End Get
        Set(value)
            nome = value
        End Set
    End Property
    Public Property aCompleto
        Get
            Return completo
        End Get
        Set(value)
            completo = value
        End Set
    End Property

End Class