Imports AcessoBancoDados
Imports ObjetoTransferencia

Public Class AnotacaoNegocios
    '//Instânciar - Criar um novo objeto baseado em um modelo.
    Private acessoSql As New AcessoDadosSqlServer

    Public Function Inserir(anotacoes As Anotacao) As String
        Try
            acessoSql.LimparParametros()
            acessoSql.AdicionarParametros("@nome", anotacoes.aNome)
            acessoSql.AdicionarParametros("@anotacao", anotacoes.aCompleto)
            Return (acessoSql.ExecutarManipulacao(CommandType.StoredProcedure, "uspAnotadoInserir")).ToString
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function Alterar(anotacoes As Anotacao) As String
        Try
            acessoSql.LimparParametros()
            acessoSql.AdicionarParametros("@codigo", anotacoes.aCodigo)
            acessoSql.AdicionarParametros("@nome", anotacoes.aNome)
            acessoSql.AdicionarParametros("@anotacao", anotacoes.aCompleto)
            Return (acessoSql.ExecutarManipulacao(CommandType.StoredProcedure, "uspAnotadoAlterar")).ToString
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function Excluir(anotacoes As Anotacao) As String
        Try
            acessoSql.LimparParametros()
            acessoSql.AdicionarParametros("@codigo", anotacoes.aCodigo)
            Return (acessoSql.ExecutarManipulacao(CommandType.StoredProcedure, "uspAnotadoExcluir")).ToString
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function CarregarAnotacoes() As AnotacaoColecao
        Try
            Dim anotColecao As New AnotacaoColecao
            acessoSql.LimparParametros()

            Dim tabelaDados As DataTable = acessoSql.ExecutarConsulta(CommandType.StoredProcedure, "uspCarregaLista")

            For Each linha As DataRow In tabelaDados.Rows
                Dim vAnotacao As New Anotacao

                vAnotacao.aCodigo = Convert.ToInt32(linha("anot_cod"))
                vAnotacao.aNome = Convert.ToString(linha("anot_nome")).Trim
                vAnotacao.aCompleto = Convert.ToString(linha("anot_completo")).Trim
                anotColecao.Add(vAnotacao)
            Next

            Return anotColecao

        Catch ex As Exception
            Throw New Exception("Não foi possível carregar suas anotações. Detalhes: " & ex.Message)
        End Try
    End Function

End Class