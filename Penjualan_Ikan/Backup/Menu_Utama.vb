Imports System.Data.SqlClient
Public Class Menu_Utama

    Private Sub DataIkanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataIkanToolStripMenuItem.Click
        Call Koneksi()
        Ikan.Show()
    End Sub

    Private Sub DataPembeliToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataPembeliToolStripMenuItem.Click
        Call Koneksi()
        Pembeli.Show()
    End Sub

    Private Sub DataAdminToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataAdminToolStripMenuItem.Click
        Call Koneksi()
        Admin.Show()
    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransaksiToolStripMenuItem.Click
        Call Koneksi()
        Transaksi.Show()
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeluarToolStripMenuItem.Click
        Call Koneksi()
        End
    End Sub
End Class