Public Class Form1
    Private Declare Function SetCursorPos Lib "user32" (ByVal XCoord As Integer, ByVal YCoord As Integer) As Integer
    Private Declare Function apimouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean
    Const MOUSEEVENTF_LEFTDOWN As Int32 = &H2
    Const MOUSEEVENTF_LEFTUP As Int32 = &H4
    Public coords As List(Of Point)
    Public IsTurnedOn As Boolean = False

    Private Class NativeMethods
        Public Declare Function GetAsyncKeyState Lib "user32" (vkey As Long) As Integer
    End Class

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If CBool(NativeMethods.GetAsyncKeyState(Keys.F2)) Then
            IsTurnedOn = Not IsTurnedOn
            Timer1.Enabled = IsTurnedOn
        End If
        If CBool(NativeMethods.GetAsyncKeyState(Keys.F1)) Then
            IsTurnedOn = Not IsTurnedOn
            Timer1.Enabled = False
        End If
    End Sub
    Private Sub CoordinatesButton_Click(sender As Object, e As EventArgs) Handles CoordinatesButton.Click
        TextBox1.Text = (MousePosition.X)
        TextBox2.Text = (MousePosition.Y)
        coords.Add(MousePosition)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        coords = New List(Of Point)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        Timer1.Start()
    End Sub

    Private Sub StopButton_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Timer1.Stop()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        coords.Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Interval = Speed
        Try
            If repeat > 0 Then
                Dim p As Point = coords(0)
                SetCursorPos(p.X, p.Y)
                apimouse_event(MOUSEEVENTF_LEFTDOWN + MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0)
                repeat = repeat - 1
                TextBox3.Text = repeat
            Else
                Timer1.Stop()
                TextBox3.Text = saveRepeat
                repeat = saveRepeat
            End If
        Catch
            MessageBox.Show("No coordinates entered yet.")
            Timer1.Stop()
        End Try

    End Sub

    Private Sub RepeatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepeatToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub ClickerHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClickerHelpToolStripMenuItem.Click
        Help.Show()
    End Sub

    Private Sub SpeedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpeedToolStripMenuItem.Click
        Form4.Show()
    End Sub

End Class
