Imports System.Data.SqlClient
Public Class Login

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Call Koneksi()
        CMD = New SqlCommand("select * from Admin where Usename='" & TextBox1.Text & "'and Password='" & TextBox2.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            MsgBox("Login Gagal")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()
        Else
            Me.Visible = False
            Menu_Utama.Show()
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Login_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        TextBox1.Focus()
    End Sub
End Class