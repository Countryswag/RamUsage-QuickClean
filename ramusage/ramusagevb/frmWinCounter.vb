Imports System.Threading

Imports System.IO

Public Class frmWinCounter

    ''declartion for recyclebin

    Private Declare Function SHEmptyRecycleBin Lib "shell32.dll" Alias "SHEmptyRecycleBinA" (ByVal hWnd As Int32, ByVal pszRootPath As String, ByVal dwFlags As Int32) As Int32

    Private Declare Function SHUpdateRecycleBinIcon Lib "shell32.dll" () As Int32

    ''Private const for cordinates to bin

    Private Const SHERB_NOCONFIRMATION = &H1

    Private Const SHERB_NOPROGRESSUI = &H2

    Private Const SHERB_NOSOUND = &H4

    ''CPU/RAM performance

    Protected cpuCounter As System.Diagnostics.PerformanceCounter

    Protected ramCounter As System.Diagnostics.PerformanceCounter

    Protected tmr As System.Timers.Timer

    ''Borderless form

    Dim drag As Boolean

    Dim mousex As Integer

    Dim mousey As Integer


    ''Temp clean

    Dim tempFolderPath As String = System.IO.Path.GetTempPath()

    Dim tempclean As Thread

    ''Bin empty function

    Private Sub EmptyRecycleBin()

        SHEmptyRecycleBin(Me.Handle.ToInt32, vbNullString, SHERB_NOCONFIRMATION)

        SHUpdateRecycleBinIcon()

    End Sub

    ''Temp Clean function

    Sub clean()

        For Each filePath In Directory.GetFiles(tempFolderPath)
            Try
                File.Delete(filePath)

            Catch

            End Try

        Next

    End Sub

    '' QuckClean start.

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        On Error Resume Next

        EmptyRecycleBin()

        tempclean = New System.Threading.Thread(AddressOf clean)

        tempclean.Start()

        MsgBox("PC Cleaned!", MsgBoxStyle.Information)

    End Sub

    ''Following Code is for the CPU/RAM Usage/percentage. 
    ''
    ''
    ''
    ''
    ''
    ''
    ''
    ''

    Sub New()

        ' This call is required by the designer.

        InitializeComponent()

        tmr = New System.Timers.Timer(500)

        AddHandler tmr.Elapsed, AddressOf Me.tmr_Elapsed

        ' Add any initialization after the InitializeComponent() call.

        cpuCounter = New System.Diagnostics.PerformanceCounter()

        cpuCounter.CategoryName = "Processor"

        cpuCounter.CounterName = "% Processor Time"

        cpuCounter.InstanceName = "_Total"

        ramCounter = New System.Diagnostics.PerformanceCounter("Memory", "Available MBytes")

        'init cpu counter 

        Dim tmp As Single = cpuCounter.NextValue()

    End Sub

    ''
    ''

    Private Sub btnCheckRamCpu_Click(sender As System.Object, e As System.EventArgs) Handles btnCheckRamCpu.Click

        tmr.Enabled = Not tmr.Enabled

        If tmr.Enabled Then

            btnCheckRamCpu.Text = "Stop monitor ram cpu"

        Else

            btnCheckRamCpu.Text = "Start monitor ram cpu"

        End If

    End Sub

    ''
    ''

    Private Delegate Sub dlgUpdateUI(ByVal ctl As Control, ByVal text As String)

    Private Sub tmr_Elapsed(sender As Object, e As Timers.ElapsedEventArgs)

        Dim cpumsg As String = "Cpu usage: " & cpuCounter.NextValue().ToString("#0") + "%"

        Dim rammsg As String = "Ram usage: " & ramCounter.NextValue().ToString("###,###,##0") + " Mb"

        If Me.InvokeRequired Then

            Dim d As New dlgUpdateUI(AddressOf SetLblText)

            Me.Invoke(d, lblCpu, cpumsg)

            Me.Invoke(d, lblRam, rammsg)

        Else

            SetLblText(lblCpu, cpumsg)

            SetLblText(lblRam, rammsg)

        End If

    End Sub


    ''
    ''

    Private Sub frmWinCounter_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        tmr.Enabled = False

    End Sub

    ''
    ''

    Private Sub SetLblText(lbl As Label, sText As String)

        lbl.Text = sText

    End Sub


    ''Drag form
    ''
    ''
    ''
    ''
    ''

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

        drag = True

        mousex = Windows.Forms.Cursor.Position.X - Me.Left

        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        If drag Then

            Me.Top = Windows.Forms.Cursor.Position.Y - mousey

            Me.Left = Windows.Forms.Cursor.Position.X - mousex

        End If

    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp

        drag = False

    End Sub

    '' Theme settings and other.

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Me.Width = (509)

        Me.Height = (303)

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        Me.BackColor = Color.Black

        btnCheckRamCpu.ForeColor = Color.Blue

        Button2.ForeColor = Color.Blue

        Button3.ForeColor = Color.Blue

        Button4.ForeColor = Color.Blue

        lblCpu.ForeColor = Color.Blue

        lblRam.ForeColor = Color.Blue

        CheckBox1.ForeColor = Color.Blue

        CheckBox2.ForeColor = Color.Blue

        CheckBox3.ForeColor = Color.Blue

        Label1.ForeColor = Color.Blue

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged

        Me.BackColor = Color.Black

        btnCheckRamCpu.ForeColor = Color.Red

        Button2.ForeColor = Color.Red

        Button3.ForeColor = Color.Red

        Button4.ForeColor = Color.Red

        lblCpu.ForeColor = Color.Red

        lblRam.ForeColor = Color.Red

        CheckBox1.ForeColor = Color.Red

        CheckBox2.ForeColor = Color.Red

        CheckBox3.ForeColor = Color.Red

        Label1.ForeColor = Color.Red

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Me.BackColor = Color.Black

        btnCheckRamCpu.ForeColor = Color.Lime

        Button2.ForeColor = Color.Lime

        Button3.ForeColor = Color.Lime

        Button4.ForeColor = Color.Lime

        lblCpu.ForeColor = Color.Lime

        lblRam.ForeColor = Color.Lime

        CheckBox1.ForeColor = Color.Lime

        CheckBox2.ForeColor = Color.Lime

        CheckBox3.ForeColor = Color.Lime

        Label1.ForeColor = Color.Lime

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Me.Width = (163)

        Me.Height = (169)

    End Sub

End Class
