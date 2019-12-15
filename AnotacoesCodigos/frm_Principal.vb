Imports Negocios
Imports ObjetoTransferencia

Public Class frm_Principal

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        '//Não gerar linhas automaticamente
        dgvPrincipal.AutoGenerateColumns = False
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub carregaGrid()
        Dim anotNegocios As New AnotacaoNegocios
        Dim coleNegocios As New AnotacaoColecao

        coleNegocios = anotNegocios.CarregarAnotacoes()

        dgvPrincipal.DataSource = Nothing
        dgvPrincipal.DataSource = coleNegocios

        dgvPrincipal.Update()
        dgvPrincipal.Refresh()

        If dgvPrincipal.Rows.Count = 0 Then
            txtAnotacoes.Text = ""
            txtPrincipal.Text = ""
        End If

    End Sub

    Private Sub abrirCampo()
        txtPrincipal.Enabled = True

        txtAnotacoes.ReadOnly = False

        dgvPrincipal.Enabled = False
        btnAdicionar.Enabled = False
        btnEditar.Enabled = False
        btnExcluir.Enabled = False

        btnConfirmar.Enabled = True
        btnCancelar.Enabled = True
    End Sub

    Private Sub fechaCampos()
        txtPrincipal.Enabled = False
        txtAnotacoes.ReadOnly = True

        dgvPrincipal.Enabled = True
        btnAdicionar.Enabled = True
        btnEditar.Enabled = True
        btnExcluir.Enabled = True

        btnConfirmar.Enabled = False
        btnCancelar.Enabled = False

        Me.Text = "CadFácil Sistemas - Anotações de Códigos"
        txtPrincipal.Text = ""
    End Sub

    Private Sub btnAdicionar_Click(sender As Object, e As EventArgs) Handles btnAdicionar.Click
        abrirCampo()
        Me.Text = "CadFácil Sistemas - Adicionando Anotações"

        txtPrincipal.Text = ""
        txtPrincipal.Focus()
        txtAnotacoes.Text = ""
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If dgvPrincipal.SelectedRows.Count = 0 Then
            MessageBox.Show("Nenhuma Anotação Selecionado")
        Else
            abrirCampo()
            txtPrincipal.Text = dgvPrincipal.Rows(dgvPrincipal.CurrentRow.Index).Cells(1).Value
            Me.Text = "CadFácil Sistemas - Editando Anotações"
        End If
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        Try
            If Me.Text = "CadFácil Sistemas - Adicionando Anotações" Then
                Dim vAnotacao As New Anotacao
                Dim vNegocios As New AnotacaoNegocios

                vAnotacao.aNome = txtPrincipal.Text.Trim
                vAnotacao.aCompleto = txtAnotacoes.Text.Trim

                vNegocios.Inserir(vAnotacao)
                MessageBox.Show("Anotação Inserido com Sucesso!", "Inserção de Anotação:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                carregaGrid()
            Else
                Dim vAnotacao As New Anotacao
                Dim vNegocios As New AnotacaoNegocios

                vAnotacao.aCodigo = dgvPrincipal.Rows(dgvPrincipal.CurrentRow.Index).Cells(0).Value
                vAnotacao.aNome = txtPrincipal.Text.Trim
                vAnotacao.aCompleto = txtAnotacoes.Text.Trim
                vNegocios.Alterar(vAnotacao)

                MessageBox.Show("Anotação Alterada com Sucesso!", "Criação de Anotação:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                carregaGrid()
            End If
        Catch ex As Exception
            MessageBox.Show("Não foi possível Inserir ou Alterar", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        fechaCampos()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        fechaCampos()

        If dgvPrincipal.Rows.Count > 0 Then
            txtAnotacoes.Text = dgvPrincipal.Rows(dgvPrincipal.CurrentRow.Index).Cells(2).Value
        Else
            txtAnotacoes.Text = ""
        End If

    End Sub

    Private Sub frm_Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        carregaGrid()
    End Sub

    Private Sub dgvPrincipal_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPrincipal.SelectionChanged
        txtAnotacoes.Text = dgvPrincipal.Rows(dgvPrincipal.CurrentRow.Index).Cells(2).Value
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If dgvPrincipal.SelectedRows.Count = 0 Then
            MessageBox.Show("Nenhuma Anotação Selecionado")
            Return
        Else
            If MessageBox.Show("Tem certeza que deseja Excluir o Tílulo " & dgvPrincipal.Rows(dgvPrincipal.CurrentRow.Index).Cells(1).Value.ToString.Trim.ToUpper & " de sua lista?", "Pergunta:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim anotSelecionado As Anotacao
                anotSelecionado = dgvPrincipal.SelectedRows(0).DataBoundItem

                Dim anotNegocios As New AnotacaoNegocios
                Dim retorno As String = anotNegocios.Excluir(anotSelecionado)

                MessageBox.Show("Título deletado com sucesso!", "Informação:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                carregaGrid()
            Else
                Return
            End If
        End If
    End Sub

End Class