Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Imaging
Public Class Form1

    Dim con As SqlConnection = New SqlConnection("Data Source=(localdb\MSSQLLocalDB;Database=24023;Integrated Security=true;")
    Public Sub executarQuery(MyCommand As SqlCommand, MyMessage As String)
        ' criar uma sub-procedure com o nome executarQuery que permite executar tarefas numa BD
        ' Dim command As New SqlCommand(query, con)
        con.Open() ' abre uma ligação à BD
        If MyCommand.ExecuteNonQuery = 1 Then
            MessageBox.Show(MyMessage)
        Else
            MessageBox.Show("Dados Não inseridos")
        End If
        con.Close() ' fecha a ligação à BD

    End Sub

    Private Sub btnInserir_Click(sender As Object, e As EventArgs) Handles btnInserir.Click
        Dim ms As MemoryStream
        imgImagem.Image.Save(ms, imgImagem.Image.Rawformat)
        Dim Img() As Byte
        Img = ms.ToArray()

        Dim insertQuery As String = "Insert INTO Stand(Marca,Modelo,Foto)Values('" & txtMarca.Text & "','" & txtModelo.Text & "', @img)"

        Dim command As New SqlCommand(insertQuery, con)
        command.Parameters.Add("@img", SqlDbType.Image).Value = Img

        executarQuery(command, "Dados e imagem inseridas com sucesso")
        txtMarca.Text = ""
        txtModelo.Text = ""

    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim opf As New OpenFileDialog

        opf.Filter = "Choose Image(*.JPG, *.PNG, *.GIF)|*.jpg;*.png;*.gif"

        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox2.Image = Image.FromFile(opf.FileName)
        End If

        Dim ms As New MemoryStream
        PictureBox2.Image.Save(ms, PictureBox2.Image.RawFormat)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub
End Class
