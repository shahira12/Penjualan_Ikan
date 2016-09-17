Imports System.Data.SqlClient

Public Class Admin

    Sub Kosongkan()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox1.Focus()
    End Sub

    Sub databaru()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox1.Focus()
    End Sub

    Sub tampilgrid()
        Call Koneksi()
        DA = New SqlDataAdapter("select * from Admin", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DGV.DataSource = DS.Tables(0)
        DGV.ReadOnly = True
    End Sub

    Sub kodeotomatis()
        Call koneksi()
        TextBox1.Enabled = False
        CMD = New SqlCommand("SELECT * from Admin order by Id_Admin desc", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            TextBox1.Text = "ADM-0001"
        Else
            TextBox1.Text = Val(Microsoft.VisualBasic.Mid(DR.Item("Id_Admin").ToString, 5, 7)) + 1
            If Len(TextBox1.Text) = 1 Then
                TextBox1.Text = "ADM-000" & TextBox1.Text & ""
            ElseIf Len(TextBox1.Text) = 2 Then
                TextBox1.Text = "ADM-00" & TextBox1.Text & ""
            ElseIf Len(TextBox1.Text) = 3 Then
                TextBox1.Text = "ADM-0" & TextBox1.Text & ""
            End If
        End If
    End Sub

    Private Sub Admin_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call tampilgrid()
        Call Kosongkan()
        Call kodeotomatis()
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data belum lengkap")
            Exit Sub
        Else
            Try
                Call Koneksi()
                CMD = New SqlCommand("select * from Admin where Id_Admin='" & TextBox1.Text & "'", CONN)
                DR = CMD.ExecuteReader
                DR.Read()
                If Not DR.HasRows Then
                    Call Koneksi()
                    Dim simpan As String = " insert into Admin values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
                    CMD = New SqlCommand(simpan, CONN)
                    CMD.ExecuteNonQuery()
                Else
                    Call Koneksi()
                    Dim edit As String = " update Admin set Usename='" & TextBox2.Text & "',Password='" & TextBox3.Text & "' where Id_Admin='" & TextBox1.Text & "'"
                    CMD = New SqlCommand(edit, CONN)
                    CMD.ExecuteNonQuery()
                End If
                Call Kosongkan()
                Call tampilgrid()
                Call kodeotomatis()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Call Koneksi()
        Me.Visible = False
        Menu_Utama.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("anda harus mengisi data dulu")
            TextBox1.Focus()
            Exit Sub
        Else
            If MessageBox.Show("Hapus Data Ini...?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call Koneksi()
                Dim hapus As String = "delete from Admin where Id_Admin='" & TextBox1.Text & "'"
                CMD = New SqlCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                Call Kosongkan()
                Call tampilgrid()
                Call kodeotomatis()
            Else
                Call Kosongkan()
                Call kodeotomatis()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Call Kosongkan()
        Call kodeotomatis()
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox4.TextChanged
        Call Koneksi()
        CMD = New SqlCommand("select * from Admin where Usename like '%" & TextBox4.Text & "%'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            Call Koneksi()
            DA = New SqlDataAdapter("select * from Admin where Usename like '%" & TextBox4.Text & "%'", CONN)
            DS = New DataSet
            DA.Fill(DS)
            DGV.DataSource = DS.Tables(0)
        Else
            MsgBox("DATA TIDAK DI TEMUKAN")
        End If
    End Sub

    Private Sub DGV_CellContentClick_1(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV.CellContentClick
        On Error Resume Next
        TextBox1.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DGV.Rows(e.RowIndex).Cells(2).Value
    End Sub
End Class